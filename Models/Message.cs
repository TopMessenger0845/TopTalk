using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopTalk.Enums;
namespace TopTalk.Models
{
    public class Message
    {
        public int Id { get; set; }
        public User MessageFrom { get; set; }
        public TypesOfMessages MessageType { get; set; }
        public string Text { get; set; }
        public int ChatId {  get; set; }
        public Chat Chat {  get; set; }
        public DateTime CreationTime { get; set; }
    }
}
