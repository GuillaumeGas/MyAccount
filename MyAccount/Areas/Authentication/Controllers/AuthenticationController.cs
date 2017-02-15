    using MyAccount.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyAccount.Areas.Authentication.Controllers
{
    public class AuthenticationController : Controller
    {

        private IDal dal;

        public AuthenticationController()
        {
            dal = new Dal();
        }

        public ActionResult Login ()
        {
            if (HttpContext.User.Identity.IsAuthenticated && Session["user"] != null)
            {
                return Redirect("~/Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login (User usr, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User u = dal.authentication(usr.login, usr.password);
                if (u != null)
                {
                    Session["User"] = u;
                    FormsAuthentication.SetAuthCookie(u.id.ToString(), false);
                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return Redirect("~/Home");
                }
                ModelState.AddModelError("error", "Login error !");
            }
            return View();
        }

        public ActionResult Signup ()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup (User usr)
        {
            if (ModelState.IsValid)
            {
                User u = dal.addUser(usr);
                if (u != null)
                {
                    Session["User"] = u;
                    FormsAuthentication.SetAuthCookie(u.id.ToString(), false);
                    return Redirect("~/Home");
                }
                ModelState.AddModelError("error", "This username is already used !");
            }
            return View();
        }

        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            Session["user"] = null;
            return Redirect("Login");
        }
    }
}