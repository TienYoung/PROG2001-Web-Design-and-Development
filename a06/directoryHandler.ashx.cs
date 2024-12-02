using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace a06
{
    /// <summary>
    /// Summary description for fileHandler
    /// </summary>
    public class directoryHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            // Map server path to physical path.
            string myFiles = context.Server.MapPath("~/MyFiles");
            string[] files = null;

            if (Directory.Exists(myFiles))
            {
                files = Directory.GetFiles(myFiles);
                files = Array.ConvertAll(files, Path.GetFileName); // Remove directory path.
            }

            // Convert object to JSON string.
            context.Response.Write(new JavaScriptSerializer().Serialize(files));
        }

        public bool IsReusable => false;
    }
}