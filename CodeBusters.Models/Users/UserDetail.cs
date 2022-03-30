using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeBusters.Models.Users
{
    public class UserDetail
    {
        public int Id {get; set;}
        public string FullName {get; set; }
        public string Email {get; set;}
        public bool isAdmin {get; set;}
    }
}