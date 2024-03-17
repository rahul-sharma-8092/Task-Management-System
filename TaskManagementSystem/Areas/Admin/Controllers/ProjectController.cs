using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.Models;
using TaskManagementSystem.DBHandler;

namespace TaskManagementSystem.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProjectController : Controller
    {
        ProjectHandler projectHandler = new ProjectHandler();
        Handler handler = new Handler();

        [HttpGet]
        public ActionResult Index()
        {
            Project project = new Project();
            project.ProjectList = projectHandler.GetProjectList();
            return View(project);
        }

        #region Project Create
        [HttpGet]
        public ActionResult Create()
        {
            Project project = new Project();
            project.StartDate = DateTime.Now;
            project.EndDate = DateTime.Now;
            return View(project);
        }

        [HttpPost]
        public ActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                bool IsEndDateValid = DateTime.Compare(project.StartDate, project.EndDate) <= 0;
                if (!IsEndDateValid)
                {
                    ModelState.AddModelError("EndDate", "End date must be Greater than or Equal to Start date.");
                    return View(project);
                }

                User user = handler.GetUserDetails(User.Identity.Name);

                bool res = projectHandler.CreateProject(project, user.UserId);
                if (res)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(project);
        }
        #endregion

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Project project = new Project();

            DataTable table = projectHandler.GetProjectDetails(id);
            if (table.Rows.Count > 0)
            {
                project.Id = Convert.ToInt32(table.Rows[0]["ProjectId"]);
                project.Name = Convert.ToString(table.Rows[0]["ProjectName"]);
                project.Description = Convert.ToString(table.Rows[0]["ProjectDescription"]);
                project.StartDate = Convert.ToDateTime(table.Rows[0]["StartDate"]);
                project.EndDate = Convert.ToDateTime(table.Rows[0]["EndDate"]);
                project.Status = Convert.ToString(table.Rows[0]["Status"]);
                project.IsClosed = Convert.ToBoolean(table.Rows[0]["IsClosed"]);
                project.CreatedBy = Convert.ToString(table.Rows[0]["CreatedBy"]);
                project.UpdatedBy = Convert.ToString(table.Rows[0]["ModifiedBy"]);
            }

            GetStatusList(project.Status);
            return View(project);
        }

        [HttpPost]
        public ActionResult Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                User user = handler.GetUserDetails(User.Identity.Name);
                project.IsClosed = project.Status == "Closed" ? true : false;
                bool res = projectHandler.EditProject(project, user.UserId);
                if (res)
                {
                    return RedirectToAction("index");
                }
            }
            return View(project);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            User user = handler.GetUserDetails(User.Identity.Name);
            bool result = projectHandler.DeleteProject(id, user.UserId);
            if (result)
            {
                //Project Deleted
            }
            return RedirectToAction("Index");
        }

        public void GetStatusList(string status)
        {
            List<SelectListItem> statusList = new List<SelectListItem>();
            statusList.Add(new SelectListItem
            {
                Text = "Open",
                Value = "Open",
                Selected = "Open" == status,
            });
            statusList.Add(new SelectListItem
            {
                Text = "Closed",
                Value = "Closed",
                Selected = "Closed" == status,
            });

            ViewBag.Status = statusList;

            statusList.Clear();
        }
    }
}