using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Filter;
using log4net.Layout;
using System;
using System.ComponentModel;

namespace SixpenceStudio.Platform.Logging
{
    /// <summary>
    /// 日志工厂类
    /// </summary>
    public class LogFactory
    {
        public static Logger GetLogger(string name = "")
        {
            if (string.IsNullOrEmpty(name)) return null;
            
            var repository = LogManager.GetRepository(name);
            if (repository == null)
            {
                // Pattern Layout
                PatternLayout layout = new PatternLayout("[%logger][%date]%message");
                // Level Filter
                LevelMatchFilter filter = new LevelMatchFilter();
                filter.LevelToMatch = Level.All;
                filter.ActivateOptions();
                // File Appender
                RollingFileAppender appender = new RollingFileAppender();
                // 目录
                appender.File = $"log\\";
                // 立即写入磁盘
                appender.ImmediateFlush = true;
                // true：追加到文件；false：覆盖文件
                appender.AppendToFile = true;
                // 新的日期或者文件大小达到上限，新建一个文件
                appender.RollingStyle = RollingFileAppender.RollingMode.Composite;
                // 文件大小达到上限，新建文件时，文件编号放到文件后缀前面
                appender.PreserveLogFileNameExtension = true;
                // 时间模式
                appender.DatePattern = $"{DateTime.Now.ToString("yyyyMMdd")} {name}.log";
                // 最小锁定模型以允许多个进程可以写入同一个文件
                appender.LockingModel = new FileAppender.MinimalLock();
                appender.Name = $"{name}Appender";
                appender.AddFilter(filter);
                appender.Layout = layout;
                appender.ActivateOptions();
                // 文件大小上限
                appender.MaximumFileSize = "200MB";
                // 设置无限备份=-1 ，最大备份数为30
                appender.MaxSizeRollBackups = 30;
                appender.StaticLogFileName = false;
                string repositoryName = $"{name}Repository";
                BasicConfigurator.Configure(LoggerManager.CreateRepository(repositoryName), appender);
                return new Logger(LogManager.GetLogger(repositoryName, name));
            }
            var log = LogManager.GetLogger(name);
            return new Logger(log);
        }

        public static ILog GetLogger(LogType logType)
        {
            switch (logType)
            {
                case LogType.Error:
                    return LogManager.GetLogger("Error");
                case LogType.Info:
                default:
                    return LogManager.GetLogger("Debug");
            }
        }
    }

    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        [Description("报错信息")]
        Error,
        [Description("信息")]
        Info
    }
}
