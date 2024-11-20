using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace myOwnWebServer
{
    internal class Program
    {
        private static readonly string webRootPattern = @"-webRoot=([^\s]+)";
        private static readonly string webIPPattern = @"-webIP=([0-9]+\.[0-9]+\.[0-9]+\.[0-9]+)";
        private static readonly string webPortPattern = @"-webPort=([0-9]+)";

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
                        //while (true)
                        {
                            int readSize = netStream.Read(buffer, 0, client.ReceiveBufferSize);
                            byte[] data = buffer.Take(readSize).ToArray();

                            string request = System.Text.Encoding.ASCII.GetString(data);
                            Console.WriteLine(request);

                            FileStream fileStream = new FileStream("./index.html", FileMode.Open);

                            string response = "HTTP/1.1 200 OK\r\nContent-Type: text/html\r\nContent-Length: " + fileStream.Length + "\r\n\r\n";
                            data = System.Text.Encoding.ASCII.GetBytes(response);
                            netStream.Write(data, 0, data.Length);
                            
                            fileStream.CopyTo(netStream);
                        }
                    }
                }
                server.Stop();
                Console.WriteLine("Server Stopped!");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }
    }
}
