using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Command
{
    public class EntityCommand<T>
        where T : BaseEntity, new ()
    {
        #region 构造函数
        public EntityCommand() { }
        public EntityCommand(IPersistBroker broker)
        {
            this._broker = broker;
        }
        #endregion

        private IPersistBroker _broker;
        public IPersistBroker broker
        {
            get
            {
                return _broker ?? new PersistBroker();
            }
            set
            {
                this._broker = value;
            }
        }

        /// <summary>
        ///  获取所有实体记录
        /// </summary>
        /// <returns></returns>
        public IList<T> GetAllEntity()
        {
            var sql = $"SELECT *  FROM {new T().EntityName}";
            var data = broker.RetrieveMultiple<T>(sql);
            return data;
        }

        /// <summary>
        /// 根据搜索条件查询
        /// </summary>
        /// <param name="searchList"></param>
        /// <returns></returns>
        public IList<T> GetDataList(IList<SearchCondition> searchList, string orderBy, int pageSize, int pageIndex, out int recordCount)
        {
            var sql = $"SELECT *  FROM {new T().EntityName} WHERE 1=1";
            var where = string.Empty;
            var paramList = new Dictionary<string, object>();

            if (searchList != null)
            {
                var count = 0;
                foreach (var search in searchList)
                {
                    where += $" AND {search.Name} = @params{count}";
                    paramList.Add($"@params{count++}", search.Value);
                }
            }

            if (string.IsNullOrEmpty(orderBy))
            {
                orderBy = " ORDER BY " + new T().EntityName + "id";
            }
            else
            {
                orderBy.Replace("ORDER BY", "");
                orderBy = " ORDER BY " + orderBy;
            }

            var recordCountSql = $"SELECT COUNT(1) FROM ({sql}) AS table";
            sql += $" LIMIT {pageSize} OFFSET {(pageIndex - 1) * pageSize}";
            recordCount = Convert.ToInt32(broker.DbClient.ExecuteScalar(sql, paramList));
            var data = broker.RetrieveMultiple<T>(sql + where + orderBy, paramList);
            return data;
        }

        /// <summary>
        /// 获取实体记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetEntity<T>(string id) where T : BaseEntity, new()
        {
            return broker.Retrieve<T>(id);
        }

        /// <summary>
        /// 创建实体记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public string Create<T>(T t) where T : BaseEntity, new()
        {
            if (string.IsNullOrEmpty(t.Id))
            {
                return "";
            }

            var user = this.GetCurrentUser();
            if ((!t.Attributes.ContainsKey("CreatedBy") || t.GetAttributeValue("CreatedBy") == null) && t.GetType().GetProperty("createdBy") != null)
            {
                t.SetAttributeValue("CreatedBy", user.userId);
                t.SetAttributeValue("CreatedByName", user.name);
            }
            if ((!t.Attributes.ContainsKey("ModifiedBy") || t.GetAttributeValue("ModifiedBy") == null) && t.GetType().GetProperty("modifiedBy") != null)
            {
                t.SetAttributeValue("ModifiedBy", user.userId);
                t.SetAttributeValue("ModifiedByName", user.name);
            }
            if ((!t.Attributes.ContainsKey("CreatedOn") || t.GetAttributeValue("CreatedOn") == null) && t.GetType().GetProperty("createdOn") != null)
            {
                t.SetAttributeValue("CreatedOn", DateTime.Now);
            }
            if ((!t.Attributes.ContainsKey("ModifiedOn") || t.GetAttributeValue("ModifiedOn") == null) && t.GetType().GetProperty("modifiedOn") != null)
            {
                t.SetAttributeValue("ModifiedOn", DateTime.Now);
            }

            return broker.Create(t);
        }

        /// <summary>
        /// 更新实体记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void Update<T>(T t) where T : BaseEntity, new()
        {
            if (string.IsNullOrEmpty(t.Id))
            {
                return;
            }
            var user = this.GetCurrentUser();
            if ((!t.Attributes.ContainsKey("ModifiedBy") || t.GetAttributeValue("ModifiedBy") == null) && t.GetType().GetProperty("modifiedBy") != null)
            {
                t.SetAttributeValue("ModifiedBy", user.userId);
                t.SetAttributeValue("ModifiedByName", user.name);
            }
            if ((!t.Attributes.ContainsKey("ModifiedOn") || t.GetAttributeValue("ModifiedOn") == null) && t.GetType().GetProperty("modifiedOn") != null)
            {
                t.SetAttributeValue("ModifiedOn", DateTime.Now);
            }
            broker.Update(t);
        }

        /// <summary>
        /// 创建或更新历史记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public string CreateOrUpdateData<T>(T t) where T : BaseEntity, new()
        {
            var id = t.Id;
            var isExist = GetEntity<T>(id) != null;
            if (isExist)
            {
                Update(t);
            }
            else
            {
                id = Create(t);
            }
            return id;
        }

        /// <summary>
        /// 删除历史记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        public void Delete<T>(List<string> ids) where T : BaseEntity, new()
        {
            ids.ForEach(id => broker.Delete(new T().EntityName, id));
        }

    }
}
