
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

public class CreateChatResponseData : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "CreateChatResponse";
}

public class CreateChatResponse : IMessageBuilder<CreateChatResponseData>
{
    private CreateChatResponseData _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static CreateChatResponseData Parse(Message msg)
    {
        if (msg.MessageType != CreateChatResponseData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new CreateChatResponseData()
        {
            // Add properties initialization if needed
        };
    }
}