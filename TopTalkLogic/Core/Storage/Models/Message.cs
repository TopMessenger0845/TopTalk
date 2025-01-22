
namespace TopTalk.Core.Storage.Models
{
    public class MessageEntity
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid SenderId { get; set; }
        public UserEntity Sender { get; set; }
        public Guid ChatId { get; set; }
        public ChatEntity Chat { get; set; }
    }
}
