using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

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
            // Open file
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
            // Save file
            else if (context.Request.HttpMethod == "POST")
            {
                string filename = null;
                string content = null;
                try
                {
                    using (StreamReader streamReader = new StreamReader(context.Request.InputStream))
                    {
                        string body = streamReader.ReadToEnd();
                        dynamic requestData = new JavaScriptSerializer().Deserialize<dynamic>(body);
                        filename = requestData["filename"];
                        content = requestData["content"];
                    }
                    filename = Path.Combine(context.Server.MapPath("~/MyFiles"), filename);
                    File.WriteAllText(filename, content);

                    // Save successfully, response No Content.
                    context.Response.StatusCode = 204;
                }
                catch 
                {
                    // Save failed, response Internal Server Error.
                    context.Response.StatusCode = 500;
                }
            }
        }

        public bool IsReusable => false;
    }
}