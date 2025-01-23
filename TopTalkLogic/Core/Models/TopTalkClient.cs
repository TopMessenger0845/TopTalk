
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;
using TopTalk.Core.Models.MessageBuilder.Chats;
using TopTalk.Core.Models.MessageBuilder.Messages;
using TopTalk.Core.Models.MessageBuilder.Users;
using TopTalk.Core.Storage.Enums;
using TopTalkLogic.Core.Models.MessageBuilder.Chats;

namespace TopTalkLogic.Core.Models
{
    public class TopTalkClient : BaseClient
    {
        public event Action<CreateChatResponseData>? OnCreatedChat;
        public event Action<ChatUpdateNotificationData>? OnChatUpdated;
        public event Action<AuthenticationResponseData>? OnAuthentication;
        public event Action<RegisterResponseData>? OnRegister;
        public event Action<EndSessionNotificationData>? OnEndSession;
        public event Action<GetMyChatsResponseData>? OnGetMyChats;
        public TopTalkClient() : base() { }
        protected override void RegisterMessageBuilders()
        {
            MessageBuilderService
                .Register(() => new DeleteMessageRequest())                     // Todo: Удаление сообщения
                .Register(() => new EditMessageRequest())                       // Todo: Редактирование сообщения
                .Register(() => new DeleteChatRequest())                        // Todo: Удаление чата 
                .Register(() => new SubscriptionRequest())                      // Todo: Подписка на канал
                .Register(() => new InviteUserRequest())                        // Todo: Пригласить пользователя в чат
                .Register(() => new SendMessageRequest())                       // Отправка сообщения
                .Register(() => new CreateChatRequest())                        // Создание чата 
                .Register(() => new RegisterRequest())                          // Запрос на регистрацию
                .Register(() => new AuthenticationRequestMessageBuilder())      // Запрос на авторизацию
                .Register(() => new ChatHistoryRequest())                       // Запрос на получении истории чата
                .Register(() => new CloseSessionRequestMessageBuilder())        // Закрыть сессию
                .Register(() => new GetMyChatsRequest());                       // Запрос на получения Id чатов, в которых пользователь что то писал
        }

        protected override async void RegisterMessageHandlers()
        {
            Handlers
                .AddHandlerForMessageType(ChatUpdateNotificationData.MsgType, async msg =>
                {
                    return await SafeWrapperForHandler(msg, async msg =>
                        OnChatUpdated?.Invoke(ChatUpdateNotification.Parse(msg)));
                })
                .AddHandlerForMessageType(RegisterResponseData.MsgType, async msg =>
                {
                    return await SafeWrapperForHandler(msg, async msg =>
                        OnRegister?.Invoke(RegisterResponse.Parse(msg)));
                })
                .AddHandlerForMessageType(AuthenticationResponseData.MsgType, async msg =>
                {
                    return await SafeWrapperForHandler(msg, async msg => 
                        OnAuthentication?.Invoke(AuthenticationResponseMessageBuilder.Parse(msg)));
                })
                .AddHandlerForMessageType(ErroreData.MsgType, async msg =>
                {
                    return await SafeWrapperForHandler(msg, async msg => 
                        InvokeOnErroreFromServer(ErroreMessageBuilder.Parse(msg)));
                })
                .AddHandlerForMessageType(EndSessionNotificationData.MsgType, async msg =>
                {
                    Disconnect();
                    return await SafeWrapperForHandler(msg, async msg => 
                        OnEndSession?.Invoke(EndSessionNotificationMessageBuilder.Parse(msg)));
                })
                .AddHandlerForMessageType(GetMyChatsResponseData.MsgType, async msg =>
                {
                    return await SafeWrapperForHandler(msg, async msg =>
                        OnGetMyChats?.Invoke(GetMyChatsResponse.Parse(msg)));
                })

                //.AddHandlerForMessageType(DeleteMessageResponseData.MsgType, async msg =>
                //{
                //    return null;
                //})
                //.AddHandlerForMessageType(EditMessageResponseData.MsgType, async msg =>
                //{
                //    return null;
                //})
                .AddHandlerForMessageType(CreateChatResponseData.MsgType, async msg =>
                {
                    try { OnCreatedChat?.Invoke(CreateChatResponse.Parse(msg)); }
                    catch(Exception ex) { InvokeOnErroreOnClient(ex.Message); }
                    return null;
                })
                .AddHandlerForMessageType(SubscriptionResponseData.MsgType, async msg =>
                {

                    return null;
                })
                .AddHandlerForMessageType(InviteUserNotificationData.MsgType, async msg =>
                {

                    return null;
                });
        }

        #region Client Api

        public async Task SendMessage(Guid chatId, string msg)
        {
            await SendMessageAsync<SendMessageRequest, SendMessageRequestData>(builder => builder
                .SetChatId(chatId)
                .SetMessage(msg)
            );
        }

        public async Task CreateChat(string name, TypesOfChats chatType)
        {
            await SendMessageAsync<CreateChatRequest, CreateChatRequestData>(builder => builder
                .SetChatName(name)
                .SetChatType(chatType)
            );
        }

        public async Task Register(string login, string password)
        {
            await SendMessageAsync<RegisterRequest, RegisterRequestData>(builder => builder
                .SetLogin(login)
                .SetPassword(password)
            );
        }

        public async Task Login(string login, string password)
        {
            await SendMessageAsync<AuthenticationRequestMessageBuilder, AuthenticationRequestData>(builder => builder
                .SetLogin(login)
                .SetPassword(password)
            );
        }

        public async Task SendCloseSessionRequest()
        {
            await SendMessageAsync<CloseSessionRequestMessageBuilder, CloseSessionRequestData>();
        }

        public async Task GetChatHistory(Guid chatId)
        {
            await SendMessageAsync<ChatHistoryRequest, ChatHistoryRequestData>(builder => builder.SetChatId(chatId)); 
        }

        public async Task GetMyChats()
        {
            await SendMessageAsync<GetMyChatsRequest, GetMyChatsRequestData>();
        }

        //public async Task DeleteMessage(Guid msgId)
        //{
        //    await SendMessageAsync<DeleteMessageRequest, DeleteMessageRequestData>(builder => builder
        //        .SetMessageId(msgId)
        //    );
        //}
        //public async Task EditMessage(Guid messageId, string newMessage)
        //{
        //    await SendMessageAsync<EditMessageRequest, EditMessageRequestData>(builder => builder
        //        .SetNewMessage(newMessage)
        //        .SetMessageId(messageId)
        //    );
        //}
        //public async Task DeleteChat(Guid chatId)
        //{
        //    await SendMessageAsync<DeleteChatRequest, DeleteChatRequestData>(builder => builder
        //        .SetChatId(chatId)
        //    );
        //}
        #endregion


        private async Task<Message?> SafeWrapperForHandler(Message msg, Func<Message, Task> handler)
        {
            try
            {
                await handler?.Invoke(msg)!;
            }
            catch (Exception ex)
            {
                InvokeOnErroreOnClient($"Error parsing response of type: {msg.MessageType} - {ex.Message}");
            }

            return null;
        }
    }
}
