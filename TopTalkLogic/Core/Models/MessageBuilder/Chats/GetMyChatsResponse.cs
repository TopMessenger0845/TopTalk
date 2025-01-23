
using Newtonsoft.Json;
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

public class GetMyChatsResponseData : IMsgSourceData
{
    public List<Guid> ChatIds { get; set; } = [];
    public string MessageType => MsgType;
    public static string MsgType => "GetMyChatsResponse";
}

public class GetMyChatsResponse : IMessageBuilder<GetMyChatsResponseData>
{
    private GetMyChatsResponseData _data = new();

    public GetMyChatsResponse SetChatHistory(List<Guid> chatIds)
    {
        _data.ChatIds = chatIds;
        return this;
    }

    public Message BuildMsg()
    {
        return new Message()
        {
            Payload = JsonConvert.SerializeObject(_data.ChatIds),
            MessageType = _data.MessageType,
        };
    }

    public static GetMyChatsResponseData Parse(Message msg)
    {
        if (msg.MessageType != GetMyChatsResponseData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new GetMyChatsResponseData()
        {
            ChatIds = JsonConvert.DeserializeObject<List<Guid>>(msg.Payload)!,
        };
    }
}