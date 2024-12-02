/*
 * FILE          : fileHandler.ashx.cs
 * PROJECT       : PROG2001 - Assignment #6
 * PROGRAMMER    : Tian Yang
 * FIRST VERSION : 2024-12-02
 * DESCRIPTION   :
 *   This file defines the fileHandler class, an HTTP handler that processes requests for
 *   file operations. It supports two main functionalities:
 *   - GET: Retrieves the content of a specified file in JSON format.
 *   - POST: Saves content to a specified file, creating or overwriting it.
 *   The handler responds with appropriate HTTP status codes based on the success or failure
 *   of the operation.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace a06
{
    /*
     * NAME : fileHandler
     * PURPOSE :
     *   The fileHandler class implements the IHttpHandler interface and provides file
     *   manipulation functionalities. It handles HTTP GET requests to retrieve file content
     *   and POST requests to save content to a file. This class ensures appropriate error
     *   handling and JSON-based responses.
     */
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
                if (!File.Exists(filename))
                {
                    // File not exist, response No Found.
                    context.Response.StatusCode = 404;
                    return;
                }

                try
                {
                    json.content = File.ReadAllText(filename);
                }
                catch
                {                    
                    // Open failed, response No Found.
                    context.Response.StatusCode = 404;
                    return;
                }

                // Save successfully, response OK.
                context.Response.StatusCode = 200;
                context.Response.Write(new JavaScriptSerializer().Serialize(json));
                return;
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
                        return;
                    }
                }
                catch
                {
                    // Save failed, response Internal Server Error.
                    context.Response.StatusCode = 500;
                    return;
                }
            }
        }

        public bool IsReusable => false;
    }
}