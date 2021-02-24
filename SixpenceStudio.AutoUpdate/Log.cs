using log4net;
using log4net.Core;
using SixpenceStudio.Core.Logging;
using SixpenceStudio.Core.MessageEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.AutoUpdate
{
    /// <summary>
    /// 日志记录
    /// </summary>
    public class Log : Observer
    {
        public Log()
        {
            log = LogFactory.GetLogger("AutoUpdate");
        }

        private ILog log { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public Level Level { get; set; }

        public void Info(string message)
        {
            this.Message = message;
            this.Level = Level.Info;
            log.Info(message);
            Notify();
        }

        public void Error(string message, Exception exception)
        {
            this.Message = message;
            this.Exception = exception;
            this.Level = Level.Error;
            log.Error(message, exception);
            Notify();
        }
    }

    /// <summary>
    /// 订阅者
    /// </summary>
    public class Subscriber : IObserver
    {
        public string Name { get; set; }
        public Action<string> Output;
        public void Receive(Object obj)
        {
            var log = obj as Log;
            var msg = string.Format("[{0}]{1}：{2}\r\n", log?.Level?.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), log?.Message);
            Output.Invoke(msg);
        }
    }
}
