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

        // GET: Admin/Project
        [HttpGet]
        public ActionResult Index()
        {
            Project project = new Project();
            project.ProjectList = projectHandler.GetProjectList();

            return View(project);
        }

        // GET: Admin/Project/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Project/Create
        [HttpPost]
        public ActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                bool res = projectHandler.CreateProject(project);
                if (res)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(project);

        }

        // GET: Admin/Project/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Project project = new Project();

            DataTable table = projectHandler.GetProjectDetails(id);
            if (table.Rows.Count > 0)
            {
                project.Id = Convert.ToInt32(table.Rows[0]["Id"]);
                project.Name = Convert.ToString(table.Rows[0]["Name"]);
                project.Description = Convert.ToString(table.Rows[0]["Description"]);
                project.StartDate = Convert.ToDateTime(table.Rows[0]["StartDate"]);
                project.EndDate = Convert.ToDateTime(table.Rows[0]["EndDate"]);
                project.IsClosed = Convert.ToBoolean(table.Rows[0]["IsClosed"]);
            }
            return View(project);
        }

        // POST: Admin/Project/Edit/5
        [HttpPost]
        public ActionResult Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                bool res = projectHandler.EditProject(project);
                if (res)
                {
                    return RedirectToAction("index");
                }
            }
            return View(project);
        }

        // GET: Admin/Project/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {

            return View();
        }

        // POST: Admin/Project/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection connection = new SqlConnection(""))
            {
                using (SqlCommand command = new SqlCommand("DeleteProject", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }
    }
}