
namespace TopTalk.Core.Storage.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<ContactGroup>? ContactGroups { get; set; }
        public ICollection<Message>? SentMessages { get; set; }
        public ICollection<UserChat>? UserChats { get; set; }
    }
}
