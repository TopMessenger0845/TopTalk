
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

public class RegisterResponseData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "RegisterResponse";
}

public class RegisterResponse : IMessageBuilder<RegisterResponseData>
{
    private RegisterResponseData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static RegisterResponseData Parse(Message msg)
    {
        if (msg.MessageType != RegisterResponseData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new RegisterResponseData()
        {
            // Add properties initialization if needed
        };
    }
}