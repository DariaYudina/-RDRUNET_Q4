using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.Entity
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public List<Role> Roles { get; set; }

        public User()
        {

        }
        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
