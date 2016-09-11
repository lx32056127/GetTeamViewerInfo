using System;
using System.IO;

namespace TeamViewerInfoServer.Controller
{
    public class LogController
    {
        public static string NowDate;

        private static void WriteToLogFile(object o)
        {
            NowDate = DateTime.Now.ToString("yyyyMMdd");
            try
            {
                var logFileName = "TVS" + NowDate + ".log";
                var logFilePath = System.Web.HttpContext.Current.Server.MapPath("/") + "/log/";
                if (!Directory.Exists(logFilePath))
                    Directory.CreateDirectory(logFilePath);
                using (var fs = new FileStream(logFilePath + logFileName, FileMode.Append))
                {
                    using (var sw = new StreamWriterWithTimestamp(fs))
                    {
                        sw.WriteLine(o.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
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
