using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyAccount.Models;
using System.Collections.Generic;

namespace MyAccount.Tests
{
    [TestClass]
    public class DalCategoryTests
    {
        private IDal dal;

        [TestInitialize]
        public void init()
        {
            dal = new Dal();

            List<Category> categories = dal.getCategories();
            foreach (var c in categories)
            {
                dal.deleteCategory(c.id);
            }
        }

        [TestCleanup]
        public void clean()
        {
            dal.Dispose();
        }

        [TestMethod]
        public void TestGetCategories()
        {
            List<Category> categories = dal.getCategories();
            Assert.AreEqual(0, categories.Count);
            dal.addCategory(0, "test");
            dal.addCategory(0, "test2");
            categories = dal.getCategories();
            Assert.AreEqual(2, categories.Count);

            dal.deleteCategory(0, "test");
            dal.deleteCategory(0, "test2");
        }

        [TestMethod]
        public void TestGetCategories_int_param()
        {
            dal.addCategory(0, "test");
            List<Category> categories = dal.getCategories(1);
            Assert.AreEqual(0, categories.Count);
            dal.addCategory(1, "test1");
            categories = dal.getCategories(1);
            Assert.AreEqual(1, categories.Count);

            dal.deleteCategory(0, "test");
            dal.deleteCategory(1, "test1");
        }

        [TestMethod]
        public void TestGetCategory_int_param()
        {
            Category a1 = dal.addCategory(0, "test");
            Category a2 = dal.addCategory(1, "test2");
            Category a = dal.getCategory(a1.id);
            Assert.AreNotEqual(null, a);
            Assert.AreEqual("test", a.name);

            dal.deleteCategory(0, "test");
            dal.deleteCategory(1, "test2");
        }

        [TestMethod]
        public void TestGetCategory_intstring()
        {
            Category a = dal.getCategory(0, "test");
            Assert.AreEqual(null, a);
            dal.addCategory(0, "test");
            dal.addCategory(1, "test2");
            a = dal.getCategory(0, "test");
            Assert.AreNotEqual(null, a);
            Assert.AreEqual("test", a.name);

            dal.deleteCategory(0, "test");
            dal.deleteCategory(1, "test2");
        }

        [TestMethod]
        public void TestAddCategory_intstring()
        {
            List<Category> categories = dal.getCategories();
            Assert.AreEqual(0, categories.Count);
            dal.addCategory(0, "test");
            dal.addCategory(0, "test2");
            categories = dal.getCategories();
            Assert.AreEqual(2, categories.Count);

            dal.deleteCategory(0, "test");
            dal.deleteCategory(0, "test2");
        }

        [TestMethod]
        public void TestAddCategory_category()
        {
            List<Category> categories = dal.getCategories();
            Assert.AreEqual(0, categories.Count);
            dal.addCategory(new Category { name = "test", user_id = 0 });
            dal.addCategory(new Category { name = "test2", user_id = 1 });
            categories = dal.getCategories();
            Assert.AreEqual(2, categories.Count);
            Category cat = dal.getCategory(0, "test");
            Assert.AreEqual("test", cat.name);

            dal.deleteCategory(0, "test");
            dal.deleteCategory(0, "test2");
        }

        [TestMethod]
        public void TestSetCategory()
        {
            Category cat = dal.addCategory(0, "test");
            Category cat_updated = dal.setCategory(cat.id, 0, "test2");
            Assert.AreEqual("test2", cat_updated.name);

            dal.deleteCategory(cat.id);
        }

        [TestMethod]
        public void TestDeleteCategory_int()
        {
            Category cat = dal.addCategory(0, "test");
            Assert.AreEqual(1, dal.getCategories().Count);
            dal.deleteCategory(cat.id);
            Assert.AreEqual(0, dal.getCategories().Count);
        }

        [TestMethod]
        public void TestDeleteCategory_intstring()
        {
            Category cat = dal.addCategory(0, "test");
            Assert.AreEqual(1, dal.getCategories().Count);
            dal.deleteCategory(0, "test");
            Assert.AreEqual(0, dal.getCategories().Count);
        }
    }
}
