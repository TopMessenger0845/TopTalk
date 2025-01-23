
using System.Collections.Concurrent;
using TopNetwork.Core;
using TopNetwork.RequestResponse;
using TopNetwork.Services.MessageBuilder;
using TopTalk.Core.Models.MessageBuilder.Users;
using TopTalk.Core.Storage.Models;

namespace TopTalkLogic.Core.Services
{
    public class DbAuthenticationService
    {
        private readonly SemaphoreSlim _sessionsLock = new(1, 1);
        private readonly MessageBuilderService _msgService;
        private readonly ConcurrentDictionary<TopClient, ClientTimerSession> _authenticatedSessions = new();
        private TimeSpan _maxSessionDuration = TimeSpan.FromHours(3);

        public TimeSpan MaxSessionDuration => _maxSessionDuration;
        public int CountAuthConnections => _authenticatedSessions.Count;
        public LogString? Logger { get; set; }

        [Inject]
        public DbUserService UserService { get; set; }
        [Inject]
        public MessageBuilderService MsgService { get; set; }

        public bool IsAuthClient(TopClient client) => _authenticatedSessions.ContainsKey(client);
        public UserEntity GetUserBy(TopClient client) => _authenticatedSessions[client].User;
        public TopClient? GetTopClientBy(string login)
        {
            var res = _authenticatedSessions.Where(s => s.Value.Login == login);

            if (res.Any())
                return res.First().Key;

            return null;
        }

        public async Task<Message?> AuthenticateClient(TopClient client, AuthenticationRequestData requestData)
        {
            var user = await UserService.Authenticate(requestData.Login, requestData.Password);
            if (user != null)
            {
                if (_authenticatedSessions.Values.Any(s => s.Login == requestData.Login))
                {
                    return BuildFailedAuthResponse("Этот логин уже используется.");
                }

                var session = new ClientTimerSession(client, user, _maxSessionDuration, NotifySessionExpired);
                client.OnConnectionLost += () => CloseSession(client);

                _authenticatedSessions[client] = session;
                return BuildSuccessAuthResponse();
            }

            return BuildFailedAuthResponse("Неверный логин или пароль.");
        }

        public async Task<Message?> RegisterClient(TopClient client, RegisterRequestData requestData)
        {
            try
            {
                await UserService.RegisterUser(requestData.Login, requestData.Password);
                Logger?.Invoke($"[AuthenticationService]: Клиент [{client.LastUseEndPoint}] успешно зарегал нового пользователя, под логином - {requestData.Login}.");
                return _msgService.BuildMessage<RegisterResponse, RegisterResponseData>(builder => builder.SetExplanatoryMsg($"Вы успешно зарегистрировали нового пользователя, под логином - {requestData.Login}."));
            }
            catch (Exception ex)
            {
                Logger?.Invoke($"[AuthenticationService]: Ошибка при регистрации Клиента - [{client.LastUseEndPoint}]; Errore: {ex.Message}");
                return _msgService.BuildMessage<RegisterResponse, RegisterResponseData>(builder => builder.SetExplanatoryMsg($"Ошибка регистрации: {ex.Message}"));
            }
        }

        public void CloseSession(TopClient client)
        {
            if (_authenticatedSessions.TryRemove(client, out var session))
            {
                session.Dispose();
            }
        }

        private async Task NotifySessionExpired(TopClient client)
        {
            CloseSession(client);
            if (client.IsConnected)
            {
                try
                {
                    await client.SendMessageAsync(_msgService.BuildMessage<EndSessionNotificationMessageBuilder, EndSessionNotificationData>(builder => builder
                        .SetPayload("Ваша сессия истекла.")));
                }
                catch { }
            }

            client.Disconnect();
        }

        public async Task UpdateSessionDuration(TimeSpan newDuration)
        {
            await _sessionsLock.WaitAsync();
            try
            {
                _maxSessionDuration = newDuration;

                foreach (var session in _authenticatedSessions.Values)
                {
                    session.UpdateDuration(newDuration);
                }
            }
            finally
            {
                _sessionsLock.Release();
            }
        }

        private Message BuildSuccessAuthResponse() =>
            _msgService.BuildMessage<AuthenticationResponseMessageBuilder, AuthenticationResponseData>(builder => builder
                .SetAuthentication(true)
                .SetExplanatoryMsg($"Вы успешно авторизовались!\nВаша сессия длится - {MaxSessionDuration.TotalMinutes} Мин."));

        private Message BuildFailedAuthResponse(string reason) =>
            _msgService.BuildMessage<AuthenticationResponseMessageBuilder, AuthenticationResponseData>(builder => builder
                .SetAuthentication(false)
                .SetExplanatoryMsg(reason));
    }

    public class ClientTimerSession
    {
        private readonly TopClient _client;
        private readonly Timer _timer;
        private readonly Func<TopClient, Task> _onSessionExpired;
        private readonly object _lock = new(); // Для потокобезопасности
        private readonly UserEntity _user;
        private DateTime _startTime; // Время последнего обновления
        private TimeSpan _remainingDuration; // Оставшееся время
        private bool _isDisposed; // Флаг для проверки состояния сессии

        public string Login => User.Login;
        public UserEntity User => _user;

        public ClientTimerSession(TopClient client, UserEntity user, TimeSpan duration, Func<TopClient, Task> onSessionExpired)
        {
            _client = client;
            _user = user;

            _onSessionExpired = onSessionExpired;
            _remainingDuration = duration;
            _startTime = DateTime.UtcNow;

            _timer = new Timer(OnTimerElapsed, null, duration, Timeout.InfiniteTimeSpan);
        }

        public void UpdateDuration(TimeSpan newDuration)
        {
            lock (_lock)
            {
                if (_isDisposed)
                    return;

                // Вычисляем прошедшее время
                var elapsedTime = DateTime.UtcNow - _startTime;

                if (elapsedTime >= newDuration)
                {
                    // Таймер уже истёк или истечёт немедленно
                    TriggerExpiration();
                }
                else
                {
                    // Обновляем оставшееся время и перезапускаем таймер
                    _remainingDuration = newDuration - elapsedTime;
                    _startTime = DateTime.UtcNow;
                    _timer.Change(_remainingDuration, Timeout.InfiniteTimeSpan);
                }
            }
        }

        private void TriggerExpiration()
        {
            // Ручной вызов истечения таймера
            Dispose();
            _ = _onSessionExpired(_client);
        }

        private async void OnTimerElapsed(object? state)
        {
            lock (_lock)
            {
                if (_isDisposed)
                    return;
                _isDisposed = true; // Защищаем от повторного вызова
            }

            await _onSessionExpired(_client);
        }

        public void Dispose()
        {
            lock (_lock)
            {
                if (_isDisposed)
                    return;

                _isDisposed = true;
                _timer.Dispose();
            }
        }
    }
}
