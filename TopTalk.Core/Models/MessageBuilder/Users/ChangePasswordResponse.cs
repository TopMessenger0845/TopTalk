
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

public class ChangePasswordResponseData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "ChangePasswordResponse";
}

public class ChangePasswordResponse : IMessageBuilder<ChangePasswordResponseData>
{
    private ChangePasswordResponseData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static ChangePasswordResponseData Parse(Message msg)
    {
        if (msg.MessageType != ChangePasswordResponseData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new ChangePasswordResponseData()
        {
            // Add properties initialization if needed
        };
    }
}