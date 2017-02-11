using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyAccount.Models;
using System.Collections.Generic;

namespace MyAccount.Tests
{
    [TestClass]
    public class DalTransactionTests
    {
        private IDal dal;

        [TestInitialize]
        public void init()
        {
            dal = new Dal();

            List<transaction> transactions = dal.getTransactions();
            foreach (var t in transactions)
            {
                dal.deleteTransaction(t.id);
            }
        }

        [TestCleanup]
        public void clean()
        {
            dal.Dispose();
        }

        [TestMethod]
        public void TestGetTransactions()
        {
            List<transaction> trans = dal.getTransactions();
            Assert.AreEqual(0, trans.Count);
            transaction t1 = dal.addTransaction(0, "test", 0, false, DateTime.Now);
            transaction t2 = dal.addTransaction(1, "test2", 12.2f, true, DateTime.Now);
            trans = dal.getTransactions();
            Assert.AreEqual(2, trans.Count);

            dal.deleteTransaction(t1.id);
            dal.deleteTransaction(t2.id);
        }

        [TestMethod]
        public void TestGetTransactions_int_param()
        {
            List<transaction> trans = dal.getTransactions(0);
            Assert.AreEqual(0, trans.Count);
            transaction t1 = dal.addTransaction(0, "test", 0, false, DateTime.Now);
            transaction t2 = dal.addTransaction(1, "test2", 12.2f, true, DateTime.Now);
            trans = dal.getTransactions(0);
            Assert.AreEqual(1, trans.Count);
            Assert.AreEqual("test", trans[0].name);

            dal.deleteTransaction(t1.id);
            dal.deleteTransaction(t2.id);
        }

        [TestMethod]
        public void TestGetTransactions_intdatetime_param()
        {
            DateTime d1 = new DateTime(2017, 01, 01);
            DateTime d2 = new DateTime(2017, 02, 01);
            List<transaction> trans = dal.getTransactions(0, d2);
            Assert.AreEqual(0, trans.Count);
            transaction t1 = dal.addTransaction(0, "test", 0, false, d1);
            transaction t2 = dal.addTransaction(0, "test2", 12.2f, true, d2);
            trans = dal.getTransactions(0, d2);
            Assert.AreEqual(1, trans.Count);
            Assert.AreEqual("test2", trans[0].name);
            trans = dal.getTransactions(0, d1);
            Assert.AreEqual(2, trans.Count);

            dal.deleteTransaction(t1.id);
            dal.deleteTransaction(t2.id);
        }

        [TestMethod]
        public void TestGetTransactions_intstring_param()
        {
            List<transaction> trans = dal.getTransactions(0, "test");
            Assert.AreEqual(0, trans.Count);
            transaction t1 = dal.addTransaction(0, "test", 0, false, DateTime.Now);
            transaction t2 = dal.addTransaction(0, "bidule", 12.2f, true, DateTime.Now);
            trans = dal.getTransactions(0, "test");
            Assert.AreEqual(1, trans.Count);
            Assert.AreEqual("test", trans[0].name);
            transaction t3 = dal.addTransaction(0, "testbidule", 12.2f, true, DateTime.Now);
            trans = dal.getTransactions(0, "test");
            Assert.AreEqual(2, trans.Count);

            dal.deleteTransaction(t1.id);
            dal.deleteTransaction(t2.id);
            dal.deleteTransaction(t3.id);
        }

        [TestMethod]
        public void TestGetInvalidatedTransactions_int_param()
        {
            List<transaction> trans = dal.getInvalidatedTransactions(0);
            Assert.AreEqual(0, trans.Count);
            transaction t1 = dal.addTransaction(0, "test", 0, true, DateTime.Now);
            transaction t2 = dal.addTransaction(0, "test2", 12.2f, false, DateTime.Now);
            transaction t3 = dal.addTransaction(1, "test3", 12.2f, false, DateTime.Now);
            trans = dal.getInvalidatedTransactions(0);
            Assert.AreEqual(1, trans.Count);
            Assert.AreEqual("test2", trans[0].name);

            dal.deleteTransaction(t1.id);
            dal.deleteTransaction(t2.id);
            dal.deleteTransaction(t3.id);
        }

