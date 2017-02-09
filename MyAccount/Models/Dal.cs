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

        public void setUser(int id, string login, string pass)
        {
            user usr = bdd.user.Find(id);
            if (usr != null)
            {
                usr.login = login;
                usr.password = pass;
                bdd.SaveChanges();
            }
        }

        public void deleteUser(int id)
        {
            bdd.user.Remove(bdd.user.Find(id));
            bdd.SaveChanges();
        }

        public void deleteUser(string login)
        {
            bdd.user.Remove(bdd.user.FirstOrDefault(u => u.login == login));
            bdd.SaveChanges();
        }

        public void Dispose()
        {
            bdd.Dispose();
        }
    }
}