
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

namespace TopTalk.Core.Models.MessageBuilder.Chats;

public class SubscriptionResponseData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "SubscriptionResponse";
}

public class SubscriptionResponse : IMessageBuilder<SubscriptionResponseData>
{
    private SubscriptionResponseData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static SubscriptionResponseData Parse(Message msg)
    {
        if (msg.MessageType != SubscriptionResponseData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new SubscriptionResponseData()
        {
            // Add properties initialization if needed
        };
    }
}