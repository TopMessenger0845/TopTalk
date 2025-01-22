
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

public class FriendRequestNotificationData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "FriendRequestNotification";
}

public class FriendRequestNotification : IMessageBuilder<FriendRequestNotificationData>
{
    private FriendRequestNotificationData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static FriendRequestNotificationData Parse(Message msg)
    {
        if (msg.MessageType != FriendRequestNotificationData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new FriendRequestNotificationData()
        {
            // Add properties initialization if needed
        };
    }
}