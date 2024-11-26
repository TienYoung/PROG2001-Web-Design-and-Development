/*
 * FILE          : Program.cs
 * PROJECT       : PROG2001 - A5
 * PROGRAMMER    : Tian Yang
 * FIRST VERSION : 2024-11-19
 * DESCRIPTION   :
 *   This file contains the main entry point for the myOwnWebServer project. It parses command-line
 *   arguments to configure the server settings, including the root directory, IP address, and port.
 *   The main method initializes and runs the server instance.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace myOwnWebServer
{
    /*
     * NAME : Program
     * PURPOSE :
     *   The Program class serves as the entry point for the myOwnWebServer project. It reads and
     *   processes command-line arguments, sets up configuration parameters, and starts the Server
     *   instance with the given settings.
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            string webRoot = "./webRoot";
            string webIP = "127.0.0.1";
            string webPort = "5300";
            foreach (string arg in args)
            {
                string[] parts = arg.Split('=');
                if (parts[0] == "-webRoot")
                {
                    webRoot = parts[1];
                }
                else if (parts[0] == "-webIP")
                {
                    webIP = parts[1];
                }
                else if (parts[0] == "-webPort")
                {
                    webPort = parts[1];
                }
            }
            
            Server server = new Server(webIP, webPort, webRoot);
            server.Run();
        }

    }
}
