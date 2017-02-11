using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace MyAccount.Models
{
    public class Dal : IDal
    {
        private BddContext bdd;

        public Dal()
        {
            bdd = new BddContext();
        }

        public List<user> getUsers()
        {
            return bdd.user.ToList();
        }

        public user getUser(string login)
        {
            return bdd.user.FirstOrDefault(u => u.login == login);
        }

        public user getUser (int id)
        {
            return bdd.user.Find(id);
        }

        public user authentication (string login, string password)
        {
            return bdd.user.FirstOrDefault(u => u.login == login && u.password == password);
        }

        public user addUser(string login_value, string pass_value)
        {
            try
            {
                user usr = bdd.user.Add(new user { login = login_value, password = pass_value });
                bdd.SaveChanges();
                return usr;
            } catch (DbUpdateException e)
            {
                return null;
            }
        }

        public user addUser(user usr)
        {
            try
            {
                user u = bdd.user.Add(new user { login = usr.login, password = usr.password });
                bdd.SaveChanges();
                return u;
            } catch (DbUpdateException e)
            {
                return null;
            }
        }

        public user setUser(int id, string login, string pass)
        {
            user usr = bdd.user.Find(id);
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
            user usr = bdd.user.Find(id);
            if (usr != null)
            {
                bdd.user.Remove(usr);
                bdd.SaveChanges();
            }
        }

        public void deleteUser(string login)
        {
            user usr = bdd.user.FirstOrDefault(u => u.login == login);
            if (usr != null)
            {
                bdd.user.Remove(usr);
                bdd.SaveChanges();
            }
        }

        public List<account> getAccounts()
        {
            return bdd.account.ToList();
        }

        public List<account> getAccounts(int user_id)
        {
            return bdd.account.Where(a => a.id_user == user_id).ToList();
        }

        public account getAccount(int id)
        {
            return bdd.account.FirstOrDefault(a => a.id == id);
        }

        public account getAccount(int user_id, string name)
        {
            return bdd.account.FirstOrDefault(a => a.name == name && a.id_user == user_id);
        }

        public account addAccount(int user_id, string name)
        {
            account new_account = new account { name = name, id_user = user_id, value = 0 };
            bdd.account.Add(new_account);
            bdd.SaveChanges();
            return new_account;
        }

        public account addAccount(int user_id, string name, float value)
        {
            account new_account = new account { name = name, id_user = user_id, value = value };
            bdd.account.Add(new_account);
            bdd.SaveChanges();
            return new_account;
        }

        public account addAccount(account ac)
        {
            account new_account = bdd.account.Add(new account { name = ac.name, id_user = ac.id_user, value = ac.value });
            bdd.SaveChanges();
            return new_account;
        }

        public account setAccount(int account_id, int user_id, string name, float value)
        {
            account ac = bdd.account.FirstOrDefault(a => a.id == account_id);
            if (ac != null)
            {
                ac.id_user = user_id;
                ac.name = name;
                ac.value = value;
                bdd.SaveChanges();
            }
            return ac;
        }

        public void deleteAccount (int id)
        {
            account ac = bdd.account.FirstOrDefault(a => a.id == id);
            if (ac != null)
            {
                bdd.account.Remove(ac);
                bdd.SaveChanges();
            }
        }

        public void deleteAccount(int user_id, string name)
        {
            account ac = bdd.account.FirstOrDefault(a => a.id_user == user_id && a.name == name);
            if (ac != null)
            {
                bdd.account.Remove(ac);
                bdd.SaveChanges();
            }
        }

        public List<category> getCategories ()
        {
            return bdd.category.ToList();
        }
 
        public List<category> getCategories(int user_id)
        {
            return bdd.category.Where(c => c.user_id == user_id).ToList();
        }

        public category getCategory (int id)
        {
            return bdd.category.FirstOrDefault(c => c.id == id);
        }

        public category getCategory (int user_id, string name)
        {
            return bdd.category.FirstOrDefault(c => c.user_id == user_id && c.name == name);
        }

        public category addCategory (int user_id, string name)
        {
            category cat = bdd.category.Add(new category { user_id = user_id, name = name });
            bdd.SaveChanges();
            return cat;
        }

        public category addCategory (category cat)
        {
            category res = bdd.category.Add(new category { user_id = cat.user_id, name = cat.name });
            bdd.SaveChanges();
            return res;
        }

        public category setCategory (int category_id, int user_id, string name)
        {
            category cat = bdd.category.FirstOrDefault(c => c.id == category_id);
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
            category cat = bdd.category.FirstOrDefault(c => c.id == id);
            if (cat != null)
            {
                bdd.category.Remove(cat);
                bdd.SaveChanges();
            }
        }

        public void deleteCategory (int user_id, string name)
        {
            category cat = bdd.category.FirstOrDefault(c => c.user_id == user_id && c.name == name);
            if (cat != null)
            {
                bdd.category.Remove(cat);
                bdd.SaveChanges();
            }
        }

        public List<transaction> getTransactions ()
        {
            return bdd.transaction.ToList();
        }

        public List<transaction> getTransactions(int account_id)
        {
            return bdd.transaction.Where(t => t.id_account == account_id).ToList();
        }

        public List<transaction> getTransactions(int account_id, DateTime date)
        {
            return bdd.transaction.Where(t => t.id_account == account_id && t.date >= date).ToList();
        }

        public List<transaction> getTransactions(int account_id, string name)
        {
            return bdd.transaction.Where(t => t.id_account == account_id && t.name.Contains(name)).ToList();
        }

        public transaction getTransaction(int id)
        {
            return bdd.transaction.FirstOrDefault(t => t.id == id);
        }

        public transaction addTransaction(int account_id, string name, float value, bool validated, DateTime date)
        {
            transaction t = bdd.transaction.Add (new transaction { id_account = account_id, name = name, value = value, validated = validated, date = date });
            bdd.SaveChanges();
            return t;
        }

        public transaction addTransaction(transaction t)
        {
            transaction transac = bdd.transaction.Add(new transaction { id_account = t.id_account, name = t.name, value = t.value, validated = t.validated, date = t.date });
            bdd.SaveChanges();
            return transac;
        }

        public transaction setTransaction(int transaction_id, int account_id, string name, float value, bool validated, DateTime date) {
            transaction transac = bdd.transaction.FirstOrDefault(t => t.id == transaction_id);
            if (transac != null)
            {
                transac.id_account = account_id;
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
            transaction transac = bdd.transaction.FirstOrDefault(t => t.id == id);
            if (transac != null)
            {
                bdd.transaction.Remove(transac);
                bdd.SaveChanges();
            }
        }

        /*
        void deleteTransaction(int id);
         * */

        public void Dispose()
        {
            bdd.Dispose();
        }
    }
}
 