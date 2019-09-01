using System;
using System.Collections.Generic;
using System.Text;
using Platform.Core.Entity;
using Platform.Core.PersistBroker;

namespace Platform.Core.Command
{
    public class EntityCommand<T> : BaseCommand
        where T : BaseEntity, new()
    {
        #region 构造函数
        public EntityCommand() { }

        public EntityCommand(IPersistBroker broker) : base(broker) { }
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

        /// <summary>
        /// 根据条件获取实体视图记录列表
        /// </summary>
        /// <param name="view">实体视图</param>
        /// <param name="pageIndex">分页</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="recordCount">记录数</param>
        /// <param name="quickSearchValue">快速查询条件</param>
        /// <param name="searchList">The search list.</param>
        /// <param name="orderby">排序字段.</param>
        /// <returns>
        /// 实体视图记录列表
        /// </returns>
        public IList<T> GetEntityViewDataList(EntityViewModel view, int pageIndex, int pageSize, out int recordCount,
            string quickSearchValue, List<SearchCondition> searchList, string orderby = "")
        {
            var sql = view.Sql;
            var paramList = new Dictionary<string, object>();

            if (searchList != null && searchList.Count != 0)
            {

            }

            return Broker.RetrieveMultiple<T>(sql, paramList, !string.IsNullOrEmpty(orderby) ? orderby : view.OrderBy, pageSize, pageIndex, out recordCount);
        }
    }

    public class SearchCondition
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
