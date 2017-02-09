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
        public void TestAddUser()
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
        public void TestSetUser ()
        {
            user u = dal.addUser("test", "bidule");
            dal.setUser(u.id, "bidule", "test");
            u = dal.getUser(u.id);
            Assert.AreEqual("bidule", u.login);
            Assert.AreEqual("test", u.password);
        }
    }
}
