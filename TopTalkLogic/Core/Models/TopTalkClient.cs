
using TopNetwork.Services.MessageBuilder;
using TopTalk.Core.Models.MessageBuilder.Chats;
using TopTalk.Core.Models.MessageBuilder.Contacts;
using TopTalk.Core.Models.MessageBuilder.Messages;
using TopTalk.Core.Storage.Enums;

namespace TopTalkLogic.Core.Models
{
    public class TopTalkClient : BaseClient
    {
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
                .AddHandlerForMessageType(SendMessageResponseData.MsgType, async msg =>
                {

                    return null;
                })
                .AddHandlerForMessageType(DeleteMessageResponseData.MsgType, async msg =>
                {

                    return null;
                })
                .AddHandlerForMessageType(EditMessageResponseData.MsgType, async msg =>
                {

                    return null;
                })
                .AddHandlerForMessageType(CreateChatResponseData.MsgType, async msg =>
                {

                    return null;
                })
                .AddHandlerForMessageType(DeleteMessageResponseData.MsgType, async msg =>
                {

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


        #endregion 
    }
}
