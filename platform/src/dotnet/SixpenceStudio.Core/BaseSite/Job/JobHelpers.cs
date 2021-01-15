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
        static IScheduler sched = new StdSchedulerFactory().GetScheduler().Result;

        static JobHelpers() { }


        /// <summary>
        /// 注册作业
        /// </summary>
        public static void Start()
        {
            UnityContainerService.ResolveAll<IJob>()
                .Each(item =>
                {
                    if (item == null)
                    {
                        return;
                    }

                    // 创建 Job
                    var instance = item as JobBase;

                    if (sched.CheckExists(instance.JobKey).Result)
                    {
                        return;
                    }

                    StartService();


                    var job = instance.GetJobBuilder().Build();
                    job.JobDataMap.Add("User", UserIdentityUtil.GetAdmin());

                    var triggerBuilder = instance.GetTriggerBuilder();

                    if (triggerBuilder != null)
                    {
                        // 创建 trigger
                        ITrigger trigger = triggerBuilder.Build();
                        // 使用 trigger 规划执行任务 job
                        sched.ScheduleJob(job, trigger);

                        if (instance.DefaultTriggerState == TriggerState.Paused)
                        {
                            sched.PauseTrigger(trigger.Key);
                        }
                    }
                });
        }

        /// <summary>
        /// 注册job
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="group"></param>
        /// <param name="param"></param>
        /// <param name="cronExperssion"></param>
        public static void RegisterJob(DynamicJobBase job, object context)
        {
            StartService();

            if (sched.CheckExists(job.JobKey).Result)
            {
                return;
            }

            var jobDetail = job.GetJobBuilder().Build();

            jobDetail.JobDataMap.Add("Context", context);
            jobDetail.JobDataMap.Add("User", UserIdentityUtil.GetAdmin());

            ITrigger trigger = job.GetTriggerBuilder()
                .Build();
            sched.ScheduleJob(jobDetail, trigger).Wait();
        }

        /// <summary>
        /// 立即执行
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        /// <param name="context"></param>
        public static void RunOnceNow(string name, string group, IDictionary<string, object> context)
        {
            var jobKey = new JobKey(name, group);
            if (sched.CheckExists(jobKey).Result)
            {
                var jobDataMap = new JobDataMap(context);
                sched.TriggerJob(jobKey, jobDataMap);
            }
        }

        /// <summary>
        /// 立即执行
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        public static void RunOnceNow(string name, string group)
        {
            var jobKey = new JobKey(name, group);
            if (sched.CheckExists(jobKey).Result)
            {
                sched.TriggerJob(jobKey);
            }
        }

        /// <summary>
        /// 删除job
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        public static void DeleteJob(string name, string group)
        {
            if (sched.CheckExists(new JobKey(name, group)).Result)
            {
                sched.PauseJob(new JobKey(name, group)); // 停止任务
                sched.PauseTrigger(new TriggerKey(name, group)); // 停止触发器
                sched.UnscheduleJob(new TriggerKey(name, group)); // 移除触发器
                sched.DeleteJob(new JobKey(name, group)); // 删除任务
            }
        }

        /// <summary>
        /// 获取Job下次运行时间
        /// </summary>
        /// <param name="jobName"></param>
        /// <returns></returns>
        public static DateTimeOffset? GetJobNextTime(string jobName)
        {
            var jobs = UnityContainerService.ResolveAll<IJob>();
            var datetime = new DateTimeOffset();
            jobs.Each(job =>
            {
                var instance = Activator.CreateInstance(job.GetType()) as JobBase;
                if (instance.Name.Equals(jobName))
                {
                    datetime = sched.GetTrigger(new TriggerKey(instance.JobKey.Name, instance.JobKey.Group)).Result.GetNextFireTimeUtc().Value;
                }
            });
            return datetime;
        }

        /// <summary>
        /// 暂停 Job调度
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        public static void PauseJob(string name, string group)
        {
            var triggerKey = new TriggerKey(name, group);
            sched.PauseTrigger(triggerKey);
        }

        /// <summary>
        /// 继续
        /// </summary>
        public static void ResumeJob(string name, string group)
        {
            sched.ResumeTrigger(new TriggerKey(name, group));
        }

        /// <summary>
        /// 服务停止
        /// </summary>
        private async static void Shutdown()
        {
            if (!sched.IsShutdown)
            {
                await sched.Shutdown();
            }
        }

        /// <summary>
        /// 开启服务
        /// </summary>
        private async static void StartService()
        {
            if (!sched.IsStarted)
            {
                await sched.Start();
            }
        }
    }
}
