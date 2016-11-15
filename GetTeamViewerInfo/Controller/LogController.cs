using System;
using System.IO;
using System.Reflection;

namespace GetTeamViewerInfo.Controller
{
    public class LogController
    {
        private static FileStream _fs;
        private static StreamWriterWithTimestamp _sw;
        public static string NowDate;

        //打开日志文件
        public static bool OpenLogFile()
        {
            if (_sw != null)
            {
                _sw.Close();
                _sw.Dispose();
            }
            if (_fs != null)
            {
                _fs.Close();
                _fs.Dispose();
            }
            NowDate = DateTime.Now.ToString("yyyyMMdd");
            try
            {
                var logFileName = "Gti" + NowDate + ".log";
                var logFilePath = Assembly.GetExecutingAssembly().Location + "/log/";
                if (!Directory.Exists(logFilePath))
                    Directory.CreateDirectory(logFilePath);
                _fs = new FileStream(logFilePath + logFileName, FileMode.Append);
                _sw = new StreamWriterWithTimestamp(_fs) {AutoFlush = true};
                Console.SetOut(_sw);
                Console.SetError(_sw);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        private static void WriteToLogFile(object o)
        {
            try
            {
                Console.WriteLine(o);
            }
            catch (ObjectDisposedException)
            {
                OpenLogFile();
                WriteToLogFile(o);
            }
        }

        public static void Error(object o)
        {
            WriteToLogFile("[Application Error]" + o);
        }

        public static void Info(object o)
        {
            WriteToLogFile("[Application Info]" + o);
        }
    }

    public class StreamWriterWithTimestamp : StreamWriter
    {
        public StreamWriterWithTimestamp(Stream stream) : base(stream)
        {
        }

        private string GetTimestamp()
        {
            return "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] ";
        }

        public override void WriteLine(string value)
        {
            base.WriteLine(GetTimestamp() + value);
        }

        public override void Write(string value)
        {
            base.Write(GetTimestamp() + value);
        }
    }
}
