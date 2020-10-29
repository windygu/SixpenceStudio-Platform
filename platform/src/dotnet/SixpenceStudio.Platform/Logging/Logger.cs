using log4net;
using System;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Logging
{
    /// <summary>
    /// 日志
    /// </summary>
    public class Logger
    {
        private ILog _log;

        public Logger(ILog log)
        {
            _log = log;
        }

        public void Debug(string msg)
        {
            Task.Run(() => _log.Debug(msg));
        }

        public void Info(string msg)
        {
            Task.Run(() => _log.Info(msg));
        }

        public void Error(string msg)
        {
            Task.Run(() => _log.Error(msg));
        }

        public void Error(string msg, Exception e)
        {
            Task.Run(() => _log.Error(msg, e));
        }

        public void Warn(string msg)
        {
            Task.Run(() => _log.Warn(msg));
        }
    }
}
