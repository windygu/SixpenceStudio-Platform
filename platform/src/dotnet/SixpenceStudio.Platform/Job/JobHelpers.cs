using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Job
{
    public class JobHelpers
    {
        /// <summary>
        /// 任务调度的使用过程
        /// </summary>
        /// <returns></returns>
        private async static Task Run<T>(string cronExperssion)
            where T : IJob
        {
            // 1.创建scheduler的引用
            var schedFact = new StdSchedulerFactory();
            var sched = await schedFact.GetScheduler();
            await sched.Start();

            // 3.创建 job
            var job = JobBuilder.Create<T>()
                .Build();

            // 4.创建 trigger
            ITrigger trigger = TriggerBuilder.Create()
                .StartNow()
                .WithSchedule(CronScheduleBuilder.CronSchedule(cronExperssion))
                .Build();

            // 5.使用trigger规划执行任务job
            await sched.ScheduleJob(job, trigger);
        }

        private async static Task Run2(Type type, string cronExperssion)
        {
            // 1.创建scheduler的引用
            var schedFact = new StdSchedulerFactory();
            var sched = await schedFact.GetScheduler();
            await sched.Start();

            // 3.创建 job
            var job = JobBuilder.Create(type)
                .Build();

            // 4.创建 trigger
            ITrigger trigger = TriggerBuilder.Create()
                .StartNow()
                .WithSchedule(CronScheduleBuilder.CronSchedule(cronExperssion))
                .Build();

            // 5.使用trigger规划执行任务job
            await sched.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// 注册作业
        /// </summary>
        public static void Register()
        {
            var types = Utils.AssemblyUtils.GetTypes<IJob>("SixpenceStudio*.dll");
            foreach (var item in types)
            {
                if (!item.IsAbstract)
                {
                    var obj = Activator.CreateInstance(item);
                    var cron = item.GetProperty("CronExperssion").GetValue(obj)?.ToString();
                    Run2(item, cron);
                }
            }
        }
    }
}
