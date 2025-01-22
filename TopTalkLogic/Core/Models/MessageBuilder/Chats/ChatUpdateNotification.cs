
using Newtonsoft.Json;
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;
using TopTalk.Core.Storage.Models;

public class ChatUpdateNotificationData : IMsgSourceData
{
    public List<MessageEntity> UpdatedChatHistory { get; set; } = []; 
    public Guid ChatIdUpdated => UpdatedChatHistory[0].ChatId;

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

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
            Payload = JsonConvert.SerializeObject(_data.UpdatedChatHistory)
        };
    }

    public static ChatUpdateNotificationData Parse(Message msg)
    {
        if (msg.MessageType != ChatUpdateNotificationData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new ChatUpdateNotificationData()
        {
            UpdatedChatHistory = JsonConvert.DeserializeObject<List<MessageEntity>>(msg.Payload)!
        };
    }
}