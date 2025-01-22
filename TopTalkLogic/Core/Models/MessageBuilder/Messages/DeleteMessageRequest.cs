using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;
using TopTalk.Core.Storage.Models;

namespace TopTalk.Core.Models.MessageBuilder.Messages;

public class DeleteMessageRequestData : IMsgSourceData
{
    public Guid MessageId { get; set; }
    public string MessageType => MsgType;
    public static string MsgType => "DeleteMessageRequest";
}
public class DeleteMessageRequest : IMessageBuilder<DeleteMessageRequestData>
{
    private DeleteMessageRequestData _data = new();
    public DeleteMessageRequest SetMessageId(Guid msgId)
    {
        _data.MessageId = msgId;
        return this;
    }
    public TopNetwork.Core.Message BuildMsg()
    {
        return new TopNetwork.Core.Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static DeleteMessageRequestData Parse(TopNetwork.Core.Message msg)
    {
        if (msg.MessageType != DeleteMessageRequestData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new DeleteMessageRequestData()
        {
            // Add properties initialization if needed
        };
    }
}