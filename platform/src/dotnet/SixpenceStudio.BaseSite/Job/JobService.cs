using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Entity;
using SixpenceStudio.Platform.Job;
using SixpenceStudio.Platform.Logging;
using SixpenceStudio.Platform.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.BaseSite.Job
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