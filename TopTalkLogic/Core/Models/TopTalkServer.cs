
using System.Net;
using TopNetwork.RequestResponse;
using TopNetwork.Services.MessageBuilder;
using TopNetwork.Services;
using TopTalk.Core.Models.MessageBuilder.Messages;
using TopNetwork.Core;
using TopTalk.Core.Models.MessageBuilder.Chats;
using TopTalk.Core.Models.MessageBuilder.Contacts;

namespace TopTalkLogic.Core.Models
{
    public class TopTalkServer
    {
        // Регистрация всех фабрик для типов сообщений отправляемых сервером 
        private static readonly MessageBuilderService _msgService = new MessageBuilderService()
                    .Register(() => new ErroreMessageBuilder())
                    .Register(() => new EndSessionNotificationMessageBuilder());

        private readonly TrackerUserActivityService _activityService;
        private readonly RrServerHandlerBase _handlers;
        private RrServer _server = new();

        public EndPoint? EndPoint => _server.CurrentEndPoint;
        public bool IsRunning => _server.IsRunning;
        public int CountOpenSessions => _server.CountOpenSessions;


        public TopTalkServer(string? userFilePath = null)
        {
            //_server.Logger = Logger.LogString;

            _activityService = new(_msgService);

            _server
                .RegisterService(_msgService)
                .RegisterService(_activityService);


            _handlers = new RrServerHandlerBase()
                .AddHandlerForMessageType(SendMessageRequestData.MsgType, async (client, msg, context) =>
                {
                    return await SafeWrapperForHandler(client, msg, context, async (client, msg, context) =>
                    {
                        
                        return null;
                    });
                })
                .AddHandlerForMessageType(DeleteMessageRequestData.MsgType, async (client, msg, context) =>
                {
                    return await SafeWrapperForHandler(client, msg, context, async (client, msg, context) =>
                    {

                        return null;
                    });
                })
                .AddHandlerForMessageType(EditMessageRequestData.MsgType, async (client, msg, context) =>
                {
                    return await SafeWrapperForHandler(client, msg, context, async (client, msg, context) =>
                    {

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
    }
}
