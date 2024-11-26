using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
{
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

        internal void Write(string state, string message)
        {
            using (StreamWriter logStream = new StreamWriter(logFilename, true))
            {
                logStream.WriteLine("{0} {1} {2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), state, message);
            }
        }
    }
}
