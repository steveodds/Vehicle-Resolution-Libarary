using System;
using System.IO;
using System.Text;

namespace Vehicle_Resolution_Libarary
{
    internal class Logger
    {
        private static Logger _logger;
        private readonly string _logFile;
        private bool isStart = true;

        private Logger() 
        {
            _logFile = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\VRL-LOG.log";
            if (!File.Exists(_logFile))
                File.Create(_logFile);
        }
        public static Logger GetInstance()
        {
            if (_logger is null)
                _logger = new Logger();

            return _logger;
        }

        public void Log(string message)
        {
            var builder = new StringBuilder();
            if (isStart)
            {
                builder.AppendLine(GenerateLogHeader());
                isStart = false;
            }
            builder.AppendLine($"[{DateTime.Now}]: {message}");
            WriteToLog(builder.ToString());
        }

        public void MarkAsEnd()
        {
            isStart = true;
        }

        private void WriteToLog(string block)
        {
            using(var file = new StreamWriter(_logFile, true))
            {
                file.WriteLine(block);
            }
        }

        private string GenerateLogHeader()
        {
            var header = new StringBuilder();
            header.AppendLine("===========================================");
            header.AppendLine($"Generating entry at {DateTime.Now}");
            header.AppendLine("===========================================");
            return header.ToString();
        }
    }
}
