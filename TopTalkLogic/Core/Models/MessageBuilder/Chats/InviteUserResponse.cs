
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

namespace TopTalk.Core.Models.MessageBuilder.Chats;

public class InviteUserResponseData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "InviteUserResponse";
}

public class InviteUserResponse : IMessageBuilder<InviteUserResponseData>
{
    private InviteUserResponseData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static InviteUserResponseData Parse(Message msg)
    {
        if (msg.MessageType != InviteUserResponseData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new InviteUserResponseData()
        {
            // Add properties initialization if needed
        };
    }
}