using MyAccount.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyAccount.Controllers
{
    public class MainController : Controller
    {
        protected User user;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
                if (Session["user"] != null)
            {
                user = Session["user"] as User;
                base.OnActionExecuting(filterContext);
            } else
            {
                filterContext.Result = new RedirectResult("~/Account/Login");
            }
        }
    }
}