using TopTalk.Core.Storage.Enums;
using TopTalk.Core.Storage.Models;

namespace TopTalk.Core.Services.Builders
{
    /// <summary>
    /// Билдер отвечающий за создание объекта Chat
    /// </summary>
    public class ChatBuilder
    {
        public ChatEntity Build(string name, TypesOfChats typeOfChat, Storage.Models.UserEntity user)
        {
            ChatEntity chat = new ChatEntity()
            {
                Name = name,
                ChatType = typeOfChat
            };
            return chat;
        }
    }
}
