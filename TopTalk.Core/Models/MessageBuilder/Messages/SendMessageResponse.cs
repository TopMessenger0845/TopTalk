
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

public class SendMessageResponseData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "SendMessageResponse";
}

public class SendMessageResponse : IMessageBuilder<SendMessageResponseData>
{
    private SendMessageResponseData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static SendMessageResponseData Parse(Message msg)
    {
        if (msg.MessageType != SendMessageResponseData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new SendMessageResponseData()
        {
            // Add properties initialization if needed
        };
    }
}