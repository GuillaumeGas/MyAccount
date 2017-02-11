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
         * Retrieves all user's accounts
         * Retrieves all user's transactions from the begin of the month
         */
        public ActionResult Index()
        {
            user u = Session["user"] as user;
            ViewBag.AccountsList = dal.getAccounts(u.id);

            DateTime current_date = DateTime.Now;
            DateTime begin_date = new DateTime(current_date.Year, current_date.Month, 1);

            List<transaction> transactions = new List<transaction>();
            foreach (var account in dal.getAccounts(u.id))
            {
                transactions.AddRange(dal.getTransactions(account.id, begin_date));
            }
            ViewBag.TransactionsList = transactions;

            return View();
        }

        public ActionResult Test ()
        {
            return View("Test");
        }
    }
}