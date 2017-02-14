using System.Web.Mvc;

namespace MyAccount.Areas.Authentication
{
    public class AuthenticationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Authentication";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Authentication_default",
                "Authentication/{controller}/{action}/{id}",
                new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "MyAccount.Areas.Authentication.Controllers" }
            );
        }
    }
}