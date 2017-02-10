using MyAccount.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyAccount.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private IDal dal;

        public HomeController ()
        {
            dal = new Dal();
        }

        // GET: Home
        public ActionResult Index()
        {
            user u = Session["user"] as user;
            ViewBag.AccountsList = dal.getAccounts(u.id);

            return View();
        }

        public ActionResult Test ()
        {
            return View("Test");
        }
    }
}