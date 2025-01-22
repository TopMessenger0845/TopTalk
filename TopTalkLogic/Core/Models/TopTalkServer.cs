
using System.Net;
using TopNetwork.RequestResponse;
using TopNetwork.Services.MessageBuilder;
using TopNetwork.Services;
using TopTalk.Core.Models.MessageBuilder.Messages;
using TopNetwork.Core;
using TopTalk.Core.Models.MessageBuilder.Chats;
using TopTalk.Core.Models.MessageBuilder.Contacts;
using TopTalk.Core.Services.Builders;
using TopTalkLogic.Core.Services;

namespace TopTalkLogic.Core.Models
{
    public class TopTalkServer
    {
        // Регистрация всех фабрик для типов сообщений отправляемых сервером 
        private static readonly MessageBuilderService _msgService = new MessageBuilderService()
                    .Register(() => new ErroreMessageBuilder())
                    .Register(() => new EndSessionNotificationMessageBuilder());

        private readonly TrackerUserActivityService _activityService;
        private readonly DbService _dbService;
        private readonly RrServerHandlerBase _handlers;
        private readonly DbAuthenticationService _authService;

        private RrServer _server = new();

        public EndPoint? EndPoint => _server.CurrentEndPoint;
        public bool IsRunning => _server.IsRunning;
        public int CountOpenSessions => _server.CountOpenSessions;


        public TopTalkServer(string? userFilePath = null)
        {
            //_server.Logger = Logger.LogString;

            _activityService = new(_msgService);
            _dbService = new DbService();

            _authService = _server
               .RegisterService(_msgService)
                .RegisterService(_activityService)
                .RegisterService(_dbService)
                .GetService<DbAuthenticationService>()!;

            _handlers = new RrServerHandlerBase()
                .AddHandlerForMessageType(CreateChatRequestData.MsgType, async (client, msg, context) =>
                {
                    return await SafeWrapperForHandler(client, msg, context, async (client, msg, context) =>
                    {
                        if (CheckUserAuth(client, out var msgToUser))
                            return msgToUser;

                        var requestData = CreateChatRequest.Parse(msg);
                        var chatId = await _dbService.RegisterNewChat(requestData.ChatName, requestData.ChatType, _authService.GetUserBy(client).Id);

                        return _msgService.BuildMessage<CreateChatResponse, CreateChatResponseData>(builder => builder.SetChatId(chatId));
                    });
                })
                .AddHandlerForMessageType(SendMessageRequestData.MsgType, async (client, msg, context) =>
                {
                    return await SafeWrapperForHandler(client, msg, context, async (client, msg, context) =>
                    {
                        if (CheckUserAuth(client, out var msgToUser))
                            return msgToUser;

                        var requestData = SendMessageRequest.Parse(msg);
                        Guid senderId = _authService.GetUserBy(client).Id;

                        await _dbService.AddMessage(requestData.Message, senderId, requestData.ChatId);
                        var users = await _dbService.GetAllUserByChat(requestData.ChatId);

                        var chatHistory = await _dbService.GetMessagesByChatAsync(requestData.ChatId);
                        var chatUpdatedMsg = _msgService.BuildMessage<ChatUpdateNotification, ChatUpdateNotificationData>(builder => builder.SetChatHistory(chatHistory));

                        var tasks = new List<Task>();

                        foreach (var user in users)
                        {
                            var userConnection = _authService.GetTopClientBy(user.User.Login);
                            if (userConnection == null) continue;

                            tasks.Add(userConnection.SendMessageAsync(chatUpdatedMsg));
                        }

                        await Task.WhenAll(tasks);

                        return null;
                    });
                })
                .AddHandlerForMessageType(DeleteMessageRequestData.MsgType, async (client, msg, context) =>
                {
                    return await SafeWrapperForHandler(client, msg, context, async (client, msg, context) =>
                    {
                        if (CheckUserAuth(client, out var msgToUser))
                            return msgToUser;


                        return null;
                    });
                })
                .AddHandlerForMessageType(EditMessageRequestData.MsgType, async (client, msg, context) =>
                {
                    return await SafeWrapperForHandler(client, msg, context, async (client, msg, context) =>
                    {
                        if (CheckUserAuth(client, out var msgToUser))
                            return msgToUser;


                        return null;
                    });
                })
                .AddHandlerForMessageType(CreateChatRequestData.MsgType, async (client, msg, context) =>
                {
                    return await SafeWrapperForHandler(client, msg, context, async (client, msg, context) =>
                    {

                        return null;
                    });
                })
                .AddHandlerForMessageType(DeleteChatRequestData.MsgType, async (client, msg, context) =>
                {
                    return await SafeWrapperForHandler(client, msg, context, async (client, msg, context) =>
                    {

                        return null;
                    });
                })
                .AddHandlerForMessageType(SubscriptionRequestData.MsgType, async (client, msg, context) =>
                {
                    return await SafeWrapperForHandler(client, msg, context, async (client, msg, context) =>
                    {

                        return null;
                    });
                })
                .AddHandlerForMessageType(InviteUserRequestData.MsgType, async (client, msg, context) =>
                {
                    return await SafeWrapperForHandler(client, msg, context, async (client, msg, context) =>
                    {

                        return null;
                    });
                });

            _server.SetSessionFactory(SessionFactory);
        }


        public void SetEndPoint(IPEndPoint endPoint)
            => _server.SetEndPoint(endPoint);

        public async Task StartServer(CancellationToken token = default)
            => await _server.StartAsync(token);

        public async Task StopServer()
            => await _server.StopAsync();

        private async Task<ClientSession?> SessionFactory(TopClient client, ServiceRegistry context, LogString? logger)
        {
            ClientSession session = new(client, _handlers, context)
            {
                logger = logger,
            };

            _activityService.UpdateLastActive(client);

            return session;
        }

        private async Task<Message> SafeWrapperForHandler(TopClient client, Message msg, ServiceRegistry context, Func<TopClient, Message, ServiceRegistry, Task<Message?>> handler)
        {
            try
            {
                return await handler?.Invoke(client, msg, context);
            }
            catch (Exception ex)
            {
                //Logger.LogString($"[Server]: Ошибка обработки {msg.MessageType} от [{client.RemoteEndPoint}].\n{ex.Message}");
                return _msgService.BuildMessage<ErroreMessageBuilder, ErroreData>(builder => builder
                    .SetPayload($"Невозможно обработать {msg.MessageType}.\n{ex.Message}")
                );
            }
        }

        private bool CheckUserAuth(TopClient client, out Message? msg)
        {
            if (!_authService.IsAuthClient(client))
            {
                msg = _msgService.BuildMessage<ErroreMessageBuilder, ErroreData>(builder =>
                    builder.SetPayload("Для использования данной операции авторизуйтесь."));

                return false;
            }

            msg = null;
            return true;
        }
    }
}
