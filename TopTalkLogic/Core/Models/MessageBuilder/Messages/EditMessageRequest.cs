
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

namespace TopTalk.Core.Models.MessageBuilder.Messages;

public class EditMessageRequestData : IMsgSourceData
{
    public Guid MessageId { get; set; }
    public string NewMessage { get; set; } = string.Empty;

    public string MessageType => MsgType;
    public static string MsgType => "EditMessageRequest";
}

public class EditMessageRequest : IMessageBuilder<EditMessageRequestData>
{
    private EditMessageRequestData _data = new();

    public EditMessageRequest SetMessageId(Guid id)
    {
        _data.MessageId = id;
        return this;
    }

    public EditMessageRequest SetNewMessage(string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message, nameof(message));

        _data.NewMessage = message;
        return this;
    }

    public Message BuildMsg()
    {
        return new Message()
        {
            Payload = _data.NewMessage,
            MessageType = _data.MessageType,
            Headers =
            {
                {nameof(EditMessageRequestData.MessageId), _data.MessageId.ToString() }
            },
        };
    }

    public static EditMessageRequestData Parse(Message msg)
    {
        if (msg.MessageType != EditMessageRequestData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new EditMessageRequestData()
        {
            NewMessage = msg.Payload,
            MessageId = Guid.Parse(msg.Headers[nameof(EditMessageRequestData.MessageId)])
        };
    }
}