﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopTalk.Enums;

namespace TopTalk.Models
{
    public class UsersAndChats
    {
        public int Id { get; set; }
        public Chat Chat { get; set; }
        public User User { get; set; }
        public TypesOfUsers UserType { get; set; }
    }
}
