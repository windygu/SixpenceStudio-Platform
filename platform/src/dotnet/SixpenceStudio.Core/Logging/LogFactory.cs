using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Filter;
using log4net.Layout;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SixpenceStudio.Core.Logging
{
    /// <summary>
    /// 日志工厂类
    /// </summary>
    public class LogFactory
    {
        private static Dictionary<string, ILog> loggers = new Dictionary<string, ILog>();
        private static readonly Object lockObject = new object();

        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ILog GetLogger(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;

            if (loggers.ContainsKey(name))
            {
                return loggers[name];
            }
            else
            {
                lock (lockObject)
                {
                    if (loggers.ContainsKey(name))
                    {
                        return loggers[name];
                    }
                    var logger = CreateLoggerInstance(name);
                    loggers.Add(name, logger);
                    return logger;
                }
            }
        }

        /// <summary>
        /// 创建日志实例
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static ILog CreateLoggerInstance(string name)
        {
            // Pattern Layout
            PatternLayout layout = new PatternLayout("[%logger][%date]%message\r\n");
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
            appender.DatePattern = $"yyyyMMdd\" {name}.log\"";
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
            ILoggerRepository repository = LoggerManager.CreateRepository(repositoryName);
            BasicConfigurator.Configure(repository, appender);
            var logger = LogManager.GetLogger(repositoryName, name);
            return logger;
        }

        internal static ILog GetLogger(LogType logType)
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
