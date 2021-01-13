using Quartz;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.Job;
using SixpenceStudio.Core.Logging;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.Core.Job
{
    public class JobService : EntityService<job>
    {
        #region 构造函数
        public JobService()
        {
            _cmd = new EntityCommand<job>();
        }

        public JobService(IPersistBroker broker)
        {
            _cmd = new EntityCommand<job>(broker);
        }
        #endregion

        /// <summary>
        /// 创建或更新
        /// </summary>
        /// <param name="job"></param>
        public void CreateOrUpdateData(JobBase job)
        {
            var sql = @"
SELECT * FROM job
WHERE name = @name
";
            var data = Broker.Retrieve<job>(sql, new Dictionary<string, object>() { { "@name", job.Name } });
            if (data != null)
            {
                data.runTime = job.CronExperssion;
                data.description = job.Description;
            }
            else
            {
                data = new job()
                {
                    Id = Guid.NewGuid().ToString(),
                    name = job.Name,
                    description = job.Description,
                    runTime = job.CronExperssion
                };
            }
            Broker.Save(data);
        }

        /// <summary>
        /// 删除job列表不存在的job
        /// </summary>
        /// <param name="jobNameList"></param>
        public void DeleteJob(List<string> jobNameList)
        {
            var sql = @"SELECT * FROM job WHERE name NOT IN (in@names)";
            var dataList = Broker.RetrieveMultiple<job>(sql, new Dictionary<string, object>() { { "in@names", string.Join(",", jobNameList) } });
            base.DeleteData(dataList.Select(item => item.Id).ToList());
        }

        /// <summary>
        /// 查询所有的job
        /// </summary>
        /// <returns></returns>
        public IList<job> GetDataList()
        {
            var sql = @"
SELECT * FROM job
ORDER BY name
";
            var dataList = _cmd.Broker.RetrieveMultiple<job>(sql);
            return dataList;
        }

        /// <summary>
        /// 手动执行job
        /// </summary>
        /// <param name="name"></param>
        public void StartJob(string name)
        {
            JobHelpers.StartJob(name);
        }

        /// <summary>
        ///删除job
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        public void DeleteJob(string name, string group)
        {
            JobHelpers.DeleteJob(name, group);
        }

        /// <summary>
        /// 暂停job
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        public void PauseJob(string name, string group)
        {
            JobHelpers.PauseJob(name, group);
        }

        /// <summary>
        /// 继续job
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        public void ContinueJob(string name, string group)
        {
            JobHelpers.Continue(name, group);
        }

    }
}