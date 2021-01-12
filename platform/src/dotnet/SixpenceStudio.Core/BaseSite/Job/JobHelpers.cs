using log4net.Repository.Hierarchy;
using Quartz;
using Quartz.Impl;
using SixpenceStudio.Core.Auth;
using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
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
        public async static Task Run<T>(string cronExperssion, string name, string group, object param)
            where T : IJob
        {
            await sched.Start();

            // 创建 Job
            var job = JobBuilder.Create<T>()
                .WithIdentity(name, group)
                .Build();
            job.JobDataMap.Add("Context", param);

            // 创建 trigger
            ITrigger trigger = TriggerBuilder.Create()
                .StartNow()
                .WithSchedule(CronScheduleBuilder.CronSchedule(cronExperssion))
                .Build();

            // 使用 trigger 规划执行任务 job
            sched.ScheduleJob(job, trigger).Wait();
            sched.Start().Wait();
        }

        /// <summary>
        /// 删除job
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        public static void DeleteJob(string name, string group)
        {
            sched.DeleteJob(new JobKey(name, group)).Wait();
        }

        /// <summary>
        /// 手动执行
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal async static Task RunManually(Type type)
        {
            // 1.创建scheduler的引用
            await sched.Start();

            // 3.创建 job
            var job = JobBuilder.Create(type)
                .Build();

            job.JobDataMap.Add("User", UserIdentityUtil.GetCurrentUser());

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
            UnityContainerService.ResolveAll<IJob>()
                .Each(item =>
                {
                    sched.Start().Wait();

                    // 创建 Job
                    var job = JobBuilder.Create(item.GetType())
                        .Build();
                    job.JobDataMap.Add("User", UserIdentityUtil.GetAdmin());

                    if (item == null)
                    {
                        return;
                    }

                    var cronExperssion = item.GetType().GetProperty("CronExperssion").GetValue(item).ToString();
                    var name = item.GetType().GetProperty("Name").GetValue(item).ToString();
                    if (!string.IsNullOrEmpty(cronExperssion))
                    {
                        // 创建 trigger
                        ITrigger trigger = TriggerBuilder.Create()
                            .StartNow()
                            .WithSchedule(CronScheduleBuilder.CronSchedule(cronExperssion))
                            .Build();

                        // 使用 trigger 规划执行任务 job
                        sched.ScheduleJob(job, trigger).Wait();
                    }
                    logger.Info($"创建{name}Job成功");
                });
        }

        /// <summary>
        /// 获取Job下次运行时间
        /// </summary>
        /// <param name="jobName"></param>
        /// <returns></returns>
        public static string GetJobNextTime(string jobName)
        {
            var jobs = UnityContainerService.ResolveAll<IJob>();
            var time = "";
            jobs.Each(job =>
            {
                var instance = Activator.CreateInstance(job.GetType()) as JobBase;
                if (instance.Name.Equals(jobName))
                {
                    time = CronUtil.GetNextDateTime(instance.CronExperssion, DateTime.Now);
                }
            });
            return time;
        }

        /// <summary>
        /// 手动执行一次任务
        /// </summary>
        /// <param name="name"></param>
        public static void StartJob(string name)
        {
            var jobs = UnityContainerService.ResolveAll<IJob>();
            jobs.Each(job =>
            {
                var instance = Activator.CreateInstance(job.GetType()) as JobBase;
                if (instance.Name.Equals(name))
                {
                    RunManually(job.GetType()).Wait();
                }
            });
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
