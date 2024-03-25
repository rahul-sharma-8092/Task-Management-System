using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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
                    string authToken = string.Empty;
                    string UserId = Encryption.Encrypt(_user.UserId.ToString());
                    string FullName = Encryption.Encrypt(_user.FullName);
                    string Email = Encryption.Encrypt(_user.Email);
                    string RoleId = Encryption.Encrypt(_user.RoleId.ToString());

                    authToken = UserId + "|" + FullName + "|" + Email + "|" + RoleId;

                    FormsAuthentication.SetAuthCookie(authToken, false);

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

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(FormCollection form)
        {
            Handler handler = new Handler();
            if (form.Keys.Count > 0)
            {
                string email = form["Email"].ToString();
                if (!string.IsNullOrEmpty(email))
                {
                    User user = handler.GetUserDetails(email);
                    if (!string.IsNullOrEmpty(user.Email))
                    {
                        string token = user.Email + "|" + user.UserId + "|" + DateTime.Now.ToString("O");
                        token = Encryption.Encrypt(token);

                        string _url = "https://localhost:44330/Home/ResetPassword?token=" + token;

                        bool res = handler.SaveForgotPassToken(user.Email, user.UserId, token);

                        if (res)
                        {
                            string rohan = Encryption.Decrypt(token);

                            Logger.ForgotPassLinksLogger(_url);

                            //Send this url to User through Email
                            ModelState.AddModelError("message", "Link Send to your registered e-mail.");
                            return View();
                        }
                        ModelState.AddModelError("message", "Something went wrong! Try again later.");
                        return View();
                    }
                    ModelState.AddModelError("Email", "Invalid Email id.");
                    return View();
                }
            }
            ModelState.AddModelError("Email", "Email-Id is required.");
            return View();
        }

        [HttpGet]
        public ActionResult ResetPassword(string token)
        {
            string DecToken = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("token is null.");
                }
                token = token.Replace(' ', '+');
                DecToken = Encryption.Decrypt(token);
            }
            catch (Exception)
            {
                ViewBag.IsLinkExpired = true;
                return View();
            }

            string email = DecToken.Split('|')[0];
            string userId = DecToken.Split('|')[1];

            Handler handler = new Handler();

            DataTable table = handler.GetForgotPassToken(email, Convert.ToInt32(userId));
            if (table.Rows.Count > 0)
            {
                DecToken = table.Rows[0]["Token"].ToString();
                bool isEqual = string.Equals(token, DecToken);

                DateTime generateTime = Convert.ToDateTime(table.Rows[0]["CreatedAt"]);
                int isValid = DateTime.Compare(DateTime.Now, generateTime.AddMinutes(30));

                if (isEqual && (isValid < 0))
                {
                    ViewBag.IsLinkExpired = false;
                    TempData["token"] = token;
                    return View();
                }
            }
            ViewBag.IsLinkExpired = true;
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(FormCollection form)
        {
            string password = form["Password"].ToString();
            string cnfPassword = form["CnfPassword"].ToString();

            if (!string.Equals(password, cnfPassword))
            {
                ModelState.AddModelError("CnfPassword", "Password & Confirm Password must be same.");
                ViewBag.IsLinkExpired = false;
                return View();
            }

            string token = TempData["token"].ToString();
            string DecToken = Encryption.Decrypt(token);
            string email = DecToken.Split('|')[0];
            string userId = DecToken.Split('|')[1];

            Handler handler = new Handler();

            DataTable table = handler.GetForgotPassToken(email, Convert.ToInt32(userId));
            if (table.Rows.Count > 0)
            {
                DecToken = table.Rows[0]["Token"].ToString();
                bool isEqual = string.Equals(token, DecToken);

                DateTime generateTime = Convert.ToDateTime(table.Rows[0]["CreatedAt"]);
                int isValid = DateTime.Compare(DateTime.Now, generateTime.AddMinutes(30));

                if (isEqual && (isValid < 0))
                {
                    bool res = handler.PasswordChanged(email, Convert.ToInt32(userId), password);
                    if (!res)
                    {
                        ModelState.AddModelError("message", "Something went wrong! Try again later");
                        ViewBag.IsLinkExpired = false;
                        return View();
                    }
                    ModelState.AddModelError("message", "Password reset Sucessfully! Please login.");
                    ViewBag.IsLinkExpired = false;
                    return View();
                }
            }
            ViewBag.IsLinkExpired = true;
            return View();

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