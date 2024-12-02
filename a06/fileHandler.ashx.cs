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
    public class fileHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            if (context.Request.HttpMethod == "GET")
            {
                // Get file name from URL.
                string filename = context.Request.QueryString["filename"];

                filename = Path.Combine(context.Server.MapPath("~/MyFiles"), filename);
                if (File.Exists(filename))
                {
                    string content = File.ReadAllText(filename);
                    context.Response.Write(new JavaScriptSerializer().Serialize(new { content = content }));
                }
            }
        }

        public bool IsReusable => false;
    }
}