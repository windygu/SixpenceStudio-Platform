using Quartz;
using Quartz.Impl;
using SixpenceStudio.Core.Auth;
using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Job
{
    /// <summary>
    /// Job帮助类
    /// </summary>
    public static class JobHelpers
    {
        // 创建scheduler的引用
        static IScheduler sched = new StdSchedulerFactory().GetScheduler().Result;

        static JobHelpers() { }

        /// <summary>
        /// 任务调度的使用过程
        /// </summary>
        /// <returns></returns>
        public async static Task Run<T>(string cronExperssion, string name, string group, object param)
            where T : IJob
        {
            var job = JobBuilder.Create<T>()
                .WithIdentity(name, group)
                .Build();

            job.JobDataMap.Add("Context", param);
            job.JobDataMap.Add("User", UserIdentityUtil.GetCurrentUser());

            ITrigger trigger = TriggerBuilder.Create()
                .StartNow()
                .WithSchedule(CronScheduleBuilder.CronSchedule(cronExperssion))
                .Build();

            await sched.ScheduleJob(job, trigger);
            StartJob();
        }

        /// <summary>
        /// 手动执行
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private async static Task RunOnce(Type type)
        {
            var job = JobBuilder.Create(type)
                .Build();

            job.JobDataMap.Add("User", UserIdentityUtil.GetCurrentUser());

            ITrigger trigger = TriggerBuilder.Create()
                .StartNow()
                .Build();

            await sched.ScheduleJob(job, trigger);
            StartJob();
        }

        /// <summary>
        /// 删除job
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        public static async void DeleteJob(string name, string group)
        {
            await sched.DeleteJob(new JobKey(name, group));
        }


        /// <summary>
        /// 注册作业
        /// </summary>
        public static void Register(Logging.Logger logger)
        {
            UnityContainerService.ResolveAll<IJob>()
                .Each(item =>
                {
                    if (item == null)
                    {
                        return;
                    }

                    StartJob();

                    // 创建 Job
                    var instance = item as JobBase;
                    var job = JobBuilder.Create(item.GetType())
                        .WithIdentity(new JobKey(instance.Name, item.GetType().Namespace))
                        .Build();
                    new JobService().CreateOrUpdateData(instance);
                    job.JobDataMap.Add("User", UserIdentityUtil.GetAdmin());

                    if (!string.IsNullOrEmpty(instance.CronExperssion))
                    {
                        // 创建 trigger
                        ITrigger trigger = TriggerBuilder.Create()
                            .StartNow()
                            .WithSchedule(CronScheduleBuilder.CronSchedule(instance.CronExperssion))
                            .Build();

                        // 使用 trigger 规划执行任务 job
                        sched.ScheduleJob(job, trigger).Wait();
                    }
                    logger.Info($"创建{instance.Name}Job成功");
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
                    RunOnce(job.GetType()).Wait();
                }
            });
        }

        /// <summary>
        /// 停止所有 Job
        /// </summary>
        public static void PauseJob()
        {
            sched.PauseAll();
        }

        /// <summary>
        /// 暂停 Job
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        public static void PauseJob(string name, string group)
        {
            sched.PauseJob(new JobKey(name, group));
        }

        /// <summary>
        /// 继续
        /// </summary>
        public static void Continue(string name, string group)
        {
            sched.ResumeJob(new JobKey(name, group));
        }

        /// <summary>
        /// 服务停止
        /// </summary>
        public async static void StopJob()
        {
            if (!sched.IsShutdown)
            {
                await sched.Shutdown();
            }
        }

        /// <summary>
        /// 开启服务
        /// </summary>
        public async static void StartJob()
        {
            if (!sched.IsStarted)
            {
                await sched.Start();
            }
        }
    }
}
