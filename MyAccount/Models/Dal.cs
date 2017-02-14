using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace MyAccount.Models
{
    public class Dal : IDal
    {
        private Entities bdd;

        public enum TransacFilter
        {
            ALL,
            VALIDATED,
            INVALIDATED
        }

        public Dal()
        {
            bdd = new Entities();
        }

        public void Dispose()
        {
            bdd.Dispose();
        }

        #region User
        public List<User> getUsers()
        {
            return bdd.User.ToList();
        }

        public User getUser(string login)
        {
            return bdd.User.FirstOrDefault(u => u.login == login);
        }

        public User getUser (int id)
        {
            return bdd.User.Find(id);
        }

        public User authentication (string login, string password)
        {
            return bdd.User.FirstOrDefault(u => u.login == login && u.password == password);
        }

        public User addUser(string login_value, string pass_value)
        {
            try
            {
                User usr = bdd.User.Add(new User { login = login_value, password = pass_value });
                bdd.SaveChanges();
                return usr;
            } catch (DbUpdateException e)
            {
                return null;
            }
        }

        public User addUser(User usr)
        {
            try
            {
                User u = bdd.User.Add(new User { login = usr.login, password = usr.password });
                bdd.SaveChanges();
                return u;
            } catch (DbUpdateException e)
            {
                return null;
            }
        }

        public User setUser(int id, string login, string pass)
        {
            User usr = bdd.User.Find(id);
            if (usr != null)
            {
                usr.login = login;
                usr.password = pass;
                bdd.SaveChanges();
            }
            return usr;
        }

        public void deleteUser(int id)
        {
            User usr = bdd.User.Find(id);
            if (usr != null)
            {
                bdd.User.Remove(usr);
                bdd.SaveChanges();
            }
        }

        public void deleteUser(string login)
        {
            User usr = bdd.User.FirstOrDefault(u => u.login == login);
            if (usr != null)
            {
                bdd.User.Remove(usr);
                bdd.SaveChanges();
            }
        }
        #endregion

        #region Account
        public List<Account> getAccounts()
        {
            return bdd.Account.ToList();
        }

        public List<Account> getAccounts(int user_id)
        {
            return bdd.Account.Where(a => a.user_id == user_id).ToList();
        }

        public Account getAccount(int id)
        {
            return bdd.Account.FirstOrDefault(a => a.id == id);
        }

        public Account getAccount(int user_id, string name)
        {
            return bdd.Account.FirstOrDefault(a => a.name == name && a.user_id == user_id);
        }

        public Account addAccount(int user_id, string name)
        {
            Account new_Account = new Account { name = name, user_id = user_id, value = 0 };
            bdd.Account.Add(new_Account);
            bdd.SaveChanges();
            return new_Account;
        }

        public Account addAccount(int user_id, string name, float value)
        {
            Account new_Account = new Account { name = name, user_id = user_id, value = value };
            bdd.Account.Add(new_Account);
            bdd.SaveChanges();
            return new_Account;
        }

        public Account addAccount(Account ac)
        {
            Account new_Account = bdd.Account.Add(new Account { name = ac.name, user_id = ac.user_id, value = ac.value });
            bdd.SaveChanges();
            return new_Account;
        }

        public Account setAccount(int Account_id, int user_id, string name, float value)
        {
            Account ac = bdd.Account.FirstOrDefault(a => a.id == Account_id);
            if (ac != null)
            {
                ac.user_id = user_id;
                ac.name = name;
                ac.value = value;
                bdd.SaveChanges();
            }
            return ac;
        }

        public void deleteAccount (int id)
        {
            Account ac = bdd.Account.FirstOrDefault(a => a.id == id);
            if (ac != null)
            {
                bdd.Account.Remove(ac);
                bdd.SaveChanges();
            }
        }

        public void deleteAccount(int user_id, string name)
        {
            Account ac = bdd.Account.FirstOrDefault(a => a.user_id == user_id && a.name == name);
            if (ac != null)
            {
                bdd.Account.Remove(ac);
                bdd.SaveChanges();
            }
        }
        #endregion

        #region Category
        public List<Category> getCategories ()
        {
            return bdd.Category.ToList();
        }
 
        public List<Category> getCategories(int user_id)
        {
            return bdd.Category.Where(c => c.user_id == user_id).ToList();
        }

        public Category getCategory (int id)
        {
            return bdd.Category.FirstOrDefault(c => c.id == id);
        }

        public Category getCategory (int user_id, string name)
        {
            return bdd.Category.FirstOrDefault(c => c.user_id == user_id && c.name == name);
        }

        public Category addCategory (int user_id, string name)
        {
            Category cat = bdd.Category.Add(new Category { user_id = user_id, name = name });
            bdd.SaveChanges();
            return cat;
        }

        public Category addCategory (Category cat)
        {
            Category res = bdd.Category.Add(new Category { user_id = cat.user_id, name = cat.name });
            bdd.SaveChanges();
            return res;
        }

        public Category setCategory (int Category_id, int user_id, string name)
        {
            Category cat = bdd.Category.FirstOrDefault(c => c.id == Category_id);
            if (cat != null)
            {
                cat.user_id = user_id;
                cat.name = name;
                bdd.SaveChanges();
            }
            return cat;
        }

        public void deleteCategory(int id)
        {
            Category cat = bdd.Category.FirstOrDefault(c => c.id == id);
            if (cat != null)
            {
                bdd.Category.Remove(cat);
                bdd.SaveChanges();
            }
        }

        public void deleteCategory (int user_id, string name)
        {
            Category cat = bdd.Category.FirstOrDefault(c => c.user_id == user_id && c.name == name);
            if (cat != null)
            {
                bdd.Category.Remove(cat);
                bdd.SaveChanges();
            }
        }
        #endregion

        #region Transaction
        public List<Transaction> getTransactions (TransacFilter filter)
        {
            if(filter == TransacFilter.ALL)
                return bdd.Transaction.ToList();
            bool bool_filter = (filter == TransacFilter.VALIDATED) ? true : false;
            return bdd.Transaction.Where(t => t.validated == bool_filter).ToList();
        }

        public List<Transaction> getTransactions(int Account_id, TransacFilter filter)
        {
            if (filter == TransacFilter.ALL)
                return bdd.Transaction.Where(t => t.account_id == Account_id).ToList();
            bool bool_filter = (filter == TransacFilter.VALIDATED) ? true : false;
            return bdd.Transaction.Where(t => t.account_id == Account_id && t.validated == bool_filter).ToList();
        }

        public List<Transaction> getTransactions(int Account_id, DateTime date, TransacFilter filter)
        {
            if(filter == TransacFilter.ALL)
                return bdd.Transaction.Where(t => t.account_id == Account_id && t.date >= date).ToList();
            bool bool_filter = (filter == TransacFilter.VALIDATED) ? true : false;
            return bdd.Transaction.Where(t => t.account_id == Account_id && t.date >= date && t.validated == bool_filter).ToList();
        }

        public List<Transaction> getTransactions(int Account_id, string name, TransacFilter filter)
        {
            if (filter == TransacFilter.ALL)
                return bdd.Transaction.Where(t => t.account_id == Account_id && t.name.Contains(name)).ToList();
            bool bool_filter = (filter == TransacFilter.VALIDATED) ? true : false;
            return bdd.Transaction.Where(t => t.account_id == Account_id && t.name.Contains(name) && t.validated == bool_filter).ToList();
        }

        public Transaction getTransaction(int id)
        {
            return bdd.Transaction.FirstOrDefault(t => t.id == id);
        }

        public Transaction addTransaction(int Account_id, string name, float value, bool validated, DateTime date)
        {
            Transaction t = bdd.Transaction.Add (new Transaction { account_id = Account_id, name = name, value = value, validated = validated, date = date });
            bdd.SaveChanges();
            return t;
        }

        public Transaction addTransaction(Transaction t)
        {
            Transaction transac = bdd.Transaction.Add(new Transaction { account_id = t.account_id, name = t.name, value = t.value, validated = t.validated, date = t.date });
            bdd.SaveChanges();
            return transac;
        }

        public Transaction setTransaction(int Transaction_id, int Account_id, string name, float value, bool validated, DateTime date) {
            Transaction transac = bdd.Transaction.FirstOrDefault(t => t.id == Transaction_id);
            if (transac != null)
            {
                transac.account_id = Account_id;
                transac.name = name;
                transac.value = value;
                transac.validated = validated;
                transac.date = date;
                bdd.SaveChanges();
            }
            return transac;
        }

        public void deleteTransaction(int id)
        {
            Transaction transac = bdd.Transaction.FirstOrDefault(t => t.id == id);
            if (transac != null)
            {
                bdd.Transaction.Remove(transac);
                bdd.SaveChanges();
            }
        }
        #endregion
    }
}
 