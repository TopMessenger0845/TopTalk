
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

public class CreateChatRequestData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "CreateChatRequest";
}

public class CreateChatRequest : IMessageBuilder<CreateChatRequestData>
{
    private CreateChatRequestData _data = new();

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