
using Newtonsoft.Json;
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;
using TopTalk.Core.Storage.Models;

public class ChatUpdateNotificationData : IMsgSourceData
{
    public List<MessageEntity> UpdatedChatHistory { get; set; } = []; 
    public Guid ChatIdUpdated { get; set; }

    public string MessageType => MsgType;
    public static string MsgType => "ChatUpdateNotification";
}

public class ChatUpdateNotification : IMessageBuilder<ChatUpdateNotificationData>
{
    private ChatUpdateNotificationData _data = new();

    public ChatUpdateNotification SetChatHistory(List<MessageEntity> chatHistory)
    {
        _data.UpdatedChatHistory = chatHistory;
        return this;
    }

    public ChatUpdateNotification SetChatId(Guid id)
    {
        _data.ChatIdUpdated = id;
        return this;
    }

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
            Headers =
            {
                {nameof(ChatUpdateNotificationData.ChatIdUpdated), _data.ChatIdUpdated.ToString() }
            },
            Payload = JsonConvert.SerializeObject(_data.UpdatedChatHistory)
        };
    }

    public static ChatUpdateNotificationData Parse(Message msg)
    {
        if (msg.MessageType != ChatUpdateNotificationData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new ChatUpdateNotificationData()
        {
            UpdatedChatHistory = JsonConvert.DeserializeObject<List<MessageEntity>>(msg.Payload)!,
            ChatIdUpdated = Guid.Parse(msg.Headers[nameof(ChatUpdateNotificationData.ChatIdUpdated)])
        };
    }
}