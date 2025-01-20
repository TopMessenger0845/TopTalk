
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

public class EditMessageResponseData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "EditMessageResponse";
}

public class EditMessageResponse : IMessageBuilder<EditMessageResponseData>
{
    private EditMessageResponseData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static EditMessageResponseData Parse(Message msg)
    {
        if (msg.MessageType != EditMessageResponseData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new EditMessageResponseData()
        {
            // Add properties initialization if needed
        };
    }
}