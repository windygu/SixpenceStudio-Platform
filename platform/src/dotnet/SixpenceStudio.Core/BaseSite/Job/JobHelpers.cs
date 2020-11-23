using log4net.Repository.Hierarchy;
using Quartz;
using Quartz.Impl;
using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Job
{
    /// <summary>
    /// Job帮助类
    /// </summary>
    public static class JobHelpers
    {
        // 创建scheduler的引用
        static StdSchedulerFactory schedFact;
        static IScheduler sched;

        static JobHelpers()
        {
            schedFact = schedFact ?? new StdSchedulerFactory();
            sched = sched ?? schedFact.GetScheduler().Result;
        }

        /// <summary>
        /// 任务调度的使用过程
        /// </summary>
        /// <returns></returns>
        private async static Task Run<T>(string cronExperssion)
            where T : IJob
        {
            await sched.Start();

            // 创建 Job
            var job = JobBuilder.Create<T>()
                .Build();

            // 创建 trigger
            ITrigger trigger = TriggerBuilder.Create()
                .StartNow()
                .WithSchedule(CronScheduleBuilder.CronSchedule(cronExperssion))
                .Build();

            // 使用 trigger 规划执行任务 job
            await sched.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// 手动执行
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private async static Task RunManually(Type type)
        {
            // 1.创建scheduler的引用
            await sched.Start();

            // 3.创建 job
            var job = JobBuilder.Create(type)
                .Build();

            // 4.创建 trigger
            ITrigger trigger = TriggerBuilder.Create()
                .StartNow()
                .Build();

            // 5.使用trigger规划执行任务job
            await sched.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// 注册作业
        /// </summary>
        public static void Register(Logging.Logger logger)
        {
            foreach (var assembly in AssemblyUtil.GetAssemblies())
            {
                var types = assembly.GetTypes();
                types.Each(type =>
                {
                    if (type.IsInstanceOfType(typeof(IJob)))
                    {
                        UnityContainerService.RegisterType(typeof(JobBase), type, type.Name);
                    }
                });
            }

            UnityContainerService.ResolveAll<JobBase>()
                .Each(item =>
                {
                    sched.Start().Wait();

                    // 创建 Job
                    var job = JobBuilder.Create(item.GetType())
                        .Build();
                    if (item == null)
                    {
                        return;
                    }
                    if (!string.IsNullOrEmpty(item.CronExperssion))
                    {
                        // 创建 trigger
                        ITrigger trigger = TriggerBuilder.Create()
                            .StartNow()
                            .WithSchedule(CronScheduleBuilder.CronSchedule(item.CronExperssion))
                            .Build();

                        // 使用 trigger 规划执行任务 job
                        sched.ScheduleJob(job, trigger).Wait();
                    }
                    logger.Info($"创建{item.Name}Job成功");
                });
        }

        /// <summary>
        /// 获取Job下次运行时间
        /// </summary>
        /// <param name="jobName"></param>
        /// <returns></returns>
        public static string GetJobNextTime(string jobName)
        {
            var job = UnityContainerService.Resolve<JobBase>(jobName);
            if (job != null)
            {
                return CronUtil.GetNextDateTime(job.CronExperssion, DateTime.Now);
            }

            return "";
        }

        /// <summary>
        /// 手动执行一次任务
        /// </summary>
        /// <param name="name"></param>
        public static void StartJob(string name)
        {
            var job = UnityContainerService.Resolve<JobBase>(name);
            if (job != null)
            {
                RunManually(job.GetType()).Wait();
            }
        }

        /// <summary>
        /// 停止Job
        /// </summary>
        public static void PauseJob()
        {
            sched.PauseAll();
        }

        /// <summary>
        /// 继续
        /// </summary>
        public static void Continue()
        {
            sched.ResumeAll();
        }

        /// <summary>
        /// 服务停止
        /// </summary>
        public static void StopJob()
        {
            if (!sched.IsShutdown)
            {
                sched.Shutdown();
            }
        }
    }
}
