
namespace TopTalk.Core.Storage.Models
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<MessageEntity>? SentMessages { get; set; }
        public ICollection<UserChatEntity>? UserChats { get; set; }
    }
}
