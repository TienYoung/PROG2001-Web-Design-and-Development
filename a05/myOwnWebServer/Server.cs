/*
 * FILE          : Server.cs
 * PROJECT       : PROG2001 - A5
 * PROGRAMMER    : Tian Yang
 * FIRST VERSION : 2024-11-19
 * DESCRIPTION   :
 *   This file defines the Server class, which handles incoming HTTP requests, processes them,
 *   and sends appropriate responses. The server supports basic HTTP methods and handles errors
 *   like 404 and 405. It uses the Logger class to record all activities and errors.
 */
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
    /*
     * NAME : Server
     * PURPOSE :
     *   The Server class implements the core functionality of the HTTP server. It listens for
     *   incoming client connections, parses HTTP requests, serves files, and sends appropriate
     *   responses. The class supports GET requests and handles file-serving logic, MIME types,
     *   and errors gracefully.
     */
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

        //
        // METHOD : ParseRequestHeader
        // DESCRIPTION :
        //   This method parses the raw HTTP request string and extracts key-value pairs
        //   representing the HTTP headers, method, URL, and version.
        // PARAMETERS :
        //   string request : The raw HTTP request string from the client.
        // RETURNS :
        //   Dictionary<string, string> : A dictionary containing the parsed HTTP headers.
        //
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

        //
        // METHOD : GenerateResponseString
        // DESCRIPTION :
        //   This method constructs an HTTP response string using the provided headers and response code.
        // PARAMETERS :
        //   Dictionary<string, string> headerDict : A dictionary containing HTTP response headers.
        // RETURNS :
        //   string : The formatted HTTP response string.
        //
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

        //
        // METHOD : ReceiveMessage
        // DESCRIPTION :
        //   This method reads a message from the given network stream. It retrieves up to the specified
        //   number of bytes and returns the message as a string.
        // PARAMETERS :
        //   NetworkStream netStream : The network stream to read from.
        //   int size                : The maximum number of bytes to read from the stream.
        // RETURNS :
        //   string : The message received from the network stream.
        //
        private string ReceiveMessage(NetworkStream netStream, int size)
        {
            byte[] buffer = new byte[size];
            StringBuilder message = new StringBuilder();
            int readSize = netStream.Read(buffer, 0, size);

            message.Append(Encoding.ASCII.GetString(buffer, 0, readSize));

            return message.ToString();
        }

        //
        // METHOD : ResponseFile
        // DESCRIPTION :
        //   This method sends an HTTP response to the client with the contents of the specified file.
        //   It includes appropriate headers, such as Content-Type and Content-Length, and writes
        //   the file content to the network stream.
        // PARAMETERS :
        //   NetworkStream netStream : The network stream to send the response to.
        //   string filename         : The path of the file to be sent.
        //   string contentType      : The MIME type of the file being served.
        // RETURNS :
        //   void : This method does not return a value.
        //
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

        //
        // METHOD : Response404
        // DESCRIPTION :
        //   This method sends a 404 Not Found HTTP response to the client. It includes a default
        //   "404.html" file in the response body if available.
        // PARAMETERS :
        //   NetworkStream netStream : The network stream to send the response to.
        // RETURNS :
        //   void : This method does not return a value.
        //
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

        //
        // METHOD : Response405
        // DESCRIPTION :
        //   This method sends a 405 Method Not Allowed HTTP response to the client. It includes headers
        //   indicating that the request method is not supported and an empty response body.
        // PARAMETERS :
        //   NetworkStream netStream : The network stream to send the response to.
        // RETURNS :
        //   void : This method does not return a value.
        //
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

        //
        // METHOD : Run
        // DESCRIPTION :
        //   This method runs the server in an infinite loop. It accepts client connections, reads
        //   HTTP requests, processes them, and sends appropriate responses. The method supports GET
        //   requests and handles errors like 404 and 405.
        // PARAMETERS :
        //   None
        // RETURNS :
        //   void : This method does not return a value.
        //
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
                        logger.Write("[IOException]", e.Message);
                        Response404(netStream);
                    }
                    catch (UnauthorizedAccessException e)
                    {
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
