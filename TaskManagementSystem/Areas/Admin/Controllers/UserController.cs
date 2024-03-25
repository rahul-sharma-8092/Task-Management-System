using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.DBHandler;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        Handler handler = new Handler();


        public ActionResult Index(int pageIndex = 1, int pageSize = 5)
        {
            UserWithPagination user = new UserWithPagination();
            user.UserList = handler.GetUserList(pageIndex, pageSize);

            user.Pagination.PageIndex = pageIndex;
            user.Pagination.PageSize = pageSize;
            user.Pagination.TotalRows = user.UserList[0].TotalUsers;
            user.Pagination.TotalPage = (int)Math.Ceiling((double)user.Pagination.TotalRows / pageSize);

            return View(user);
        }

        #region Create Admin | Employee | Client
        [HttpGet]
        public ActionResult Create()
        {
            GetRoleList(2);
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            GetRoleList(2);
            if (ModelState.IsValid)
            {
                bool IsEMailExist = handler.IsEmailExists(user.Email);
                if (IsEMailExist)
                {
                    ModelState.AddModelError("Email", "Email-Id already exists");
                    return View();
                }
                bool response = handler.CreateUser(user);
                if (response)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("message", "Error occurred while creating user.");
                return View(user);
            }
            return View();
        }
        #endregion

        #region Edit User
        [HttpGet]
        public ActionResult Edit(int id)
        {
            User user = handler.GetUserDetails("", id);

            GetRoleList(user.RoleId);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            GetRoleList(user.RoleId);

            if (ModelState.IsValid)
            {
                bool result = handler.UpdateUser(user);
                if (result)
                {
                    // User Updated
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("message", "User not updated due to some server issue.");
            }
            return View(user);
        }
        #endregion

        #region Delete
        [HttpGet]
        public ActionResult Delete(int id)
        {
            bool result = handler.DeleteUser(id);
            if (result)
            {
                // User Deleted;
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region GetRoleList
        private void GetRoleList(int roleId)
        {
            List<SelectListItem> roleList = new List<SelectListItem>();
            roleList.Add(new SelectListItem
            {
                Text = "Admin",
                Value = "1",
                Selected = 1 == roleId
            });
            roleList.Add(new SelectListItem
            {
                Text = "Employee",
                Value = "2",
                Selected = 2 == roleId
            });
            roleList.Add(new SelectListItem
            {
                Text = "Client",
                Value = "3",
                Selected = 3 == roleId
            });

            ViewBag.RoleList = roleList;
        }
        #endregion
    }
}