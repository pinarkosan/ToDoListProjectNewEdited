using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Business
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string NameSurname { get; set; }
        public string Password { get; set; }

        public int Email { get; set; }
     
    }
}
