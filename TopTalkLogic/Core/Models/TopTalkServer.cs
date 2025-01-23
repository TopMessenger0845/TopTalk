﻿
using System.Net;
using TopNetwork.RequestResponse;
using TopNetwork.Services.MessageBuilder;
using TopNetwork.Services;
using TopTalk.Core.Models.MessageBuilder.Messages;
using TopNetwork.Core;
using TopTalk.Core.Models.MessageBuilder.Chats;
using TopTalkLogic.Core.Services;
using TopTalk.Core.Models.MessageBuilder.Users;
using TopTalkLogic.Core.Models.MessageBuilder.Chats;

namespace TopTalkLogic.Core.Models
{
    public class TopTalkServer
    {
        // Регистрация всех фабрик для типов сообщений отправляемых сервером 
        private static readonly MessageBuilderService _msgService = new MessageBuilderService()
                    .Register(() => new ErroreMessageBuilder())
                    .Register(() => new EndSessionNotificationMessageBuilder())
                    .Register(() => new ChatUpdateNotification())
                    .Register(() => new CreateChatResponse())
                    .Register(() => new GetMyChatsResponse());

        private readonly TrackerUserActivityService _activityService;
        private readonly DbService _dbService;
        private readonly RrServerHandlerBase _handlers;
        private readonly DbAuthenticationService _authService;

        private RrServer _server = new();

        public EndPoint? EndPoint => _server.CurrentEndPoint;
        public bool IsRunning => _server.IsRunning;
        public int CountOpenSessions => _server.CountOpenSessions;
        public LogString? logger;


