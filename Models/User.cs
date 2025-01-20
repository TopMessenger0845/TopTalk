using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopTalk.Enums;
namespace TopTalk.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public int PasswordHash { get; set; }
        public int UserTypeId { get; set; }
        public ICollection<Chat> Chats { get; set; }
        public TypesOfUsers UserType {  get; set; }
    }
}
