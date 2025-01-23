
using TopNetwork.Services.MessageBuilder;
using TopTalk.Core.Models.MessageBuilder.Chats;
using TopTalk.Core.Models.MessageBuilder.Messages;
using TopTalk.Core.Storage.Enums;

namespace TopTalkLogic.Core.Models
{
    public class TopTalkClient : BaseClient
    {
        public event Action<CreateChatResponseData>? OnCreatedChat;
        public event Action<ChatUpdateNotificationData>? OnChatUpdated;

        protected override void RegisterMessageBuilders()
        {
            MessageBuilderService
                .Register(() => new SendMessageRequest())
                .Register(() => new DeleteMessageRequest())
                .Register(() => new EditMessageRequest())
                .Register(() => new CreateChatRequest())
                .Register(() => new DeleteChatRequest())
                .Register(() => new SubscriptionRequest())
                .Register(() => new InviteUserRequest());
        }

        protected override void RegisterMessageHandlers()
        {
            Handlers
                .AddHandlerForMessageType(ChatUpdateNotificationData.MsgType, async msg =>
                {
                    try { OnChatUpdated?.Invoke(ChatUpdateNotification.Parse(msg)); }
                    catch (Exception ex) { InvokeOnErroreOnClient(ex.Message); }
                    return null;
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
            await SendMessageAsync<AuthenticationRequestMessageBuilder, AuthenticationRequestData>(builder => builder
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
    }
}
