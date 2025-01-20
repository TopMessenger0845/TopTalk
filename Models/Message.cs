using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopTalk.Models
{
    public class Message
    {
        public int Id { get; set; }
        public User MessageFrom { get; set; }
        public int MessageTypeId { get; set; }
        public TypeOfMessage MessageType { get; set; }
        public string Text { get; set; }
        public int ChatId {  get; set; }
        public ChatInfo Chat {  get; set; }
        public DateTime CreationTime { get; set; }
    }
}
