﻿using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Entity;
using SixpenceStudio.Platform.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Entity
{
    public class EntityCommand<T>
        where T : BaseEntity, new ()
    {
        #region 构造函数
        public EntityCommand()
        {
            broker = PersistBrokerFactory.GetPersistBroker();
        }
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
                return _broker ?? PersistBrokerFactory.GetPersistBroker();
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
        /// 根据搜索条件分页查询
        /// </summary>
        /// <param name="view"></param>
        /// <param name="searchList"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public IList<T> GetDataList(EntityView<T> view, IList<SearchCondition> searchList, string orderBy, int pageSize, int pageIndex, out int recordCount, string searchValue = "")
        {
            var sql = view.Sql;
            var paramList = new Dictionary<string, object>();

            GetSql(ref sql, searchList, ref paramList, orderBy, view, searchValue);

            var recordCountSql = $"SELECT COUNT(1) FROM ({sql}) AS table1";
            sql += $" LIMIT {pageSize} OFFSET {(pageIndex - 1) * pageSize}";
            recordCount = Convert.ToInt32(broker.ExecuteScalar(recordCountSql, paramList));
            var data = broker.RetrieveMultiple<T>(sql, paramList);
            return data;
        }

        /// <summary>
        /// 根据搜索条件查询
        /// </summary>
        /// <param name="view">视图</param>
        /// <param name="searchList">搜索条件</param>
        /// <param name="orderBy">排序</param>
        /// <returns></returns>
        public IList<T> GetDataList(EntityView<T> view, IList<SearchCondition> searchList, string orderBy, string searchValue = "")
        {
            var sql = view.Sql;
            var paramList = new Dictionary<string, object>();

            GetSql(ref sql, searchList, ref paramList, orderBy, view, searchValue);

            var data = broker.RetrieveMultiple<T>(sql, paramList);
            return data;
        }

        /// <summary>
        /// 格式化Sql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="searchList"></param>
        /// <param name="paramList"></param>
        /// <param name="orderBy"></param>
        /// <param name="view"></param>
        private void GetSql(ref string sql, IList<SearchCondition> searchList, ref Dictionary<string, object> paramList, string orderBy, EntityView<T> view, string searchValue)
        {
            var entityName = new T().EntityName;
            var count = 0;

            var index = sql.IndexOf("where", StringComparison.CurrentCultureIgnoreCase);
            if (index == -1)
            {
                sql += " WHERE 1=1 ";
            }

            if (!string.IsNullOrEmpty(searchValue) && view.CustomFilter != null)
            {
                foreach (var item in view.CustomFilter)
                {
                    sql += $" AND {entityName}.{item} LIKE @params{count}";
                    paramList.Add($"@params{count++}", $"%{searchValue}%");
                }
            }
            if (searchList != null && searchList.Count() > 0)
            {
                foreach (var search in searchList)
                {
                    var searchCondition = DialectSql.GetSearchCondition(search.Type, "params", search.Value, ref count);
                    sql += $" AND {entityName}.{search.Name} {searchCondition.sql}";
                    foreach (var item in searchCondition.paramsList)
                    {
                        paramList.Add(item.Key, item.Value);
                    }
                }
            }

            // 以ORDERBY的传入参数优先级最高
            if (string.IsNullOrEmpty(orderBy))
            {
                orderBy = string.IsNullOrEmpty(view.OrderBy) ? "" : $" ORDER BY {view.OrderBy}";
            }
            else
            {
                orderBy.Replace("ORDER BY", "");
                orderBy = $" ORDER BY {orderBy},{new T().EntityName}id";
            }

            sql += orderBy;
        }


        /// <summary>
        /// 获取实体记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetEntity(string id)
        {
            return broker.Retrieve<T>(id);
        }

        /// <summary>
        /// 创建实体记录
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public string Create(T t)
        {
            if (string.IsNullOrEmpty(t.Id))
            {
                return "";
            }

            var user = this.GetCurrentUser();
            if ((!t.Attributes.ContainsKey("createdBy") || t.GetAttributeValue("createdBy") == null) && t.GetType().GetProperty("createdBy") != null)
            {
                t["createdBy"] = user.userId;
                t["createdByName"] = user.name;
            }
            if ((!t.Attributes.ContainsKey("createdOn") || t.GetAttributeValue("createdOn") == null) && t.GetType().GetProperty("createdOn") != null)
            {
                t["createdOn"] = DateTime.Now;
            }
            t["modifiedBy"] = user.userId;
            t["modifiedByName"] = user.name;
            t["modifiedOn"] = DateTime.Now;
            var id = "";
            broker.ExecuteTransaction(() =>
            {
                AssemblyUtil.Execute<IEntityActionPlugin>("Execute", new object[] { new Context() { Broker = broker, Entity = t, EntityName = t.EntityName, Action = EntityAction.PreCreate } }, t.EntityName);
                id = broker.Create(t);
                AssemblyUtil.Execute<IEntityActionPlugin>("Execute", new object[] { new Context() { Broker = broker, Entity = t, EntityName = t.EntityName, Action = EntityAction.PostCreate } }, t.EntityName);
            });
            return id;
        }

        /// <summary>
        /// 更新实体记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void Update(T t)
        {
            if (string.IsNullOrEmpty(t.Id))
            {
                return;
            }
            var user = this.GetCurrentUser();
            t["modifiedBy"] = user.userId;
            t["modifiedByName"] = user.name;
            t["modifiedOn"] = DateTime.Now;
            broker.ExecuteTransaction(() =>
            {
                AssemblyUtil.Execute<IEntityActionPlugin>("Execute", new object[] { new Context() { Broker = broker, Entity = t, EntityName = t.EntityName, Action = EntityAction.PreUpdate } }, t.EntityName);
                broker.Update(t);
                AssemblyUtil.Execute<IEntityActionPlugin>("Execute", new object[] { new Context() { Broker = broker, Entity = t, EntityName = t.EntityName, Action = EntityAction.PostUpdate } }, t.EntityName);
            });
        }

        /// <summary>
        /// 创建或更新历史记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public string CreateOrUpdateData(T t)
        {
            var id = t.Id;
            var isExist = GetEntity(id) != null;
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
        public void Delete(List<string> ids)
        {
            broker.ExecuteTransaction(() =>
            {
                ids.ForEach(id =>
                {
                    var data = broker.Retrieve<T>(id);
                    AssemblyUtil.Execute<IEntityActionPlugin>("Execute", new object[] { new Context() { Broker = broker, Entity = data, EntityName = data.EntityName, Action = EntityAction.PreDelete } }, data.EntityName);
                    broker.Delete(new T().EntityName, id);
                    AssemblyUtil.Execute<IEntityActionPlugin>("Execute", new object[] { new Context() { Broker = broker, Entity = data, EntityName = data.EntityName, Action = EntityAction.PreDelete } }, data.EntityName);
                });
            });
        }
    }
}