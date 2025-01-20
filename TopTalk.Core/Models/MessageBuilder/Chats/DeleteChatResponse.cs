
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

public class DeleteChatResponseData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "DeleteChatResponse";
}

public class DeleteChatResponse : IMessageBuilder<DeleteChatResponseData>
{
    private DeleteChatResponseData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static DeleteChatResponseData Parse(Message msg)
    {
        if (msg.MessageType != DeleteChatResponseData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new DeleteChatResponseData()
        {
            // Add properties initialization if needed
        };
    }
}