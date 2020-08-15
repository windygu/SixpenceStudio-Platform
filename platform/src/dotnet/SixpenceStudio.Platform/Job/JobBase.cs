using Quartz;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Job
{
    public abstract class JobBase : IJob
    {
        /// <summary>
        /// 作业名
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// 作业描述
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// 调度时间
        /// </summary>
        public abstract string CronExperssion { get; }
        
        /// <summary>
        /// 任务
        /// </summary>
        public abstract void Run(IPersistBroker broker);

        /// <summary>
        /// 任务执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                LogUtils.DebugLog($"作业：{Name} 开始执行\r\n");

                var broker = new PersistBroker();
                Run(broker);
                
                // 更新下次执行时间
                var nextTime = JobHelpers.GetJobNextTime(Name);
                var nextTimeSql = "";
                var paramList = new Dictionary<string, object>() {
                    { "@time", DateTime.Now },
                    { "@name", Name }
                };
                if (!string.IsNullOrEmpty(nextTime))
                {
                    paramList.Add("@nextTime", Convert.ToDateTime(nextTime));
                    nextTimeSql = ", nextruntime = @nextTime";
                }

                broker.Execute($"UPDATE job SET lastruntime = @time {nextTimeSql} WHERE name = @name", paramList);
                LogUtils.DebugLog($"作业：{Name} 执行结束\r\n");
            });
        }
    }
}
