﻿using System;
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
                        Console.WriteLine(request);

                        Dictionary<string, string> requestDict = ParseRequestHeader(request);

                        string response = null;
                        if (requestDict["Method"] == "GET")
                        {
                            

                            string url = "./webroot";
                            if (requestDict["URL"] == "/")
                            {
                                url += "/index.html";
                            }
                            else
                            {
                                url += requestDict["URL"];
                            }

                            string contentType = null;
                            string ext = Path.GetExtension(url);
                            switch (ext)
                            {
                                case ".gif":
                                    contentType = "image/gif";
                                    break;
                                case ".htm":
                                case ".html":
                                case ".htmls":
                                    contentType = "text/html";
                                    break;
                                case ".jpg":
                                case ".jpeg":
                                    contentType = "image/jpeg";
                                    break;
                                case ".png":
                                    contentType = "image/png";
                                    break;
                                case ".txt":
                                    contentType = "image/plain";
                                    break;
                            }

                            try
                            {
                                using (FileStream fileStream = new FileStream(url, FileMode.Open))
                                {
                                    Dictionary<string, string> responseDict = new Dictionary<string, string>();
                                    responseDict["Response"] = "HTTP/1.1 200 OK";
                                    responseDict["Connection"] = "Close";
                                    responseDict["Content-Type"] = contentType;
                                    responseDict["Content-Length"] = fileStream.Length.ToString();
                                    responseDict["Date"] = DateTime.Now.ToUniversalTime().ToString("r");
                                    responseDict["Server"] = "myOwnServer";
                                    response = GenerateResponseString(responseDict);

                                    data = System.Text.Encoding.ASCII.GetBytes(response);
                                    netStream.Write(data, 0, data.Length);
                                    fileStream.CopyTo(netStream);
                                }
                            }
                            catch (IOException e)
                            {
                                Console.Error.WriteLine(e.Message);

                                using (FileStream fileStream = new FileStream("./webroot/404.html", FileMode.Open))
                                {
                                    Dictionary<string, string> responseDict = new Dictionary<string, string>();
                                    responseDict["Response"] = "HTTP/1.1 404 Not Found";
                                    responseDict["Connection"] = "Close";
                                    responseDict["Content-Type"] = "text/html";
                                    responseDict["Content-Length"] = fileStream.Length.ToString();
                                    responseDict["Date"] = DateTime.Now.ToUniversalTime().ToString("r");
                                    responseDict["Server"] = "myOwnServer";
                                    response = GenerateResponseString(responseDict);

                                    data = System.Text.Encoding.ASCII.GetBytes(response);
                                    netStream.Write(data, 0, data.Length);
                                    fileStream.CopyTo(netStream);
                                }
                            }
                        }
                        else
                        {
                            using (FileStream fileStream = new FileStream("./webroot/404.html", FileMode.Open))
                            {
                                Dictionary<string, string> responseDict = new Dictionary<string, string>();
                                responseDict["Response"] = "HTTP/1.1 404 Not Found";
                                responseDict["Connection"] = "Close";
                                responseDict["Content-Length"] = "0";
                                responseDict["Content-Type"] = "text/html";
                                responseDict["Date"] = DateTime.Now.ToUniversalTime().ToString("r");
                                responseDict["Server"] = "myOwnServer";
                                response = GenerateResponseString(responseDict);

                                data = System.Text.Encoding.ASCII.GetBytes(response);
                                netStream.Write(data, 0, data.Length);
                            }
                        }
                    }
                }
            }
        }

        static Dictionary<string, string> ParseRequestHeader(string request)
        {
            Dictionary<string, string> headerDict = new Dictionary<string, string>();

            string[] lines = request.Split('\n');
            string[] parts = lines[0].Split(' ');
            if (parts.Length != 3)
            {
                throw new Exception("Request invalid!");
            }
            headerDict["Method"] = parts[0];
            headerDict["URL"] = parts[1];
            headerDict["Version"] = parts[2];

            for (int i = 1; i < lines.Length; i++)
            {
                if (String.IsNullOrWhiteSpace(lines[i]))
                {
                    continue;
                }

                parts = lines[i].Split(new[] { ": " }, 2, StringSplitOptions.None);
                if (parts.Length != 2)
                {
                    Console.Error.WriteLine("Request message error: {0}", lines[i]);
                    continue;
                }

                headerDict.Add(parts[0], parts[1]);
            }
            return headerDict;
        }

        static string GenerateResponseString(Dictionary<string, string> headerDict)
        {
            string response = headerDict["Response"] + "\r\n";
            headerDict.Remove("Response");
            foreach (var message in headerDict)
            {
                response += message.Key + ": " + message.Value + "\r\n";
            }
            response += "\r\n";

            return response;
        }
    }
}
