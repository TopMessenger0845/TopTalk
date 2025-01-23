
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

namespace TopTalkLogic.Core.Models.MessageBuilder.Chats
{
    public class ChatHistoryRequestData : IMsgSourceData
    {
        public Guid ChatId { get; set; }

        public string MessageType => MsgType;
        public static string MsgType => "ChatHistoryRequest";
    }

    public class ChatHistoryRequest : IMessageBuilder<ChatHistoryRequestData>
    { 
        private ChatHistoryRequestData _data = new();

        public ChatHistoryRequest SetChatId(Guid chatid)
        {
            _data.ChatId = chatid;
            return this;
        }
        public Message BuildMsg()
        {
            return new Message()
            {
                MessageType = _data.MessageType,
                Payload = _data.ChatId.ToString()
            };
        }

        public static ChatHistoryRequestData Parse(Message msg)
        {
            if (msg.MessageType != ChatHistoryRequestData.MsgType)
                throw new InvalidOperationException("Incorrect message type.");

            return new ChatHistoryRequestData()
            {
                ChatId = Guid.Parse(msg.Payload)
            };
        }
    }
}
