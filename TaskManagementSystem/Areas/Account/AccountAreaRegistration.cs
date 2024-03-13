using System.Web.Mvc;

namespace TaskManagementSystem.Areas.Account
{
    public class AccountAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Account";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Account_default",
                "Account/{controller}/{action}/{id}",
                new { controller="Home", action = "Login", id = UrlParameter.Optional },
                new[] { "TaskManagementSystem.Areas.Account.Controllers" }
            );
        }
    }
}