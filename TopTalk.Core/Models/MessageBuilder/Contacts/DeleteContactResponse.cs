
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

public class DeleteContactResponseData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "DeleteContactResponse";
}

public class DeleteContactResponse : IMessageBuilder<DeleteContactResponseData>
{
    private DeleteContactResponseData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static DeleteContactResponseData Parse(Message msg)
    {
        if (msg.MessageType != DeleteContactResponseData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new DeleteContactResponseData()
        {
            // Add properties initialization if needed
        };
    }
}