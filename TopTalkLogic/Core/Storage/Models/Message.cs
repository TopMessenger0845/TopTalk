
namespace TopTalk.Core.Storage.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid SenderId { get; set; }
        public User Sender { get; set; }
        public Guid ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
