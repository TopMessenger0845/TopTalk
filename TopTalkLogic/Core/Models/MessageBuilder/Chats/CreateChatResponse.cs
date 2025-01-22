
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

namespace TopTalk.Core.Models.MessageBuilder.Chats;

public class CreateChatResponseData : IMsgSourceData
{
    public Guid CreatedChatId { get; set; }

    public string MessageType => MsgType;
    public static string MsgType => "CreateChatResponse";
}

public class CreateChatResponse : IMessageBuilder<CreateChatResponseData>
{
    private CreateChatResponseData _data = new();

    public CreateChatResponse SetChatId(Guid chatId)
    {
        _data.CreatedChatId = chatId;
        return this;
    }

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
            Payload = _data.CreatedChatId.ToString()
        };
    }

    public static CreateChatResponseData Parse(Message msg)
    {
        if (msg.MessageType != CreateChatResponseData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new CreateChatResponseData()
        {
            CreatedChatId = Guid.Parse(msg.Payload)
        };
    }
}