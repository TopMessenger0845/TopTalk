
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

namespace TopTalk.Core.Models.MessageBuilder.Messages;

public class EditMessageRequestData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "EditMessageRequest";
}

public class EditMessageRequest : IMessageBuilder<EditMessageRequestData>
{
    private EditMessageRequestData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static EditMessageRequestData Parse(Message msg)
    {
        if (msg.MessageType != EditMessageRequestData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new EditMessageRequestData()
        {
            // Add properties initialization if needed
        };
    }
}