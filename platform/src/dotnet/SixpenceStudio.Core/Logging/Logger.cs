using log4net;
using System;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Logging
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

        #region 同步写日志
        public void Debug(string msg)
        {
            _log.Debug(msg);
        }
        public void Info(string msg)
        {
            _log.Info(msg);
        }
        public void Warn(string msg)
        {
            _log.Warn(msg);
        }
        public void Error(string msg)
        {
            _log.Error(msg);
        }
        public void Error(string msg, Exception e)
        {
            _log.Error(msg, e);
        }
        #endregion

        #region 异步写日志
        public Task AsyncDebug(string msg)
        {
            return Task.Run(() => Debug(msg));
        }
        public Task AsyncInfo(string msg)
        {
            return Task.Run(() => Info(msg));
        }
        public Task AsyncError(string msg)
        {
            return Task.Run(() => Error(msg));
        }
        public void AsyncError(string msg, Exception e)
        {
            Task.Run(() => _log.Error(msg, e));
        }
        public void AsyncWarn(string msg)
        {
            Task.Run(() => Warn(msg));
        }
        #endregion
    }
}
