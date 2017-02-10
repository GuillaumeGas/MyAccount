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
            account new_account = new account { name = ac.name, id_user = ac.id_user, value = ac.value };
            bdd.account.Add(new_account);
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

        public void Dispose()
        {
            bdd.Dispose();
        }
    }
}