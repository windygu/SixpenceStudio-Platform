using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.Extensions;
using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SixpenceStudio.Core.Data
{
    internal class PersistBroker : IPersistBroker, IDisposable
    {
        /// <summary>
        /// Generate Broker
        /// </summary>
        /// <param name="readonly">只读</param>
        internal PersistBroker(string connectionString, DriverType driverType = DriverType.Postgresql)
        {
            _dbClient = new DbClientProxy();
            _dbClient.Initialize(connectionString, driverType);
        }

        /// <summary>
        /// 数据库实例
        /// </summary>
        private IDbClient _dbClient;
        public IDbClient DbClient => _dbClient;

        #region CRUD
        /// <summary>
        /// 创建实体记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Create(BaseEntity entity)
        {
            return this.ExecuteTransaction(() =>
            {
                #region 创建前 Plugin
                var context = new PluginContext() { Entity = entity, Broker = this, Action = EntityAction.PreCreate, EntityName = entity.EntityName };

                UnityContainerService.ResolveAll<IPersistBrokerBeforeCreateOrUpdate>()?
                    .Each(item => item.Execute(context));
                UnityContainerService.ResolveAll<IPersistBrokerPlugin>(item => item.StartsWith(entity.EntityName.Replace("_", ""), StringComparison.OrdinalIgnoreCase))
                    .Each(item => item.Execute(context));
                #endregion

                var sql = "INSERT INTO {0}({1}) Values({2})";
                var attrs = new List<string>();
                var values = new List<object>();
                var paramList = new Dictionary<string, object>();
                foreach (var attr in entity.Attributes)
                {
                    var attrName = attr.Key == "Id" ? entity.MainKeyName : attr.Key;
                    var keyValue = DialectSql.GetSpecialValue($"@{attrName}", attr.Value);
                    attrs.Add(attrName);
                    values.Add(keyValue.name);
                    paramList.Add(attrName, keyValue.value);
                }
                sql = string.Format(sql, entity.EntityName, string.Join(",", attrs), string.Join(",", values));
                this.Execute(sql, paramList);

                #region 创建后 Plugin
                context.Action = EntityAction.PostCreate;
                UnityContainerService.ResolveAll<IPersistBrokerPlugin>(item => item.StartsWith(entity.EntityName.Replace("_", ""), StringComparison.OrdinalIgnoreCase))
                    .Each(item => item.Execute(context));
                #endregion

                return entity.Id;
            });
        }

        /// <summary>
        /// 删除实体记录
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(string entityName, string id)
        {
            var entity = new SpEntity(entityName, id);
            var plugin = UnityContainerService.Resolve<IPersistBrokerPlugin>(item => item.StartsWith(entityName.Replace("_", ""), StringComparison.OrdinalIgnoreCase));
            plugin?.Execute(new PluginContext() { Broker = this, Entity = entity, EntityName = entityName, Action = EntityAction.PreDelete });

            var sql = "DELETE FROM {0} WHERE {1}id = @id";
            sql = string.Format(sql, entityName, entityName);
            int result = this.Execute(sql, new Dictionary<string, object>() { { "@id", id } });

            plugin?.Execute(new PluginContext() { Broker = this, Entity = entity, EntityName = entityName, Action = EntityAction.PostDelete });
            return result;
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Delete(BaseEntity entity)
        {
            return this.ExecuteTransaction(() =>
            {
                var plugin = UnityContainerService.Resolve<IPersistBrokerPlugin>(item => item.StartsWith(entity.EntityName.Replace("_", ""), StringComparison.OrdinalIgnoreCase));
                plugin?.Execute(new PluginContext() { Broker = this, Entity = entity, EntityName = entity.EntityName, Action = EntityAction.PreDelete });
                var sql = "DELETE FROM {0} WHERE {1}id = @id";
                sql = string.Format(sql, entity.EntityName, entity.EntityName);
                int result = this.Execute(sql, new Dictionary<string, object>() { { "@id", entity.Id } });
                plugin?.Execute(new PluginContext() { Broker = this, Entity = entity, EntityName = entity.EntityName, Action = EntityAction.PostDelete });
                return result;
            });
        }

        /// <summary>
        /// 批量删除实体记录
        /// </summary>
        /// <param name="objArray"></param>
        /// <returns></returns>
        public int Delete(BaseEntity[] objArray)
        {
            if (objArray == null || objArray.Length == 0) return 0;

            return objArray.Sum(Delete);
        }

        /// <summary>
        /// 保存实体记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Save(BaseEntity entity)
        {
            var sql = $@"
SELECT * FROM {entity.EntityName}
WHERE {entity.EntityName}Id = @id;
";
            var dataList = this.Query(sql, new Dictionary<string, object>() { { "@id", entity.Id } });

            if (dataList != null && dataList.Rows.Count > 0)
                Update(entity);
            else
                Create(entity);

            return entity.Id;
        }

        /// <summary>
        /// 更新实体记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(BaseEntity entity)
        {
            return this.ExecuteTransaction(() =>
            {
                #region 更新前 Plugin
                var context = new PluginContext() { Broker = this, Entity = entity, EntityName = entity.EntityName, Action = EntityAction.PreUpdate };
                UnityContainerService.ResolveAll<IPersistBrokerBeforeCreateOrUpdate>()?
                    .Each(item => item.Execute(context));

                UnityContainerService.ResolveAll<IPersistBrokerPlugin>(item => item.StartsWith(entity.EntityName.Replace("_", ""), StringComparison.OrdinalIgnoreCase))
                    .Each(item => item.Execute(context));
                #endregion

                var sql = @"
UPDATE {0} SET {1} WHERE {2} = @id;
";
                var paramList = new Dictionary<string, object>();

                #region 处理属性
                var attributes = new List<string>();
                int count = 0;
                foreach (var item in entity.Attributes)
                {
                    if (item.Key != "Id" && item.Key != entity.EntityName + "Id")
                    {
                        var keyValue = DialectSql.GetSpecialValue($"@param{count}", item.Value);
                        paramList.Add($"@param{count}", keyValue.value);
                        attributes.Add($"{ item.Key} = {keyValue.name}");
                        count++;
                    }
                    else
                    {
                        paramList.Add("@id", item.Value);
                    }
                }
                #endregion
                sql = string.Format(sql, entity.EntityName, string.Join(",", attributes), entity.MainKeyName);
                var result = this.Execute(sql, paramList);

                #region 更新后 Plugin
                context.Action = EntityAction.PostUpdate;
                UnityContainerService.ResolveAll<IPersistBrokerPlugin>(item => item.StartsWith(entity.EntityName.Replace("_", ""), StringComparison.OrdinalIgnoreCase))
                    .Each(item => item.Execute(context));
                #endregion
                return result;
            });
        }
        #endregion

        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="where"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public int DeleteByWhere(string entityName, string where, Dictionary<string, object> paramList = null)
        {
            var sql = "DELETE FROM {0} WHERE 1=1 {1}";
            sql = string.Format(sql, string.IsNullOrEmpty(where) ? "" : $" AND {where}");
            int result = this.Execute(sql, paramList);
            return result;
        }

        /// <summary>
        /// 查询记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Retrieve<T>(string id) where T : BaseEntity, new()
        {
            var sql = $"SELECT * FROM {new T().EntityName} WHERE {new T().EntityName}id =@id";
            return Retrieve<T>(sql, new Dictionary<string, object>() { { "@id", id } });
        }

        /// <summary>
        /// 查询记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public T Retrieve<T>(string sql, Dictionary<string, object> paramList) where T : BaseEntity, new()
        {
            return _dbClient.Query<T>(sql, paramList).FirstOrDefault();
        }

        /// <summary>
        /// 查询多条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public IList<T> RetrieveMultiple<T>(string sql, Dictionary<string, object> paramList = null) where T : BaseEntity, new()
        {
            return _dbClient.Query<T>(sql, paramList).ToList();
        }

        /// <summary>
        /// 查询多条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <param name="orderby"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public IList<T> RetrieveMultiple<T>(string sql, Dictionary<string, object> paramList, string orderby, int pageSize, int pageIndex) where T : BaseEntity, new()
        {
            if (!string.IsNullOrEmpty(orderby))
            {
                if (!orderby.Contains("order by", StringComparison.OrdinalIgnoreCase))
                    sql += $" ORDER BY {orderby}";
                else
                    sql += $" {orderby}";
            }

            sql += $" LIMIT {pageSize} OFFSET {pageSize * (pageIndex - 1)}";
            return RetrieveMultiple<T>(sql, paramList);
        }

        /// <summary>
        /// 查询多条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <param name="orderby"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList<T> RetrieveMultiple<T>(string sql, Dictionary<string, object> paramList, string orderby, int pageSize, int pageIndex, out int recordCount) where T : BaseEntity, new()
        {
            var recordCountSql = $"SELECT COUNT(1) FROM ({sql}) AS table1";
            recordCount = Convert.ToInt32(this.ExecuteScalar(recordCountSql, paramList));
            var data = RetrieveMultiple<T>(sql, paramList, orderby, pageSize, pageIndex);
            return data;
        }

        /// <summary>
        /// 根据 id 批量查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IList<T> RetrieveMultiple<T>(IList<string> ids) where T : BaseEntity, new()
        {
            var sql = $@"
SELECT
	*
FROM
	{new T().EntityName}
WHERE 
	{new T().EntityName}id IN (@ids)";
            var parmas = new Dictionary<string, object>();
            var count = 0;
            ids.ToList().ForEach(item =>
            {
                parmas.Add($"@id{++count}", ids[count - 1]);
            });
            sql = sql.Replace("@ids", string.Join(",", parmas.Keys));
            var data = RetrieveMultiple<T>(sql, parmas);
            return data;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            (this.DbClient as IDisposable).Dispose();
        }
    }
}
