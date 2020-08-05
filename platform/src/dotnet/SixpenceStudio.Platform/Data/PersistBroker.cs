using SixpenceStudio.Platform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixpenceStudio.Platform.Utils;
using System.Configuration;
using SixpenceStudio.Platform.Configs;

namespace SixpenceStudio.Platform.Data
{
    public class PersistBroker : IPersistBroker
    {
        public PersistBroker()
        {
            string connectionString = ConfigurationManager.AppSettings["DbConnectrionString"];
            DecryptAndEncryptHelper helper = new DecryptAndEncryptHelper(ConfigInformation.Key, ConfigInformation.Vector);
            var decryptionString = helper.Decrypto(connectionString);
            _dbClient = new DbClient();
            _dbClient.Initialize(decryptionString);
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
                paramList.Add(keyValue.name, keyValue.value);
            }
            sql = string.Format(sql, entity.EntityName, string.Join(",", attrs), string.Join(",", values));
            this.Execute(sql, paramList);
            return entity.Id;
        }

        /// <summary>
        /// 删除实体记录
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(string entityName, string id)
        {
            var sql = "DELETE FROM {0} WHERE {1}id = @id";
            sql = string.Format(sql, entityName, entityName);
            int result = this.Execute(sql, new Dictionary<string, object>() { { "@id", id } });
            return result;
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Delete(BaseEntity obj)
        {
            var sql = "DELETE FROM {0} WHERE {1}id = @id";
            sql = string.Format(sql, obj.EntityName, obj.EntityName);
            int result = this.Execute(sql, new Dictionary<string, object>() { { "@id", obj.Id } });
            return result;
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
            var sql = @"
INSERT INTO {0} ({1}) VALUES ({2});
";
            var attributes = new List<string>();
            var values = new List<string>();
            var paramList = new Dictionary<string, object>();
            int count = 0;
            foreach (var item in entity.Attributes)
            {
                var paramName = $"@param{count}";
                var keyValue = DialectSql.GetSpecialValue(paramName, item.Value);
                attributes.Add(item.Key.ToString());
                values.Add(keyValue.name);
                paramList.Add(paramName, keyValue.value);
                count++;
            }
            sql = string.Format(sql, entity.EntityName, string.Join(",", attributes), string.Join(",", values));
            this.Execute(sql);
            return entity.Id;
        }

        /// <summary>
        /// 更新实体记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(BaseEntity entity)
        {
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

            return result;
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
            return _dbClient.Query<T>(sql, new Dictionary<string, object>() { { "@id", id } }).FirstOrDefault();
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
    }
}
