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
    }
}