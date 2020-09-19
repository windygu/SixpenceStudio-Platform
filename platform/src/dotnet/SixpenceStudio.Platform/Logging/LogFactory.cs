using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace SixpenceStudio.Platform.Logging
{
    /// <summary>
    /// 日志工厂类
    /// </summary>
    public class LogFactory
    {
        /// <summary>
        /// 获取日志实例
        /// </summary>
        /// <param name="logName"></param>
        /// <returns></returns>
        public static ILog GetLogInstance(string logName = "")
        {
            return LogManager.GetLogger(logName);
        }
    }

    /// <summary>
    /// 日志帮助类（常用log）
    /// </summary>
    public static class LogUtils
    {
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
