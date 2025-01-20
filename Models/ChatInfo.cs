using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopTalk.Models
{
    public class ChatInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ChatTypeId { get; set; }
        public TypeOfChat ChatType { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