        public TopTalkServer(LogString? logger = null, string? userFilePath = null)
        {
            //_server.Logger = Logger.LogString;
            _server.Logger = logger;

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
                        if (CheckUserNotAuth(client, out var msgToUser))
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
                        if (CheckUserNotAuth(client, out var msgToUser))
                            return msgToUser;

                        var requestData = SendMessageRequest.Parse(msg);
                        Guid senderId = _authService.GetUserBy(client).Id;

                        await _dbService.AddMessage(requestData.Message, senderId, requestData.ChatId);
                        var users = await _dbService.GetAllUserByChat(requestData.ChatId);

                        var chatHistory = await _dbService.GetMessagesByChatAsync(requestData.ChatId);
                        var chatUpdatedMsg = _msgService.BuildMessage<ChatUpdateNotification, ChatUpdateNotificationData>(builder => builder.SetChatHistory(chatHistory).SetChatId(requestData.ChatId));

                        await client.SendMessageAsync(chatUpdatedMsg);

                        //var tasks = new List<Task>();

                        //foreach (var user in users)
                        //{
                        //    var userConnection = _authService.GetTopClientBy(user.User.Login);
                        //    if (userConnection == null) continue;

                        //    await userConnection.SendMessageAsync(chatUpdatedMsg);
                        //}

                        //await Task.WhenAll(tasks);

                        return null;
                    });
                })
                .AddHandlerForMessageType(GetMyChatsRequestData.MsgType, async (client, msg, context) =>
                {
                    return await SafeWrapperForHandler(client, msg, context, async (client, msg, context) =>
                    {
                        if (CheckUserNotAuth(client, out var msgToUser))
                            return msgToUser;

                        var chatIds = (await _dbService.GetChatsByUser(_authService.GetUserBy(client).Id)).Select(chat => chat.Id).ToList();

                        return _msgService.BuildMessage<GetMyChatsResponse, GetMyChatsResponseData>(builder => builder.SetChatHistory(chatIds));
                    });
                })
                .AddHandlerForMessageType(ChatHistoryRequestData.MsgType, async (client, msg, context) =>
                {
                    return await SafeWrapperForHandler(client, msg, context, async (client, msg, context) =>
                    {
                        var requestData = ChatHistoryRequest.Parse(msg);
                        var messages = await _dbService.GetMessagesByChatAsync(requestData.ChatId);

                        return  _msgService.BuildMessage<ChatUpdateNotification, ChatUpdateNotificationData>(builder => builder.SetChatHistory(messages).SetChatId(requestData.ChatId));
                    });
                })
                .AddHandlerForMessageType(RegisterRequestData.MsgType, async (client, msg, context) =>
                {
                    return await SafeWrapperForHandler(client, msg, context, async (client, msg, context) =>
                    {
                        var requestData = RegisterRequest.Parse(msg);
                        return await _authService.RegisterClient(client, requestData); ;
                    });
                })
                .AddHandlerForMessageType(CloseSessionRequestData.MsgType, async (client, msg, context) =>
                {
                    return await SafeWrapperForHandler(client, msg, context, async (client, msg, context) =>
                    {
                        _authService.CloseSession(client);
                        return _msgService.BuildMessage<EndSessionNotificationMessageBuilder, EndSessionNotificationData>();
                    });
                })
                .AddHandlerForMessageType(AuthenticationRequestData.MsgType, async (client, msg, context) =>
                {
                    return await SafeWrapperForHandler(client, msg, context, async (client, msg, context) =>
                    {
                        var requestData = AuthenticationRequestMessageBuilder.Parse(msg);
                        return await _authService.AuthenticateClient(client, requestData); ;
                    });
                })

                //.AddHandlerForMessageType(DeleteMessageRequestData.MsgType, async (client, msg, context) =>
                //{
                //    return await SafeWrapperForHandler(client, msg, context, async (client, msg, context) =>
                //    {
                //        if (CheckUserAuth(client, out var msgToUser))
                //            return msgToUser;


                //        return null;
                //    });
                //})
                //.AddHandlerForMessageType(EditMessageRequestData.MsgType, async (client, msg, context) =>
                //{
                //    return await SafeWrapperForHandler(client, msg, context, async (client, msg, context) =>
                //    {
                //        if (CheckUserAuth(client, out var msgToUser))
                //            return msgToUser;


                //        return null;
                //    });
                //})
                //.AddHandlerForMessageType(DeleteChatRequestData.MsgType, async (client, msg, context) =>
                //{
                //    return await SafeWrapperForHandler(client, msg, context, async (client, msg, context) =>
                //    {

                //        return null;
                //    });
                //})
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




        private async Task<ClientSession?> SessionFactory(TopClient client, ServiceRegistry context, LogString? logger)
        {
            ClientSession session = new(client, _handlers, context)
            {
                logger = logger,
            };

            _activityService.UpdateLastActive(client);
            session.OnMessageHandled += Session_OnMessageHandled;

            return session;
        }

        private void Session_OnMessageHandled(ClientSession arg1, Message arg2)
        {
            try
            {
                _server?.Logger?.Invoke($"[Server]: Обработано сообщение типа [{arg2.MessageType}] от [{arg1.RemoteEndPoint}] ");
                _activityService.UpdateLastActive(arg1.Client);
            }
            catch { }
        }

        private async Task<Message> SafeWrapperForHandler(TopClient client, Message msg, ServiceRegistry context, Func<TopClient, Message, ServiceRegistry, Task<Message?>> handler)
        {
            try
            {
                return await handler?.Invoke(client, msg, context);
            }
            catch (Exception ex)
            {
                logger?.Invoke($"[Server]: Ошибка обработки {msg.MessageType} от [{client.RemoteEndPoint}].\n{ex.Message}");
                return _msgService.BuildMessage<ErroreMessageBuilder, ErroreData>(builder => builder
                    .SetPayload($"Невозможно обработать {msg.MessageType}.\n{ex.Message}")
                );
            }
        }

        private bool CheckUserNotAuth(TopClient client, out Message? msg)
        {
            if (!_authService.IsAuthClient(client))
            {
                msg = _msgService.BuildMessage<ErroreMessageBuilder, ErroreData>(builder =>
                    builder.SetPayload("Для использования данной операции авторизуйтесь."));

                return true;
            }

            msg = null;
            return false;
        }

        public void SetEndPoint(IPEndPoint endPoint)
            => _server.SetEndPoint(endPoint);

        public async Task StartServer(CancellationToken token = default)
            => await _server.StartAsync(token);

        public async Task StopServer()
            => await _server.StopAsync();
    }
}
