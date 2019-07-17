using System;
using System.Collections.Generic;
using System.Text;
using Platform.Data.Entity;

namespace Platform.Data.Command
{
    public class EntityCommand<T> : BaseCommand
        where T : BaseEntity, new()
    {
        #region 构造函数
        public EntityCommand() { }
        #endregion

        /// <summary>
        /// 获取所有的实体记录
        /// </summary>
        /// <returns>所有的实体记录</returns>
        public IList<T> GetAllData()
        {
            var sql = string.Format(@"SELECT * FROM {0}", new T().EntityName);
            return Broker.RetrieveMultiple<T>(sql);
        }

    }
}
