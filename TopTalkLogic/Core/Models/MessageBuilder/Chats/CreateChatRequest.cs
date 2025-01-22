
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;
using TopTalk.Core.Storage.Enums;

namespace TopTalk.Core.Models.MessageBuilder.Chats;

public class CreateChatRequestData : IMsgSourceData
{
    public string ChatName { get; set; } = string.Empty;
    public TypesOfChats ChatType { get; set; }
    public string MessageType => MsgType;
    public static string MsgType => "CreateChatRequest";
}

public class CreateChatRequest : IMessageBuilder<CreateChatRequestData>
{
    private CreateChatRequestData _data = new();
    public CreateChatRequest SetChatName(string name)
    {
        _data.ChatName = name;
        return this;
    }
    public CreateChatRequest SetChatType(TypesOfChats type)
    {
        _data.ChatType = type;
        return this;
    }
    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static CreateChatRequestData Parse(Message msg)
    {
        if (msg.MessageType != CreateChatRequestData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new CreateChatRequestData()
        {
            // Add properties initialization if needed
        };
    }
}