
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

namespace TopTalk.Core.Models.MessageBuilder.Contacts;

public class DeleteContactRequestData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "DeleteContactRequest";
}

public class DeleteContactRequest : IMessageBuilder<DeleteContactRequestData>
{
    private DeleteContactRequestData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static DeleteContactRequestData Parse(Message msg)
    {
        if (msg.MessageType != DeleteContactRequestData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new DeleteContactRequestData()
        {
            // Add properties initialization if needed
        };
    }
}