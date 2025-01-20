
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

public class RegisterRequestData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "RegisterRequest";
}

public class RegisterRequest : IMessageBuilder<RegisterRequestData>
{
    private RegisterRequestData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static RegisterRequestData Parse(Message msg)
    {
        if (msg.MessageType != RegisterRequestData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new RegisterRequestData()
        {
            // Add properties initialization if needed
        };
    }
}