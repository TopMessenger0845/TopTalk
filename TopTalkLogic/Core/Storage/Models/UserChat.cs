
using TopTalk.Core.Storage.Enums;

namespace TopTalk.Core.Storage.Models
{
    public class UserChat
    {
        public Guid Id { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public TypesOfUsers UserType { get; set; }
    }
}
