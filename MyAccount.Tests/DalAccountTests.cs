﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyAccount.Models;
using System.Collections.Generic;

namespace MyAccount.Tests
{
    [TestClass]
    public class DalAccountTests
    {
        private IDal dal;

        [TestInitialize]
        public void init()
        {
            dal = new Dal();

            List<Account> accounts = dal.getAccounts();
            foreach (var a in accounts)
            {
                dal.deleteAccount(a.id);
            }
        }

        [TestCleanup]
        public void clean()
        {
            dal.Dispose();
        }

        [TestMethod]
        public void TestGetAccounts()
        {
            List<Account> accounts = dal.getAccounts();
            Assert.AreEqual(0, accounts.Count);
            dal.addAccount(0, "test");
            dal.addAccount(0, "test2");
            accounts = dal.getAccounts();
            Assert.AreEqual(2, accounts.Count);

            dal.deleteAccount(0, "test");
            dal.deleteAccount(0, "test2");
        }

        [TestMethod]
        public void TestGetAccounts_int_param ()
        {
            dal.addAccount(0, "test");
            List<Account> accounts = dal.getAccounts(1);
            Assert.AreEqual(0, accounts.Count);
            dal.addAccount(1, "test1");
            accounts = dal.getAccounts(1);
            Assert.AreEqual(1, accounts.Count);

            dal.deleteAccount(0, "test");
            dal.deleteAccount(1, "test1");
        }

        [TestMethod]
        public void TestGetAccount_int_param()
        {
            Account a1 = dal.addAccount(0, "test");
            Account a2 = dal.addAccount(1, "test2");
            Account a = dal.getAccount(a1.id);
            Assert.AreNotEqual(null, a);
            Assert.AreEqual("test", a.name);

            dal.deleteAccount(0, "test");
            dal.deleteAccount(1, "test2");
        }

        [TestMethod]
        public void TestGetAccount_intstring()
        {
            Account a = dal.getAccount(0, "test");
            Assert.AreEqual(null, a);
            dal.addAccount(0, "test");
            dal.addAccount(1, "test2");
            a = dal.getAccount(0, "test");
            Assert.AreNotEqual(null, a);
            Assert.AreEqual("test", a.name);

            dal.deleteAccount(0, "test");
            dal.deleteAccount(1, "test2");
        }

        [TestMethod]
        public void TestAddAccount_intstring()
        {
            List<Account> accounts = dal.getAccounts();
            Assert.AreEqual(0, accounts.Count);
            dal.addAccount(0, "test");
            dal.addAccount(0, "test2");
            accounts = dal.getAccounts();
            Assert.AreEqual(2, accounts.Count);

            dal.deleteAccount(0, "test");
            dal.deleteAccount(0, "test2");
        }

        [TestMethod]
        public void TestAddAccount_intstringfloat()
        {
            List<Account> accounts = dal.getAccounts();
            Assert.AreEqual(0, accounts.Count);
            dal.addAccount(0, "test", 10.2f);
            dal.addAccount(0, "test2", 12.4f);
            accounts = dal.getAccounts();
            Assert.AreEqual(2, accounts.Count);
            Account ac = dal.getAccount(0, "test");
            Assert.AreEqual(10.2f, ac.value);

            dal.deleteAccount(0, "test");
            dal.deleteAccount(0, "test2");
        }

        [TestMethod]
        public void TestAddAccount_account()
        {
            List<Account> accounts = dal.getAccounts();
            Assert.AreEqual(0, accounts.Count);
            dal.addAccount(new Account { name = "test", user_id = 0, value = 10.2f });
            dal.addAccount(new Account { name = "test2", user_id = 1, value = 5.3f });
            accounts = dal.getAccounts();
            Assert.AreEqual(2, accounts.Count);
            Account ac = dal.getAccount(0, "test");
            Assert.AreEqual(10.2f, ac.value);

            dal.deleteAccount(0, "test");
            dal.deleteAccount(0, "test2");
        }

        [TestMethod]
        public void TestSetAccount ()
        {
            Account ac = dal.addAccount(0, "test", 10.2f);
            Account ac_updated = dal.setAccount(ac.id, 0, "test2", 2.3f);
            Assert.AreEqual("test2", ac_updated.name);
            Assert.AreEqual(2.3f, ac_updated.value);

            dal.deleteAccount(ac.id);
        }

        [TestMethod]
        public void TestDeleteAccount_int()
        {
            Account ac = dal.addAccount(0, "test");
            Assert.AreEqual(1, dal.getAccounts().Count);
            dal.deleteAccount(ac.id);
            Assert.AreEqual(0, dal.getAccounts().Count);
        }

        [TestMethod]
        public void TestDeleteAccount_intstring()
        {
            Account ac = dal.addAccount(0, "test");
            Assert.AreEqual(1, dal.getAccounts().Count);
            dal.deleteAccount(0, "test");
            Assert.AreEqual(0, dal.getAccounts().Count);
        }   
    }
}
