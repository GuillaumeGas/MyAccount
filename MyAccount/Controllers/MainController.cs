using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyAccount.Controllers
{
    public class MainController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
                if (Session["user"] != null)
            {
                base.OnActionExecuting(filterContext);
            } else
            {
                filterContext.Result = new RedirectResult("~/Account/Login");
            }
        }
    }
}