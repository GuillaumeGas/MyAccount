using MyAccount.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyAccount.Controllers
{
    public class AccountController : MainController
    {
        public ActionResult Index()
        {
            ViewBag.AccountsList = dal.getAccounts(user.id);

            return View();
        }

        [HttpPost]
        public ActionResult Index(Account account)
        {
            if(ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(account.name))
                {
                    ModelState.AddModelError("error", "The account name can't be empty !");
                } else if (dal.getAccount(user.id, account.name) != null)
                {
                    ModelState.AddModelError("error", "You already have an account with the same name.");
                } else
                {
                    dal.addAccount(user.id, account.name, account.value);
                }
            }

            ViewBag.AccountsList = dal.getAccounts(user.id);

            return View();
        }

    }
}