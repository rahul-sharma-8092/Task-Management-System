using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    public class AttachmentController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(new Attachment());
        }

        [HttpPost]
        public ActionResult Index(Attachment attachment)
        {
            if (ModelState.IsValid)
            {
                string docsPath = MultipleDocsSave(attachment.Docs);
            }
            return View(attachment);
        }

        #region Multiple Docs Save
        public string MultipleDocsSave(List<HttpPostedFileBase> multipleFiles)
        {
            Random random = new Random();
            int str = random.Next();

            string docsPath = string.Empty;

            if (multipleFiles.Count > 0)
            {
                foreach (var file in multipleFiles)
                {
                    string fileName = file.FileName;
                    string path = Server.MapPath("~/Docs");

                    string fileExtension = Path.GetExtension(fileName);
                    try
                    {
                        fileName = "DOC" + ++str + fileExtension;

                        string fullPath = Path.Combine(path, fileName);
                        file.SaveAs(fullPath);

                        docsPath += "/Docs/" + fileName + "|";
                    }
                    catch (Exception) { }
                }
                return docsPath;
            }
            else
            {
                return docsPath;
            }
        }
        #endregion
    }
}