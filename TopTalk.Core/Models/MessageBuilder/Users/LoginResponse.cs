
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

public class LoginResponseData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "LoginResponse";
}

public class LoginResponse : IMessageBuilder<LoginResponseData>
{
    private LoginResponseData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static LoginResponseData Parse(Message msg)
    {
        if (msg.MessageType != LoginResponseData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new LoginResponseData()
        {
            // Add properties initialization if needed
        };
    }
}