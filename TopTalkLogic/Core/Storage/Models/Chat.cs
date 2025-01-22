
using TopTalk.Core.Storage.Enums;

namespace TopTalk.Core.Storage.Models
{
    public class ChatEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TypesOfChats ChatType { get; set; }
        public ICollection<MessageEntity>? Messages { get; set; }
        public ICollection<UserChatEntity>? UserChats { get; set; }
    }
}
