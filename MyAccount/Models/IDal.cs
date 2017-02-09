using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAccount.Models
{
    public interface IDal : IDisposable
    {
        List<user> getUsers();  
        user getUser(string login);
        user getUser(int id);
        user authentication(string login, string password);
        user addUser(string login_value, string pass_value);
        user addUser(user usr);
        void setUser(int id, string login, string pass);
        void deleteUser(int id);
        void deleteUser(string login);
    }
}
