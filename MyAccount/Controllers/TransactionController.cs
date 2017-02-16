using MyAccount.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyAccount.Controllers
{
    [Authorize]
    public class TransactionController : MainController
    {
        public ActionResult Index()
        {
            ViewBag.AccountsList = new SelectList(dal.getAccounts(user.id), "id", "name");

            DateTime current_date = DateTime.Now;
            DateTime begin_date = new DateTime(current_date.Year, current_date.Month, 1);

            List<Transaction> transactions = new List<Transaction>();
            foreach (var account in dal.getAccounts(user.id))
            {
                transactions.AddRange(dal.getTransactions(account.id, begin_date, Dal.TransacFilter.ALL));
            }
            ViewBag.TransactionsList = transactions;

            return View();
        }

        [HttpPost]
        public ActionResult Index(int account_id)
        {
            ViewBag.AccountsList = new SelectList(dal.getAccounts(user.id), "id", "name");

            DateTime current_date = DateTime.Now;
            DateTime begin_date = new DateTime(current_date.Year, current_date.Month, 1);

            ViewBag.TransactionsList = dal.getTransactions(account_id, begin_date, Dal.TransacFilter.ALL);

            return View();
        }

        [HttpPost]
        public ActionResult Add(Transaction transaction)
        {
            ViewBag.AccountsList = new SelectList(dal.getAccounts(user.id), "id", "name");

            DateTime current_date = DateTime.Now;
            DateTime begin_date = new DateTime(current_date.Year, current_date.Month, 1);

            if (ModelState.IsValid)
            {
                dal.addTransaction(transaction);
            }

            if (transaction != null)
            {
                ViewBag.TransactionsList = dal.getTransactions(transaction.account_id, begin_date, Dal.TransacFilter.ALL);
            } else
            {
                List<Transaction> transactions = new List<Transaction>();
                foreach (var account in dal.getAccounts(user.id))
                {
                    transactions.AddRange(dal.getTransactions(account.id, begin_date, Dal.TransacFilter.ALL));
                }
                ViewBag.TransactionsList = transactions;
            }

            return View("Index");
        }
    }
}