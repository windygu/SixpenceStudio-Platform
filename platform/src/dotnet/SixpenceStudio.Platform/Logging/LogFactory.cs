using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace SixpenceStudio.Platform.Logging
{
    public class LogFactory
    {
        public static ILog GetLogInstance(string logName = "")
        {
            return LogManager.GetLogger(logName);
        }
    }

    public class LogUtils
    {
        public static string FormatDictonary(IDictionary<string, object> paramList)
        {
            if (paramList == null)
            {
                return "\r\n";
            }
            var list = new List<string>();
            foreach (var item in paramList)
            {
                var str = $"{item.Key}: {item.Value}";
                list.Add(str);

            }
            return "\r\n" + string.Join("\r\n", list) + "\r\n";
        }

        public static void InfoLog(object msg)
        {
            var log = LogFactory.GetLogInstance("Info");
            Task.Run(() => log.Info(msg));
        }

        public static void DebugLog(object msg) 
        {
            var log = LogFactory.GetLogInstance("Debug");
            Task.Run(() => log.Debug(msg));
        }

        public static void WarnLog(object msg)
        {
            var log = LogFactory.GetLogInstance("Warn");
            Task.Run(() => log.Debug(msg));
        }

        public static void ErrorLog(object msg)
        {
            var log = LogFactory.GetLogInstance("Error");
            Task.Run(() => log.Error(msg));
        }

        public static void ErrorLog(object msg, Exception exception)
        {
            var log = LogFactory.GetLogInstance("Error");
            Task.Run(() => log.Error(msg, exception));
        }
    }
}
