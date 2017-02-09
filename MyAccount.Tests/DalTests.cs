using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyAccount.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace MyAccount.Tests
{
    [TestClass]
    public class DalTests
    {
        private IDal dal;

        [TestInitialize]
        public void init()
        {
            dal = new Dal();
        }

        [TestCleanup]
        public void clean()
        {
            dal.Dispose();
        }

        [TestMethod]
        public void TestGetUsers()
        {
            List<user> users = dal.getUsers();
            Assert.AreEqual(0, users.Count);
            user u1 = dal.addUser("test", "test");
            user u2 = dal.addUser("test2", "test2");
            users = dal.getUsers();
            Assert.AreEqual(2, users.Count);
            dal.deleteUser(u1.id);
            dal.deleteUser(u2.id);
        }

        [TestMethod]
        public void TestGetUser_string_param()
        {
            user user_unknown = dal.getUser("test");
            Assert.AreEqual(null, user_unknown);
            dal.addUser("test", "testpass");
            dal.addUser("bidule", "bidulepass");
            user u = dal.getUser("test");
            Assert.AreEqual("test", u.login);
            Assert.AreEqual("testpass", u.password);
            user u2 = dal.getUser("bidule");
            Assert.AreEqual("bidule", u2.login);
            Assert.AreEqual("bidulepass", u2.password);

            dal.deleteUser("test");
            dal.deleteUser("bidule");
        }

        [TestMethod]
        public void TestGetUser_int_param()
        {
            user user_unknown = dal.getUser(0);
            Assert.AreEqual(null, user_unknown);
            user user_added1 = dal.addUser("test", "testpass");
            user user_added2 = dal.addUser("bidule", "bidulepass");
            user u = dal.getUser(user_added1.id);
            Assert.AreEqual("test", u.login);
            Assert.AreEqual("testpass", u.password);
            user u2 = dal.getUser(user_added2.id);
            Assert.AreEqual("bidule", u2.login);
            Assert.AreEqual("bidulepass", u2.password);

            dal.deleteUser("test");
            dal.deleteUser("bidule");
        }

        [TestMethod]
        public void TestAuthentication()
        {
            user user_unknown = dal.authentication("test", "test");
            Assert.AreEqual(null, user_unknown);
            user u1 = dal.addUser("test", "testpass");
            user user_authenticated = dal.authentication("test", "testpass");
            Assert.AreEqual("test", user_authenticated.login);
            Assert.AreEqual("testpass", user_authenticated.password);

            dal.deleteUser("test");
        }

        [TestMethod]
        public void TestAddUser_stringstring_param()
        {
            user u = dal.addUser("test", "pass");
            List<user> users = dal.getUsers();
            user usr = dal.getUser("test");
            Assert.AreEqual(1, users.Count);
            Assert.AreNotEqual(null, usr);
            Assert.AreEqual("test", usr.login);

            dal.deleteUser(usr.id);
        }

        [TestMethod]
        public void TestAddUser_user_param()
        {
            user u = new user { login = "test", password = "testpass" };
            user u_res = dal.addUser(u);
            Assert.AreEqual(u.login, u_res.login);
            Assert.AreEqual(u.password, u_res.password);
            Assert.AreEqual(1, dal.getUsers().Count);

            dal.deleteUser(u_res.id);   
        }

        [TestMethod]
        public void TestSetUser ()
        {
            user u = dal.addUser("test", "bidule");
            dal.setUser(u.id, "bidule", "test");
            u = dal.getUser(u.id);
            Assert.AreEqual("bidule", u.login);
            Assert.AreEqual("test", u.password);

            dal.deleteUser("bidule");
        }

        [TestMethod]
        public void TestDeleteUser_int_param()
        {
            user u = dal.addUser("test", "testparam");
            dal.deleteUser(u.id);
            Assert.AreEqual(null, dal.getUser(u.id));
        }

        [TestMethod]
        public void TestDeleteUser_string_param ()
        {
            user u = dal.addUser("test", "testparam");
            dal.deleteUser(u.login);
            Assert.AreEqual(null, dal.getUser(u.id));
        }
    }
}
