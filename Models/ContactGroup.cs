using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopTalk.Models
{
    public class ContactGroup
    {
        public int Id { get; set; }
        public ICollection<User> Contacts { get; set; }
    }
}
