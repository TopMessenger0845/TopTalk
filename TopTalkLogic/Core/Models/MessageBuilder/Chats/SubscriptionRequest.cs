
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

namespace TopTalk.Core.Models.MessageBuilder.Chats;

public class SubscriptionRequestData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "SubscriptionRequest";
}

public class SubscriptionRequest : IMessageBuilder<SubscriptionRequestData>
{
    private SubscriptionRequestData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static SubscriptionRequestData Parse(Message msg)
    {
        if (msg.MessageType != SubscriptionRequestData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new SubscriptionRequestData()
        {
            // Add properties initialization if needed
        };
    }
}