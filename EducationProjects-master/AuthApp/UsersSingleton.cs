using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp
{
    public class UsersSingleton
    {
        public static UsersSingleton Instance { get; } = new UsersSingleton();
        public List<User> Users { get; set; } = new List<User>();

        public bool Auth(string login, string password)
        {
            return Users.Find(u => u.Login == login && u.Password == password) != null;
        }
        public void Reg(string login, string password) 
        { 
            Users.Add(new User { Login = login, Password = password });
        }
    }

    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