        [TestMethod]
        public void TestGetInvalidatedTransactions_intdatatime_param()
        {
            List<transaction> trans = dal.getInvalidatedTransactions(0);
            Assert.AreEqual(0, trans.Count);
            transaction t1 = dal.addTransaction(0, "test", 0, true, DateTime.Now);
            transaction t2 = dal.addTransaction(0, "test2", 12.2f, false, new DateTime(2017, 04, 04));
            transaction t3 = dal.addTransaction(0, "test3", 12.2f, false, new DateTime(2017, 01, 01));
            trans = dal.getInvalidatedTransactions(0, DateTime.Now);
            Assert.AreEqual(1, trans.Count);
            Assert.AreEqual("test2", trans[0].name);

            dal.deleteTransaction(t1.id);
            dal.deleteTransaction(t2.id);
            dal.deleteTransaction(t3.id);
        }

        [TestMethod]
        public void TestGetInvalidatedTransactions_intstring_param()
        {
            List<transaction> trans = dal.getInvalidatedTransactions(0);
            Assert.AreEqual(0, trans.Count);
            transaction t1 = dal.addTransaction(0, "test", 0, true, DateTime.Now);
            transaction t2 = dal.addTransaction(0, "test2", 12.2f, false, DateTime.Now);
            transaction t3 = dal.addTransaction(0, "bidule", 12.2f, false, DateTime.Now);
            trans = dal.getInvalidatedTransactions(0, "test");
            Assert.AreEqual(1, trans.Count);
            Assert.AreEqual("test2", trans[0].name);

            dal.deleteTransaction(t1.id);
            dal.deleteTransaction(t2.id);
            dal.deleteTransaction(t3.id);
        }

        [TestMethod]
        public void TestGetTransaction()
        {
            transaction t1 = dal.addTransaction(0, "test", 0, false, DateTime.Now);
            transaction t2 = dal.addTransaction(0, "bidule", 12.2f, true, DateTime.Now);
            transaction test = dal.getTransaction(t1.id);
            Assert.AreNotEqual(null, test);
            Assert.AreEqual("test", test.name);

            dal.deleteTransaction(t1.id);
            dal.deleteTransaction(t2.id);
        }

        [TestMethod]
        public void TestAddTransaction_intstringfloatboolDateTime_param()
        {
            List<transaction> trans = dal.getTransactions();
            Assert.AreEqual(0, trans.Count);
            transaction t1 = dal.addTransaction(0, "test", 0, false, DateTime.Now);
            transaction t2 = dal.addTransaction(1, "test2", 12.2f, true, DateTime.Now);
            trans = dal.getTransactions();
            Assert.AreEqual(2, trans.Count);

            dal.deleteTransaction(t1.id);
            dal.deleteTransaction(t2.id);
        }

        [TestMethod]
        public void TestAddTransaction_transaction_param()
        {
            List<transaction> trans = dal.getTransactions();
            Assert.AreEqual(0, trans.Count);
            transaction t1 = dal.addTransaction(new transaction { id_account = 0, name = "test", value = 0, validated = false, date = DateTime.Now });
            transaction t2 = dal.addTransaction(new transaction { id_account = 1, name = "test2", value = 0, validated = false, date = DateTime.Now });
            trans = dal.getTransactions();
            Assert.AreEqual(2, trans.Count);

            dal.deleteTransaction(t1.id);
            dal.deleteTransaction(t2.id);
        }

        [TestMethod]
        public void TestSetTransaction(int transaction_id, int account_id, string name, float value, bool validated, DateTime date)
        {
            transaction t1 = dal.addTransaction(0, "test", 0, false, DateTime.Now);
            transaction t1_updated = dal.setTransaction(t1.id, 1, "test2", 12.3f, true, t1.date);
            Assert.AreEqual("test2", t1_updated.name);
            Assert.AreEqual(true, t1_updated.validated);

            dal.deleteTransaction(t1.id);
        }

        [TestMethod]
        public void TestDeleteTransaction(int id)
        {
            transaction t = dal.addTransaction(0, "test", 0, true, DateTime.Now);
            Assert.AreEqual(1, dal.getTransactions());
            dal.deleteTransaction(t.id);
            Assert.AreEqual(0, dal.getTransactions());
        }
    }
}
