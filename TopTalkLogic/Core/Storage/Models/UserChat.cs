
using TopTalk.Core.Storage.Enums;

namespace TopTalk.Core.Storage.Models
{
    public class UserChatEntity
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public ChatEntity Chat { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        public TypesOfUsers UserType { get; set; }
    }
}
