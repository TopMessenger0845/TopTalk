
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

namespace TopTalk.Core.Models.MessageBuilder.Chats;

public class InviteUserRequestData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "InviteUserRequest";
}

public class InviteUserRequest : IMessageBuilder<InviteUserRequestData>
{
    private InviteUserRequestData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static InviteUserRequestData Parse(Message msg)
    {
        if (msg.MessageType != InviteUserRequestData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new InviteUserRequestData()
        {
            // Add properties initialization if needed
        };
    }
}