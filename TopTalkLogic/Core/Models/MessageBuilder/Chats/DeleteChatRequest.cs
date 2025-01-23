
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

namespace TopTalk.Core.Models.MessageBuilder.Chats;

public class DeleteChatRequestData : IMsgSourceData
{
    public Guid ChatId { get; set; }
    public string MessageType => MsgType;
    public static string MsgType => "DeleteChatRequest";
}

public class DeleteChatRequest : IMessageBuilder<DeleteChatRequestData>
{
    private DeleteChatRequestData _data = new();
    public DeleteChatRequest SetChatId(Guid chatId)
    {
        _data.ChatId = chatId;
        return this;    
    }
    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static DeleteChatRequestData Parse(Message msg)
    {
        if (msg.MessageType != DeleteChatRequestData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new DeleteChatRequestData()
        {
            // Add properties initialization if needed
        };
    }
}