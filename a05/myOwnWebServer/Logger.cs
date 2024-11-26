/*
 * FILE          : Logger.cs
 * PROJECT       : PROG2001 - A5
 * PROGRAMMER    : Tian Yang
 * FIRST VERSION : 2024-11-19
 * DESCRIPTION   :
 *   This file defines the Logger class, which is used to log server activities and errors.
 *   It supports writing logs with different states and formats, including detailed headers
 *   for HTTP responses.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
{
    /*
     * NAME : Logger
     * PURPOSE :
     *   The Logger class provides functionality for logging messages and errors during the
     *   server's operation. It writes logs to a specified file, supporting both simple messages
     *   and detailed HTTP response logs.
     */
    internal class Logger
    {
        private string logFilename;
        internal Logger(string filename)
        {
            logFilename = filename;
            using (StreamWriter logStream = new StreamWriter(logFilename))
            {
                // Clear log when started
            }
        }

        //
        // METHOD : Write
        // DESCRIPTION :
        //   This method writes a log entry to the log file with the specified state and message.
        //   Each log entry includes a timestamp and the given state and message information.
        // PARAMETERS :
        //   string state  : The state or type of the log entry (e.g., "[INFO]", "[ERROR]").
        //   string message : The message to log.
        // RETURNS :
        //   void : This method does not return a value.
        //
        internal void Write(string state, string message)
        {
            using (StreamWriter logStream = new StreamWriter(logFilename, true))
            {
                logStream.WriteLine("{0} {1} {2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), state, message);
            }
        }

        //
        // METHOD : Write
        // DESCRIPTION :
        //   This method writes a detailed log entry to the log file with the specified state, message,
        //   and additional HTTP response headers. Each log entry includes a timestamp, the state, the
        //   main message, and key HTTP headers like Content-Type, Content-Length, Server, and Date.
        // PARAMETERS :
        //   string state  : The state or type of the log entry (e.g., "[INFO]", "[ERROR]").
        //   string message : The main message to log.
        //   Dictionary<string, string> headerDict : A dictionary containing HTTP response headers
        //                                           to include in the log entry.
        // RETURNS :
        //   void : This method does not return a value.
        //
        internal void Write(string state, string message, Dictionary<string, string> headerDict)
        {
            using (StreamWriter logStream = new StreamWriter(logFilename, true))
            {
                message += ", Content-Type: " + headerDict["Content-Type"];
                message += ", Content-Length: " + headerDict["Content-Length"];
                message += ", Server: " + headerDict["Server"];
                message += ", Date: " + headerDict["Date"];

                logStream.WriteLine("{0} {1} {2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), state, message);
            }
        }
    }
}
