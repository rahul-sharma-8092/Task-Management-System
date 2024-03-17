using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Common
{
    public static class Service
    {
        #region ImageSave
        public static string ImageSave(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string fileName = file.FileName;
                string path = HttpContext.Current.Server.MapPath("~/Docs/Images");

                string fileExtension = Path.GetExtension(fileName);
                try
                {
                    Random random = new Random();
                    fileName = "IMG" + random.Next() + fileExtension;

                    string fullPath = Path.Combine(path, fileName);
                    file.SaveAs(fullPath);

                    return "/Docs/Images/" + fileName;
                }
                catch (Exception)   
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        #endregion
    }
}