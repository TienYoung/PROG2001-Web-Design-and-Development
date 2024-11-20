using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace myOwnWebServer
{
    internal class Program
    {
        static readonly string webRootPattern = @"-webRoot=([^\s]+)";
        static readonly string webIPPattern = @"-webIP=([0-9]+\.[0-9]+\.[0-9]+\.[0-9]+)";
        static readonly string webPortPattern = @"-webPort=([0-9]+)";

        static void Main(string[] args)
        {
            string webRoot = Regex.Match(args[0], webRootPattern).Groups[1].Value; ;
            string webIP = Regex.Match(args[1], webIPPattern).Groups[1].Value;
            string webPort = Regex.Match(args[2], webPortPattern).Groups[1].Value;
        }
    }
}
