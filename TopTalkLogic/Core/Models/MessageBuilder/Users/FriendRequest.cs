
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

public class FriendRequestData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "FriendRequest";
}

public class FriendRequest : IMessageBuilder<FriendRequestData>
{
    private FriendRequestData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static FriendRequestData Parse(Message msg)
    {
        if (msg.MessageType != FriendRequestData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new FriendRequestData()
        {
            // Add properties initialization if needed
        };
    }
}