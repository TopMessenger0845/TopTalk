
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

public class FriendRequestAnswerData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "FriendRequestAnswer";
}

public class FriendRequestAnswer : IMessageBuilder<FriendRequestAnswerData>
{
    private FriendRequestAnswerData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static FriendRequestAnswerData Parse(Message msg)
    {
        if (msg.MessageType != FriendRequestAnswerData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new FriendRequestAnswerData()
        {
            // Add properties initialization if needed
        };
    }
}