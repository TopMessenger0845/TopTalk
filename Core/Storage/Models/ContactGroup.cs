
namespace TopTalk.Core.Storage.Models
{
    public class ContactGroup
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<User> Contacts { get; set; }
    }
}
