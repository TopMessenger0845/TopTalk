using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopTalk.Enums;
namespace TopTalk.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ChatTypeId { get; set; }
        public TypesOfChats ChatType { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
