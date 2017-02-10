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
        user setUser(int id, string login, string pass);
        void deleteUser(int id);
        void deleteUser(string login);

        List<account> getAccounts();
        List<account> getAccounts(int user_id);
        account getAccount(int id);
        account getAccount(int user_id, string name);
        account addAccount(int user_id, string name);
        account addAccount(int user_id, string name, float value);
        account addAccount(account ac);
        account setAccount(int account_id, int user_id, string name, float value);
        void deleteAccount(int id);
        void deleteAccount(int user_id, string name);

        List<category> getCategories();
        List<category> getCategories(int user_id);  
        category getCategory(int id);
        category getCategory(int user_id, string name);
        category addCategory(int user_id, string name);
        category addCategory(category cat);
        category setCategory(int category_id, int user_id, string name);
        void deleteCategory(int id);
        void deleteCategory(int user_id, string name);
    }
}
