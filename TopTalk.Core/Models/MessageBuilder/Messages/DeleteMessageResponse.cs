
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

public class DeleteMessageResponseData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "DeleteMessageResponse"; 
}

public class DeleteMessageResponse : IMessageBuilder<DeleteMessageResponseData>
{
    private DeleteMessageResponseData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static DeleteMessageResponseData Parse(Message msg)
    {
        if (msg.MessageType != DeleteMessageResponseData.MsgType) 
            throw new InvalidOperationException("Incorrect message type.");

        return new DeleteMessageResponseData()
        {
            // Add properties initialization if needed
        };
    }
}