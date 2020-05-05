using SixpenceStudio.Platform.Command;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Service
{
    public class EntityService<T>
        where T : BaseEntity, new()
    {
        protected EntityCommand<T> _cmd;

        protected IPersistBroker Broker
        {
            get
            {
                return _cmd.broker;
            }
        }

        #region 实体表单
        /// <summary>
        /// 获取所有实体记录
        /// </summary>
        /// <returns></returns>
        public virtual DataModel<T> GetDataList(IList<SearchCondition> searchList, string orderBy, int pageSize, int pageIndex)
        {
            var data = _cmd.GetDataList(searchList, orderBy, pageSize, pageIndex, out var recordCount);
            return new DataModel<T>()
            {
                DataList = data,
                RecordCount = recordCount
            };
        }

        /// <summary>
        /// 获取实体记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetData(string id)
        {
            var obj = _cmd.GetEntity<T>(id);
            return obj;
        }

        /// <summary>
        /// 创建数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual string CreateData(T t)
        {
            return _cmd.Create(t);
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="t"></param>
        public virtual void UpdateData(T t)
        {
            _cmd.Update(t);
        }

        /// <summary>
        /// 创建或更新记录
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual string CreateOrUpdateData(T t)
        {
            return _cmd.CreateOrUpdateData(t);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="ids"></param>
        public virtual void DeletelData(List<string> ids)
        {
            _cmd.Delete<T>(ids);
        }
        #endregion
    }
}
