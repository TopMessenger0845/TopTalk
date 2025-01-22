using TopTalk.Core.Storage.Enums;
using TopTalk.Core.Storage.Models;

namespace TopTalk.Core.Services.Builders
{
    public class ChatBuilderService
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
