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
            GetRoleList();
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginData _loginData)
        {
            GetRoleList(); 
            if (ModelState.IsValid)
            {
                Handler handler = new Handler();
                User _user = handler.GetUserDetails(_loginData.Email);
                if (string.IsNullOrEmpty(_user.Email))
                {
                    ModelState.AddModelError("Email", "Email Id Not Registered");
                    return View();
                }

                if (_user.RoleId.ToString() != _loginData.Role)
                {
                    ModelState.AddModelError("Role", "Selected Role not matched with our records.");
                    return View();
                }

                bool IsPassValid = PasswordHash.VerifyHashBCrypt(_loginData.Password, _user.Password);
                if (IsPassValid)
                {
                    FormsAuthentication.SetAuthCookie(_user.Email, false);
                    if (_user.Role == "Admin")
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else if (_user.Role == "User")
                    {
                        return RedirectToAction("Index", "Home");
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

        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        private void GetRoleList()
        {
            List<SelectListItem> roleList = new List<SelectListItem>();
            roleList.Add(new SelectListItem
            {
                Text = "Admin",
                Value = "1"
            });
            roleList.Add(new SelectListItem
            {
                Text = "Employee",
                Value = "2"
            });
            roleList.Add(new SelectListItem
            {
                Text = "Client",
                Value = "3"
            });

            ViewBag.RoleList = roleList;
            roleList = null;
        }

        #region Generate Password for Admin Registration
        [HttpPost]
        public ActionResult GeneratePassword(string Pass)
        {
            string hashPass = PasswordHash.CreateHashBCrypt(Pass);
            ViewBag.HashPass = hashPass;
            return View();
        }

        [HttpGet]
        public ActionResult GeneratePassword()
        {
            ViewBag.HashPass = "";
            return View();
        }
        #endregion
    }
}