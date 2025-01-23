
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

namespace TopTalk.Core.Models.MessageBuilder.Chats;

/// <summary>
/// ����������� ������������ ��� ��� ���������� �������� � ���, ����� <see cref="InviteUserRequest"/>, ����� ��� ������������ ������ �������� ����� <see cref="AnswerOfInvitation"/>
/// </summary>
public class InviteUserNotificationData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "InviteUserNotification";
}

public class InviteUserNotification : IMessageBuilder<InviteUserNotificationData>
{
    private InviteUserNotificationData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static InviteUserNotificationData Parse(Message msg)
    {
        if (msg.MessageType != InviteUserNotificationData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new InviteUserNotificationData()
        {
            // Add properties initialization if needed
        };
    }
}