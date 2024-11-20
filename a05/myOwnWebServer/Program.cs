using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace myOwnWebServer
{
    internal class Program
    {
        static readonly string webRootPattern = @"-webRoot=([^\s]+)";
        static readonly string webIPPattern = @"-webIP=([0-9]+\.[0-9]+\.[0-9]+\.[0-9]+)";
        static readonly string webPortPattern = @"-webPort=([0-9]+)";

        static void Main(string[] args)
        {
            string webRoot = Regex.Match(args[0], webRootPattern).Groups[1].Value;
            string webIP = Regex.Match(args[1], webIPPattern).Groups[1].Value;
            string webPort = Regex.Match(args[2], webPortPattern).Groups[1].Value;

            try
            {
                TcpListener server = new TcpListener(IPAddress.Parse(webIP), Convert.ToInt32(webPort));
                server.Start();
                Console.WriteLine("Server Started!");
                using (TcpClient client = server.AcceptTcpClient())
                {
                    using (NetworkStream netStream = client.GetStream())
                    {
                        byte[] buffer = new byte[client.ReceiveBufferSize];
                        int readSize = netStream.Read(buffer, 0, client.ReceiveBufferSize);
                        byte[] data = new byte[readSize];
                        Array.Copy(buffer, data, readSize);

                        string httpHeader = System.Text.Encoding.ASCII.GetString(data);
                        Console.WriteLine(httpHeader);
                    }
                }
                server.Stop();
                Console.WriteLine("Server Stopped!");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
            }
        }
    }
}
