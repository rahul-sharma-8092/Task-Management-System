using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManagementSystem.Areas.Users.Controllers
{
    public class HomeController : Controller
    {
        // GET: Users/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}