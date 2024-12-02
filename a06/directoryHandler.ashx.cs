/*
 * FILE          : directoryHandler.ashx.cs
 * PROJECT       : PROG2001 - Assignment #6
 * PROGRAMMER    : Tian Yang
 * FIRST VERSION : 2024-12-02
 * DESCRIPTION   :
 *   This file defines the directoryHandler class, an HTTP handler that retrieves a list
 *   of all files in the specified server directory. The handler converts the file list
 *   into a JSON response for client-side consumption.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace a06
{
    /*
     * NAME : directoryHandler
     * PURPOSE :
     *   The directoryHandler class implements the IHttpHandler interface to handle HTTP
     *   requests for directory listing. It retrieves and returns a list of files from the
     *   server directory in JSON format, allowing client-side applications to access the
     *   file data easily.
     */
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

            // response OK.
            context.Response.StatusCode = 200;
            // Convert object to JSON string.
            context.Response.Write(new JavaScriptSerializer().Serialize(files));
        }

        public bool IsReusable => false;
    }
}