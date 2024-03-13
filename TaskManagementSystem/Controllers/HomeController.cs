using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TaskManagementSystem.Common;
using TaskManagementSystem.DBHandler;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                Handler handler = new Handler();
                User _user = handler.GetUserDetails(user.Email);
                if (string.IsNullOrEmpty(_user.Email))
                {
                    ModelState.AddModelError("Email", "Email Id Not Registered");
                    return View();
                }

                bool IsPassValid = PasswordHash.VerifyHashBCrypt(user.Password, _user.Password);
                if (IsPassValid)
                {
                    FormsAuthentication.SetAuthCookie(_user.Email, false);
                    if (_user.Role == "Admin")
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else if (_user.Role == "User")
                    {
                        return RedirectToAction("Index", "Home", new { area = "Users" });
                    }
                    else if (_user.Role == "Employee")
                    {
                        return RedirectToAction("Index", "Home", new { area = "Employee" });
                    }
                }
                ModelState.AddModelError("Password", "Password Invalid");
            }
            return View();
        }
    }
}