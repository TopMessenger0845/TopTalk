
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

namespace TopTalk.Core.Models.MessageBuilder.Messages;

public class SendMessageRequestData : IMsgSourceData
{
    public string Message { get; set; } = string.Empty;
    public Guid ChatId { get; set; } 

    public string MessageType => MsgType;
    public static string MsgType => "SendMessageRequest";
}

public class SendMessageRequest : IMessageBuilder<SendMessageRequestData>
{
    private SendMessageRequestData _data = new();

    public SendMessageRequest SetChatId(Guid chatId)
    {
        _data.ChatId = chatId;
        return this;
    }

    public SendMessageRequest SetMessage(string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message, nameof(message));

        _data.Message = message;
        return this;
    }

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
            Payload = _data.Message,
            Headers =
            {
                {nameof(SendMessageRequestData.ChatId), _data.ChatId.ToString() }
            }
        };
    }

    public static SendMessageRequestData Parse(Message msg)
    {
        if (msg.MessageType != SendMessageRequestData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new SendMessageRequestData()
        {
            Message = msg.Payload,
            ChatId = Guid.Parse(msg.Headers[nameof(SendMessageRequestData.ChatId)])
        };
    }
}