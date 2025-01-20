using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopTalk.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int UserTypeId { get; set; }
        public ICollection<ChatInfo> Chats { get; set; }
        public TypeOfUser UserType {  get; set; }
    }
}
