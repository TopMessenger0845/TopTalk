
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

public class LoginRequestData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "LoginRequest";
}

public class LoginRequest : IMessageBuilder<LoginRequestData>
{
    private LoginRequestData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static LoginRequestData Parse(Message msg)
    {
        if (msg.MessageType != LoginRequestData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new LoginRequestData()
        {
            // Add properties initialization if needed
        };
    }
}