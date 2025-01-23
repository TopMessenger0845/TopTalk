
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

public class GetMyChatsRequestData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "GetMyChatsRequest";
}

public class GetMyChatsRequest : IMessageBuilder<GetMyChatsRequestData>
{
    private GetMyChatsRequestData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static GetMyChatsRequestData Parse(Message msg)
    {
        if (msg.MessageType != GetMyChatsRequestData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new GetMyChatsRequestData()
        {
            // Add properties initialization if needed
        };
    }
}