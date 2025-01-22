
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

namespace TopTalk.Core.Models.MessageBuilder.Messages;

public class SendMessageRequestData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "SendMessageRequest";
}

public class SendMessageRequest : IMessageBuilder<SendMessageRequestData>
{
    private SendMessageRequestData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static SendMessageRequestData Parse(Message msg)
    {
        if (msg.MessageType != SendMessageRequestData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new SendMessageRequestData()
        {
            // Add properties initialization if needed
        };
    }
}