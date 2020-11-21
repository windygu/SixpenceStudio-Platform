using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Logging
{
     /// <summary>
    /// 日志帮助类（log信息将会输出到 all.log 里）
    /// </summary>
    public static class LogUtils
    {
        #region 同步写日志
        public static void Info(string msg)
        {
            var log = LogFactory.GetLogger(LogType.Info);
            log.Info(msg);
        }
        public static void Debug(string msg)
        {
            var log = LogFactory.GetLogger(LogType.Info);
            log.Debug(msg);
        }
        public static void Warn(string msg)
        {
            var log = LogFactory.GetLogger(LogType.Info);
            log.Debug(msg);
        }
        public static void Error(string msg)
        {
            var log = LogFactory.GetLogger(LogType.Error);
            log.Error(msg);
        }
        public static void Error(string msg, Exception exception)
        {
            var log = LogFactory.GetLogger(LogType.Error);
            log.Error(msg, exception);
        }
        #endregion

        #region 异步写日志
        public static Task AsyncInfo(string msg)
        {
            return Task.Run(() => Info(msg));
        }
        public static Task AsyncDebug(string msg)
        {
            return Task.Run(() => Debug(msg));
        }
        public static Task AsyncWarn(string msg)
        {
            return Task.Run(() => Warn(msg));
        }
        public static Task AsyncError(string msg)
        {
            return Task.Run(() => Error(msg));
        }
        public static Task AsyncError(string msg, Exception exception)
        {
            return Task.Run(() => Error(msg, exception));
        }
        #endregion
    }
}
