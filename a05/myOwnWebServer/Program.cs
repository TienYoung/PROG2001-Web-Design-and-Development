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
        private static readonly string webRootPattern = @"-webRoot=([^\s]+)";
        private static readonly string webIPPattern = @"-webIP=([0-9]+\.[0-9]+\.[0-9]+\.[0-9]+)";
        private static readonly string webPortPattern = @"-webPort=([0-9]+)";

        static void Main(string[] args)
        {
            string webRoot = Regex.Match(args[0], webRootPattern).Groups[1].Value;
            string webIP = Regex.Match(args[1], webIPPattern).Groups[1].Value;
            string webPort = Regex.Match(args[2], webPortPattern).Groups[1].Value;

            TcpListener server = new TcpListener(IPAddress.Parse(webIP), Convert.ToInt32(webPort));
            server.Start();
            while (true)
            {
                using (TcpClient client = server.AcceptTcpClient())
                {
                    using (NetworkStream netStream = client.GetStream())
                    {
                        byte[] buffer = new byte[client.ReceiveBufferSize];

                        int readSize = netStream.Read(buffer, 0, client.ReceiveBufferSize);
                        byte[] data = buffer.Take(readSize).ToArray();

                        string request = System.Text.Encoding.ASCII.GetString(data);
                        Console.WriteLine($"Request:\n{request}");

                        Dictionary<string, string> headers = new Dictionary<string, string>();

                        string[] lines = request.Split('\n');
                        string[] parts = lines[0].Split(' ');
                        if (parts.Length != 3)
                        {
                            throw new Exception("Header invalid!");
                        }
                        string method = parts[0];
                        string path = parts[1];
                        string version = parts[2];

                        for (int i = 1; i < lines.Length; i++)
                        {
                            if (String.IsNullOrWhiteSpace(lines[i]))
                            {
                                continue;
                            }

                            parts = lines[i].Split(new[] { ": " }, 2, StringSplitOptions.None);
                            if (parts.Length != 2)
                            {
                                throw new Exception("Header invalid!");
                            }

                            headers.Add(parts[0], parts[1]);
                        }

                        string response = null;
                        if (method == "GET")
                        {
                            using (FileStream fileStream = new FileStream("." + path, FileMode.Open))
                            {
                                response = "HTTP/1.1 200 OK\r\nContent-Type: text/html\r\nContent-Length: " + fileStream.Length + "\r\n\r\n";
                                data = System.Text.Encoding.ASCII.GetBytes(response);
                                netStream.Write(data, 0, data.Length);
                                fileStream.CopyTo(netStream);
                            }
                        }
                        else
                        {
                            response = "HTTP/1.1 404 Not Found\r\n";
                            response += "Connection: Close\r\n";
                            response += "Content-Length: 0\r\n";
                            response += "\r\n";
                            data = System.Text.Encoding.ASCII.GetBytes(response);
                            netStream.Write(data, 0, data.Length);
                        }
                    }
                }
            }
        }
    }
}
