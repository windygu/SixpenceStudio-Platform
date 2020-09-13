using Quartz;
using Quartz.Impl;
using SixpenceStudio.Platform.Utils;
using System;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Job
{
    public class JobHelpers
    {
        // 创建scheduler的引用
        static StdSchedulerFactory schedFact = new StdSchedulerFactory();
        static IScheduler sched = schedFact.GetScheduler().Result;

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

        private async static Task Run2(Type type, string cronExperssion)
        {
            await sched.Start();

            // 创建 job
            var job = JobBuilder.Create(type)
                .Build();

            // 创建 trigger
            ITrigger trigger = TriggerBuilder.Create()
                .StartNow()
                .WithSchedule(CronScheduleBuilder.CronSchedule(cronExperssion))
                .Build();

            // 使用trigger规划执行任务job
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
            var schedFact = new StdSchedulerFactory();
            var sched = await schedFact.GetScheduler();
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
        public static void Register()
        {
            var types = Utils.AssemblyUtil.GetTypes<IJob>("SixpenceStudio*.dll");
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

        /// <summary>
        /// 获取Job下次运行时间
        /// </summary>
        /// <param name="jobName"></param>
        /// <returns></returns>
        public static string GetJobNextTime(string jobName)
        {
            var types = Utils.AssemblyUtil.GetTypes<IJob>();
            foreach (var item in types)
            {
                if (!item.IsAbstract)
                {
                    var obj = Activator.CreateInstance(item);
                    var _jobName = item.GetProperty("Name").GetValue(obj)?.ToString();
                    var _jobCron = item.GetProperty("CronExperssion").GetValue(obj)?.ToString();
                    if (string.Equals(jobName, _jobName))
                    {
                        return CronUtil.GetNextDateTime(_jobCron, DateTime.Now);
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// 手动执行一次任务
        /// </summary>
        /// <param name="name"></param>
        public async static void StartJob(string jobName)
        {
            var types = AssemblyUtil.GetTypes<IJob>();
            foreach (var item in types)
            {
                if (!item.IsAbstract)
                {
                    var obj = Activator.CreateInstance(item);
                    var _jobName = item.GetProperty("Name").GetValue(obj)?.ToString();
                    if (string.Equals(jobName, _jobName))
                    {
                        await RunManually(item);
                    }
                }
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
