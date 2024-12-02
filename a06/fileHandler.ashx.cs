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
        private class JsonData
        {
            public string filename;
            public string content;
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            // Open file
            if (context.Request.HttpMethod == "GET")
            {
                // Get file name from URL.
                JsonData json = new JsonData();
                json.filename = context.Request.QueryString["filename"];

                string filename = Path.Combine(context.Server.MapPath("~/MyFiles"), json.filename);
                if (File.Exists(filename))
                {
                    json.content = File.ReadAllText(filename);
                    context.Response.Write(new JavaScriptSerializer().Serialize(json));
                }
            }
            // Save file
            else if (context.Request.HttpMethod == "POST")
            {
                try
                {
                    using (StreamReader streamReader = new StreamReader(context.Request.InputStream))
                    {
                        JsonData json = new JavaScriptSerializer().Deserialize<JsonData>(streamReader.ReadToEnd());
                        string filename = Path.Combine(context.Server.MapPath("~/MyFiles"), json.filename);
                        File.WriteAllText(filename, json.content);

                        // Save successfully, response No Content.
                        context.Response.StatusCode = 204;
                    }
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