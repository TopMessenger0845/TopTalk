
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

namespace TopTalk.Core.Models.MessageBuilder.Users;

public class ChangePasswordRequestData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "ChangePasswordRequest";
}

public class ChangePasswordRequest : IMessageBuilder<ChangePasswordRequestData>
{
    private ChangePasswordRequestData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static ChangePasswordRequestData Parse(Message msg)
    {
        if (msg.MessageType != ChangePasswordRequestData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new ChangePasswordRequestData()
        {
            // Add properties initialization if needed
        };
    }
}