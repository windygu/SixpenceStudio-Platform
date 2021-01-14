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
        /// 注册job
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="group"></param>
        /// <param name="param"></param>
        /// <param name="cronExperssion"></param>
        public static void RegisterJob<T>(object context, string cronExperssion) where T : JobBase, new ()
        {
            var job = new T();

            var jobDetail = JobBuilder.Create<T>()
                .WithIdentity(job.Name, job.GetType().Namespace)
                .Build();

            jobDetail.JobDataMap.Add("Context", context);
            jobDetail.JobDataMap.Add("User", UserIdentityUtil.GetAdmin());

            TriggerBuilder builder = TriggerBuilder.Create().WithIdentity(job.Name, job.GetType().Namespace);
            if (!string.IsNullOrEmpty(cronExperssion))
            {
                builder = builder.WithSchedule(CronScheduleBuilder.CronSchedule(cronExperssion));
            }

            ITrigger trigger = builder
                .StartNow()
                .Build();
            sched.ScheduleJob(jobDetail, trigger).Wait();
            StartJob();
        }

        /// <summary>
        /// 手动运行一次
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        public static void RunOnce<T>(object context) where T : JobBase, new()
        {
            var jobDetail = JobBuilder.Create<T>()
                .Build();

            jobDetail.JobDataMap.Add("Context", context);
            jobDetail.JobDataMap.Add("User", UserIdentityUtil.GetAdmin());

            ITrigger trigger = TriggerBuilder.Create()
                .StartNow()
                .Build();
            sched.ScheduleJob(jobDetail, trigger).Wait();
            StartJob();
        }

        /// <summary>
        /// 手动执行
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static void RunOnce(Type type)
        {
            var job = JobBuilder.Create(type)
                .Build();

            job.JobDataMap.Add("User", UserIdentityUtil.GetCurrentUser());

            ITrigger trigger = TriggerBuilder.Create()
                .StartNow()
                .Build();

            sched.ScheduleJob(job, trigger);
            StartJob();
        }

        /// <summary>
        /// 删除job
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        public static async void DeleteJob(string name, string group)
        {
            await sched.PauseJob(new JobKey(name, group)); // 停止任务
            await sched.PauseTrigger(new TriggerKey(name, group)); // 停止触发器
            await sched.UnscheduleJob(new TriggerKey(name, group)); // 移除触发器
            await sched.DeleteJob(new JobKey(name, group)); // 删除任务
        }


        /// <summary>
        /// 注册作业
        /// </summary>
        public static void Register()
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
                    job.JobDataMap.Add("User", UserIdentityUtil.GetAdmin());

                    if (!string.IsNullOrEmpty(instance.CronExperssion))
                    {
                        // 创建 trigger
                        ITrigger trigger = TriggerBuilder.Create()
                            .StartNow()
                            .WithSchedule(CronScheduleBuilder.CronSchedule(instance.CronExperssion))
                            .Build();

                        // 使用 trigger 规划执行任务 job
                        sched.ScheduleJob(job, trigger);
                    }
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
                    RunOnce(job.GetType());
                }
            });
        }

        /// <summary>
        /// 暂停 Job
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        public static void PauseJob(string name, string group)
        {
            sched.PauseTrigger(new TriggerKey(name, group)); // 暂停触发器
            sched.PauseJob(new JobKey(name, group)); // 暂停job
        }

        /// <summary>
        /// 继续
        /// </summary>
        public static void ResumeJob(string name, string group)
        {
            sched.ResumeTrigger(new TriggerKey(name, group));
            sched.ResumeJob(new JobKey(name, group));
        }

        /// <summary>
        /// 服务停止
        /// </summary>
        public async static void Shutdown()
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
