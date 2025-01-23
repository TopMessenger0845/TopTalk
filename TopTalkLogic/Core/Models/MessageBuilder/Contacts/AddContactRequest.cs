
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

namespace TopTalk.Core.Models.MessageBuilder.Contacts;

public class AddContactRequestData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "AddContactRequest";
}

public class AddContactRequest : IMessageBuilder<AddContactRequestData>
{
    private AddContactRequestData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static AddContactRequestData Parse(Message msg)
    {
        if (msg.MessageType != AddContactRequestData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new AddContactRequestData()
        {
            // Add properties initialization if needed
        };
    }
}