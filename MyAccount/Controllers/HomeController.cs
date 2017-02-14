using MyAccount.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyAccount.Controllers
{
    [Authorize]
    public class HomeController : MainController
    {

        private IDal dal;
       
        public HomeController ()
        {
            dal = new Dal();
        }

        /*
         * Retrieves all User's accounts
         * Retrieves all User's transactions from the begin of the month
         */
        public ActionResult Index()
        {
            ViewBag.AccountsList = new SelectList(dal.getAccounts(user.id), "id", "name");

            DateTime current_date = DateTime.Now;
            DateTime begin_date = new DateTime(current_date.Year, current_date.Month, 1);

            List<Transaction> transactions = new List<Transaction>();
            List<Transaction> invalidated_transactions = new List<Transaction>();
            foreach (var account in dal.getAccounts(user.id))
            {
                transactions.AddRange(dal.getTransactions(account.id, begin_date, Dal.TransacFilter.VALIDATED));
                transactions.AddRange(dal.getTransactions(account.id, begin_date, Dal.TransacFilter.INVALIDATED));
            }
            ViewBag.TransactionsList = transactions;
            ViewBag.InvalidatedTransactionsList = invalidated_transactions;

            return View();
        }
        
        [HttpPost]
        public ActionResult Index(int account_id)
        {
            ViewBag.AccountsList = new SelectList(dal.getAccounts(user.id), "id", "name");

            DateTime current_date = DateTime.Now;
            DateTime begin_date = new DateTime(current_date.Year, current_date.Month, 1);

            ViewBag.TransactionsList = dal.getTransactions(account_id, begin_date, Dal.TransacFilter.VALIDATED);
            ViewBag.InvalidatedTransactionsList = dal.getTransactions(account_id, begin_date, Dal.TransacFilter.INVALIDATED);

            return View();
        }
    }
}