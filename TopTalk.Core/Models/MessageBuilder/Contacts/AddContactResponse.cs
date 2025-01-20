
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

public class AddContactResponseData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "AddContactResponse";
}

public class AddContactResponse : IMessageBuilder<AddContactResponseData>
{
    private AddContactResponseData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static AddContactResponseData Parse(Message msg)
    {
        if (msg.MessageType != AddContactResponseData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new AddContactResponseData()
        {
            // Add properties initialization if needed
        };
    }
}