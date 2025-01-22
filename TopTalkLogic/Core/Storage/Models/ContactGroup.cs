
namespace TopTalk.Core.Storage.Models
{
    public class ContactGroup
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<User> Contacts { get; set; }
    }
}
