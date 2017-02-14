using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAccount.Models
{
    public interface IDal : IDisposable
    {
        #region User
        List<User> getUsers();  
        User getUser(string login);
        User getUser(int id);
        User authentication(string login, string password);
        User addUser(string login_value, string pass_value);
        User addUser(User usr);
        User setUser(int id, string login, string pass);
        void deleteUser(int id);
        void deleteUser(string login);
        #endregion

        #region Account
        List<Account> getAccounts();
        List<Account> getAccounts(int user_id);
        Account getAccount(int id);
        Account getAccount(int user_id, string name);
        Account addAccount(int user_id, string name);
        Account addAccount(int user_id, string name, float value);
        Account addAccount(Account ac);
        Account setAccount(int account_id, int user_id, string name, float value);
        void deleteAccount(int id);
        void deleteAccount(int user_id, string name);
        #endregion

        #region Category
        List<Category> getCategories();
        List<Category> getCategories(int user_id);  
        Category getCategory(int id);
        Category getCategory(int user_id, string name);
        Category addCategory(int user_id, string name);
        Category addCategory(Category cat);
        Category setCategory(int category_id, int user_id, string name);
        void deleteCategory(int id);
        void deleteCategory(int user_id, string name);
        #endregion

        #region Transaction
        List<Transaction> getTransactions(Dal.TransacFilter filter);
        List<Transaction> getTransactions(int account_id, Dal.TransacFilter filter);
        List<Transaction> getTransactions(int account_id, DateTime date, Dal.TransacFilter filter);
        List<Transaction> getTransactions(int account_id, string name, Dal.TransacFilter filter);
        Transaction getTransaction(int id);
        Transaction addTransaction(int account_id, string name, float value, bool validated, DateTime date);
        Transaction addTransaction(Transaction t);
        Transaction setTransaction(int transaction_id, int account_id, string name, float value, bool validated, DateTime date);
        void deleteTransaction(int id);
        #endregion
    }
}
