using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Logging
{
     /// <summary>
    /// 日志帮助类（log信息将会输出到 all.log 里）
    /// </summary>
    public static class LogUtils
    {
        public static void Info(string msg)
        {
            var log = LogFactory.GetLogger(LogType.Info);
            Task.Run(() => log.Info(msg));
        }

        public static void Debug(string msg)
        {
            var log = LogFactory.GetLogger(LogType.Info);
            Task.Run(() => log.Debug(msg));
        }

        public static void Warn(string msg)
        {
            var log = LogFactory.GetLogger(LogType.Info);
            Task.Run(() => log.Debug(msg));
        }

        public static void Error(string msg)
        {
            var log = LogFactory.GetLogger(LogType.Error);
            Task.Run(() => log.Error(msg));
        }

        public static void Error(string msg, Exception exception)
        {
            var log = LogFactory.GetLogger(LogType.Error);
            Task.Run(() => log.Error(msg, exception));
        }
    }
}
