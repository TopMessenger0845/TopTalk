
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

public class AnswerOfInvitationData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "AnswerOfInvitation";
}

public class AnswerOfInvitation : IMessageBuilder<AnswerOfInvitationData>
{
    private AnswerOfInvitationData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static AnswerOfInvitationData Parse(Message msg)
    {
        if (msg.MessageType != AnswerOfInvitationData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new AnswerOfInvitationData()
        {
            // Add properties initialization if needed
        };
    }
}