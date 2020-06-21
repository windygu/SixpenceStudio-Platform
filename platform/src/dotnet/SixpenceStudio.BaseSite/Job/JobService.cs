using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Job;
using SixpenceStudio.Platform.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.BaseSite.Job
{
    public class JobService
    {
        IPersistBroker broker;
        public JobService()
        {
            this.broker = new PersistBroker();
        }

        /// <summary>
        /// 查询所有的job
        /// </summary>
        /// <returns></returns>
        public IList<job> GetDataList()
        {
            var sql = @"
SELECT * FROM job
";
            var dataList = broker.RetrieveMultiple<job>(sql);
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
    }
}