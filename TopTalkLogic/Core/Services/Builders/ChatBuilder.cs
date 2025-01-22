using TopTalk.Core.Storage.Enums;
using TopTalk.Core.Storage.Models;

namespace TopTalk.Core.Services.Builders
{
    /// <summary>
    /// Билдер отвечающий за создание объекта Chat
    /// </summary>
    public class ChatBuilder
    {
        public Chat Build(string name, TypesOfChats typeOfChat, Storage.Models.User user)
        {
            Chat chat = new Chat()
            {
                Name = name,
                ChatType = typeOfChat
            };
            return chat;
        }
    }
}
