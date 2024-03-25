using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Common
{
    public static class Logger
    {
        public static void ForgotPassLinksLogger(string links)
        {
            string path = ConfigurationManager.AppSettings["ForgotPassLinks"].ToString();

            StreamWriter writer = new StreamWriter(path, true);

            writer.WriteLine(links.Trim());

            writer.Dispose();
        }
    }
}