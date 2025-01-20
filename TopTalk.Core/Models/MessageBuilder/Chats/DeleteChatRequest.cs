
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

public class DeleteChatRequestData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "DeleteChatRequest";
}

public class DeleteChatRequest : IMessageBuilder<DeleteChatRequestData>
{
    private DeleteChatRequestData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static DeleteChatRequestData Parse(Message msg)
    {
        if (msg.MessageType != DeleteChatRequestData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new DeleteChatRequestData()
        {
            // Add properties initialization if needed
        };
    }
}