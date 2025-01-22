﻿
using TopNetwork.Services.MessageBuilder;
using TopTalk.Core.Models.MessageBuilder.Chats;
using TopTalk.Core.Models.MessageBuilder.Contacts;
using TopTalk.Core.Models.MessageBuilder.Messages;

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
                .Register(() => new InviteUserRequest())
                .Register(() => new AddContactRequest())
                .Register(() => new DeleteContactRequest());
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
                })
                .AddHandlerForMessageType(AddContactResponseData.MsgType, async msg =>
                {

                    return null;
                })
                .AddHandlerForMessageType(DeleteContactResponseData.MsgType, async msg =>
                {

                    return null;
                });
        }
    }
}
