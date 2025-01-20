
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

public class DeleteMessageRequestData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "DeleteMessageRequest";
}

public class DeleteMessageRequest : IMessageBuilder<DeleteMessageRequestData>
{
    private DeleteMessageRequestData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static DeleteMessageRequestData Parse(Message msg)
    {
        if (msg.MessageType != DeleteMessageRequestData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new DeleteMessageRequestData()
        {
            // Add properties initialization if needed
        };
    }
}