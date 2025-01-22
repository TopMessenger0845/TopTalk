
using TopTalk.Core.Storage.Enums;

namespace TopTalk.Core.Storage.Models
{
    public class Chat
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TypesOfChats ChatType { get; set; }
        public ICollection<Message>? Messages { get; set; }
        public ICollection<UserChat>? UserChats { get; set; }
    }
}
