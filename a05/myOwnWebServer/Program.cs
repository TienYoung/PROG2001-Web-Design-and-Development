using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace myOwnWebServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string webRoot = "./webRoot";
            string webIP = "127.0.0.1";
            string webPort = "5300";
            foreach (string arg in args)
            {
                Console.WriteLine(arg);

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
