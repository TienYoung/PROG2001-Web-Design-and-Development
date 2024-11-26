using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
{
    internal class Server
    {
        private Logger logger = null;
        private TcpListener listener = null;
        private string root;

        internal Server(string ip, string port, string webRoot)
        {
            logger = new Logger("./myOwnWebServer.log");
            listener = new TcpListener(IPAddress.Parse(ip), Convert.ToInt32(port));
            listener.Start();
            root = webRoot;
            logger.Write("[SERVER STARTED]", ip + ":" + port + " " + Path.GetFullPath(webRoot));
        }

        private Dictionary<string, string> ParseRequestHeader(string request)
        {
            Dictionary<string, string> headerDict = new Dictionary<string, string>();

            string[] lines = request.Split(new[] { '\r', '\n' });
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
                    logger.Write("[ERROR]", lines[i]);
                    continue;
                }

                headerDict.Add(parts[0], parts[1]);
            }
            return headerDict;
        }

        private string GenerateResponseString(Dictionary<string, string> headerDict)
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

        private string ReceiveMessage(NetworkStream netStream, int size)
        {
            byte[] buffer = new byte[size];
            StringBuilder message = new StringBuilder();
            int readSize = netStream.Read(buffer, 0, size);

            message.Append(Encoding.ASCII.GetString(buffer, 0, readSize));

            return message.ToString();
        }

        private void ResponseFile(NetworkStream netStream, string filename, string contentType)
        {
            using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                Dictionary<string, string> responseDict = new Dictionary<string, string>();
                responseDict["Response"] = "HTTP/1.1 200 OK";
                responseDict["Connection"] = "Close";
                responseDict["Content-Type"] = contentType;
                responseDict["Content-Length"] = fileStream.Length.ToString();
                responseDict["Date"] = DateTime.Now.ToUniversalTime().ToString("r");
                responseDict["Server"] = "myOwnServer";
                string response = GenerateResponseString(responseDict);

                byte[] data = System.Text.Encoding.ASCII.GetBytes(response);
                netStream.Write(data, 0, data.Length);
                fileStream.CopyTo(netStream);

                logger.Write("[RESPONSE]", "HTTP/1.1 200 OK", responseDict);
            }
        }

        private void Response404(NetworkStream netStream)
        {
            using (FileStream fileStream = new FileStream(root + "/404.html", FileMode.Open, FileAccess.Read))
            {
                Dictionary<string, string> responseDict = new Dictionary<string, string>();
                responseDict["Response"] = "HTTP/1.1 404 Not Found";
                responseDict["Connection"] = "Close";
                responseDict["Content-Type"] = "text/html";
                responseDict["Content-Length"] = fileStream.Length.ToString();
                responseDict["Date"] = DateTime.Now.ToUniversalTime().ToString("r");
                responseDict["Server"] = "myOwnServer";
                string response = GenerateResponseString(responseDict);

                byte[] data = System.Text.Encoding.ASCII.GetBytes(response);
                netStream.Write(data, 0, data.Length);
                fileStream.CopyTo(netStream);

                logger.Write("[RESPONSE]", "HTTP/1.1 404 Not Found");
            }
        }

        private void Response405(NetworkStream netStream)
        {
            Dictionary<string, string> responseDict = new Dictionary<string, string>();
            responseDict["Response"] = "HTTP/1.1 405 Method Not Allowed";
            responseDict["Connection"] = "Close";
            responseDict["Content-Length"] = "0";
            responseDict["Date"] = DateTime.Now.ToUniversalTime().ToString("r");
            responseDict["Server"] = "myOwnServer";
            string response = GenerateResponseString(responseDict);

            byte[] data = System.Text.Encoding.ASCII.GetBytes(response);
            netStream.Write(data, 0, data.Length);

            logger.Write("[RESPONSE]", "HTTP/1.1 405 Method Not Allowed");
        }

        internal void Run()
        {
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream netStream = client.GetStream();
                string request = ReceiveMessage(netStream, client.ReceiveBufferSize);
                logger.Write("[REQUEST]", request.Split(new[] { '\r', '\n' })[0]);

                Dictionary<string, string> requestDict = ParseRequestHeader(request);
                if (requestDict["Method"] == "GET")
                {
                    string url = root;
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
                        ResponseFile(netStream, url, contentType);
                    }
                    catch (IOException e)
                    {
                        Console.Error.WriteLine(e);
                        logger.Write("[IOException]", e.Message);
                        Response404(netStream);
                    }
                    catch (UnauthorizedAccessException e)
                    {
                        Console.Error.WriteLine(e);
                        logger.Write("[UnauthorizedAccessException]", e.Message);
                        Response404(netStream);
                    }
                }
                else
                {
                    Response405(netStream);
                }

                // Clear resources
                netStream.Close();
                client.Close();
            }
        }
    }
}
