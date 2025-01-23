
using Microsoft.Toolkit.Uwp.Notifications;
using System.Net;
using System.Windows;
using TopTalkLogic.Core.Models;

namespace TopTalk.ViewModels
{
    public class TopTalkClientVm : ClientViewModel
    {
        private static SemaphoreSlim _semaphore = new(1, 1);
        public static TopTalkClient Client { get; set; }

        public string _connectionStatus = "Статус подключения: ";
        public string СonnectionStatus
        {
            set => SetProperty(ref _connectionStatus, value);
            get => _connectionStatus;
        }

        public string _currentСhat = "Текущий чат: системный";
        public string СurrentСhat
        {
            set => SetProperty(ref _currentСhat, value);
            get => _currentСhat;
        }

        public string _currentUser = "<NoName>";
        public string CurrentUser
        {
            set => SetProperty(ref _currentUser, value);
            get => _currentUser;
        }

        public TopTalkClientVm() : base(GetClient().Result)
        {
            // Добавляем команды специфичные для ChatClient
            _commandProcessor!
                .AddCommand("/Authentication", "/Authentication <Login> <Password>", HandleAuthentication)
                .AddCommand("/SignOut", "/SignOut", HandleSignOut)
                .AddCommand("/Disconnect", "/Disconnect", HandleDisconnect)
                .AddCommand("/Clear", "/Clear", HandleClear)
                .AddCommand("/Connect", "/Connect", HandleConnect);
            //    .AddCommand("/Delay", "/Delay <milliseconds>", HandleDelay)
            //    .AddCommand("/SendMessage", "/SendMessage <message>", HandleSendMessage);

            СonnectionStatus = "Статус подключения: " + (Client.IsConnected ? "подключено" : "не удалось установить соединение");
        }

        private async Task HandleAuthentication(string input)
        {
            var parts = input.Split(' ');
            if (parts.Length == 3)
            {
                await Client.Login(parts[1], parts[2]);
            }
            else
            {
                ShowMessageBox("Команда /Authentication должна быть в формате: { /Authentication <Login> <Password> }");
            }
        }

        private async Task HandleSignOut(string input)
            => await Client.SendCloseSessionRequest();

        private async Task HandleClear(string input)
            => Application.Current.Dispatcher.Invoke(() => Messages.Clear());

        private async Task HandleConnect(string input)
        {
            if (Client.IsConnected)
                return;

            await Client.ConnectAsync("127.0.0.1", 5335);

            AddMessage("Server", "Успешно подключились!"); 
        }
        private async Task HandleDisconnect(string input)
            => Client.Disconnect();

        #region Not touch
        public static async Task Init()
        {
            await _semaphore.WaitAsync();
            try
            {
                Client = new TopTalkClient();
                await Client.ConnectAsync("127.0.0.1", 5335);

                Client.OnErroreFromServer += Client_OnErroreFromServer;
                Client.OnErroreOnClient += data => MessageBox.Show(data, "Ошибка на клиенте", MessageBoxButton.OK, MessageBoxImage.Error);

                new ToastContentBuilder().AddText("Connected to Server").AddText("Успешное подключение к серверу...").Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fatal Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
            finally { _semaphore.Release(); }
        }

        public static async Task<TopTalkClient> GetClient()
        {
            await _semaphore.WaitAsync();
            try { return Client; }
            finally { _semaphore.Release(); }
        }
        protected override async Task ExecuteCommandAsync(string input)
        {
            try
            {
                await _commandProcessor.ExecuteCommand(input);
            }
            catch (Exception ex)
            {
                AddMessage("_commandProcessor", ex.Message);
            }
        }

        private static void Client_OnErroreFromServer(TopNetwork.Services.MessageBuilder.ErroreData obj)
        {
            MessageBox.Show(obj.Payload, "Ошибка на сервере", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #endregion
    }
}
