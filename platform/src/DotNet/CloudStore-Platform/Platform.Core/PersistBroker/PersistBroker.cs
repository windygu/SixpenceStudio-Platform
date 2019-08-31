using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Platform.Core.Entity;
using Platform.Core.SQLDb;

namespace Platform.Core.PersistBroker
{
    internal sealed class PersistBroker : IPersistBroker
    {
        #region 构造函数
        public PersistBroker()
        {
            Initialize("");
        }

        #endregion

        private ISQLDb _sqlDb;
        public ISQLDb SqlDb => _sqlDb;

        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <param name="dbConnectionString"></param>
        private void Initialize(string dbConnectionString)
        {
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                throw new CSException("AFF1F98B-531E-4219-AC08-2FE5E1993090", "数据库连接字符串为空");
            }
            _sqlDb.Initalize(dbConnectionString);
            _sqlDb = new SQLDb.SQLDb();
        }

        #region Retrieve
        /// <summary>
        /// 根据实体类型名字和实体Id获取实体对象的实例
        /// </summary>
        /// <param name="typeName">实体类型名字</param>
        /// <param name="id">实体Id</param>
        /// <returns></returns>
        public BaseEntity Retrieve(string typeName, string id)
        {
            var sql = string.Format("select * from {0} where {0}id = '{1}'", typeName, id);
            var entities = RetrieveMultiple(typeName, sql, null);
            return entities != null && entities.Count > 0 ? entities[0] : null;
        }

        /// <summary>
        /// 根据 实体T 和 实体Id 获取实体对象实例
        /// </summary>
        public T Retrieve<T>(string id)
            where T : BaseEntity, new()
        {
            string s = typeof(T).ToString();
            var typeName = s.Substring(s.LastIndexOf(".", StringComparison.Ordinal + 1));
            var sql = string.Format("select * from {0} where {0}id='{1}'", typeName, id);
            var list = RetrieveMultiple<T>(sql);
            return list.Count > 0 ? list[0] : default(T);
        }

        /// <summary>
        /// 根据查询条件查询实体对象
        /// </summary>
        public T Retrieve<T>(string sql, Dictionary<string, object> paramList)
            where T : BaseEntity, new()
        {
            var list = RetrieveMultiple<T>(sql, paramList);
            return list.Count > 0 ? list[0] : default(T);
        }
        #endregion

        #region RetrieveMultiple
        public IList<T> RetrieveMultiple<T>(string sql, Dictionary<string, object> paramList = null)
            where T : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        public IList<T> RetrieveMultiple<T>(string sql, Dictionary<string, object> paramList, string orderby, int pageSize, int pageIndex)
            where T : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        public IList<T> RetrieveMultiple<T>(string sql, Dictionary<string, object> paramList, string orderby, int pageSize, int pageIndex, out int recordCount)
          where T : BaseEntity, new()
        {
            using (var reader = _sqlDb.ExecuteReader(sql, paramList, orderby, pageSize, pageIndex))
            {
                return ParseDataReader2Entity<T>(reader);
            }
        }

        /// <summary>
        /// 根据查询条件查询实体的对象列表
        /// </summary>
        private IList<BaseEntity> RetrieveMultiple(string typeName, string sql, Dictionary<string, object> paramList)
        {
            using (var reader = _sqlDb.ExecuteReader(sql, paramList))
            {
                var type = ServiceObjectFactory.GetTypeOfEntityByName(typeName);
                return ParseDataReader2Entity(reader, type);
            }

        }
        #endregion

        #region Create Update Delete
        /// <summary>
        /// 创建实体对象
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>创建行数</returns>
        public string Create(BaseEntity entity)
        {
            if (entity == null) return "";

            if (!entity.Attributes.ContainsKey("CreatedOn") || entity.Attributes["CreatedOn"] == null)
            {
                entity.Attributes["CreatedOn"] = DateTime.Now;
            }

            if (!entity.Attributes.ContainsKey("ModifiedOn") || entity.Attributes["ModifiedOn"] == null)
            {
                entity.Attributes["ModifiedOn"] = DateTime.Now;
            }

            var eo = new EntityObjectMapper(entity);

            string sql;
            Dictionary<string, object> paramList;
            eo.GetParameterizedInsertSql(out sql, out paramList);
            _sqlDb.Execute(sql, paramList);

            return entity.Id;
        }

        /// <summary>
        /// 保存实体，系统根据ID自动判断更新还是新建(实体的Id不为空，则是Update，否则是Create
        /// </summary>
        public string Save(BaseEntity entity)
        {
            string id = null;

            var keyAttributeName = entity.GetKeyProperty().Name;
            if (entity.Attributes.ContainsKey(keyAttributeName))
            {
                id = entity.Attributes[keyAttributeName] as string;
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                id = Create(entity);
            }
            else
            {
                Update(entity);
            }

            return id;
        }

        /// <summary>
        /// 更新实体记录
        /// </summary>
        /// <param name="entity">实体对象实例</param>
        /// <returns>影响的行数</returns>
        public int Update(BaseEntity entity)
        {
            entity.Attributes["ModifiedOn"] = DateTime.Now;
            var eo = new EntityObjectMapper(entity);
            string sql;
            Dictionary<string, object> paramList;
            eo.GetParameterizedUpdateSql(out sql, out paramList);
            return _sqlDb.Execute(sql, paramList);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>删除行数</returns>
        public int Delete(BaseEntity entity)
        {
            if (entity == null) return 0;

            return _sqlDb.Execute(new EntityObjectMapper(entity).GetDeleteSql());
        }

        /// <summary>
        /// 删除数据的实体记录
        /// </summary>
        /// <param name="typeName">表的名字</param>
        /// <param name="id">记录的主键Id</param>
        /// <returns>影响的数据库行数</returns>
        public int Delete(string typeName, string id)
        {
            if (string.IsNullOrEmpty(typeName) || string.IsNullOrEmpty(id)) return 0;

            return Delete(new XrmEntity(typeName, id));
        }

        /// <summary>
        /// 删除实体记录
        /// </summary>
        /// <param name="objArray">实体数组</param>
        /// <returns>影响的记录行数</returns>
        public int Delete(BaseEntity[] objArray)
        {
            if (objArray == null || objArray.Length == 0)
            {
                return 0;
            }
            return objArray.Sum(Delete);
        }

        /// <summary>
        /// 删除实体记录，根据Where条件
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="where"></param>
        /// <param name="paramList"></param>
        /// <returns>影响的记录行数</returns>
        public int DeleteByWhere(string typeName, string where, Dictionary<string, object> paramList = null)
        {
            string sql = string.Format("select {0}id from {0}", typeName);
            if (!where.ToLower().Contains("where"))
                sql = sql + " where ";

            sql += " " + where;
            var dt = _sqlDb.Query(sql, paramList);

            if (dt == null || dt.Rows.Count == 0) return 0;

            var n = 0;
            foreach (DataRow dr in dt.Rows)
            {
                n += Delete(typeName, dr[0] == null ? "" : dr[0].ToString());
            }
            return n;
        }
        #endregion


    }

    public sealed class EntityObjectMapper
    {
        private readonly BaseEntity _entity;

        #region 构造函数
        public EntityObjectMapper(BaseEntity entity)
        {
            _entity = entity;
        }
        #endregion

        #region Template
        /// <summary>
        /// 删除记录的Sql模板
        /// </summary>
        private const string DeleteTemplate = @"delete from {0}Base  where {0}id='{1}'";

        /// <summary>
        /// 更新记录的Sql模板
        /// </summary>
        private const string UpdateTemplate = @"update {0} set {1} where {0}Id = '{2}'";

        /// <summary>
        /// 新增记录的Sql模板
        /// </summary>
        private const string InsertTemplate = @"insert into {0}({1}) values ({2})";
        #endregion

        #region Insert Sql
        public void GetParameterizedInsertSql(out string outSql, out Dictionary<string, object> outParamList)
        {
            var entityTypeName = _entity.EntityName;
            var fieldNames = string.Empty;// 字段列表
            var paramNames = string.Empty; //字段参数值列表

            if (string.IsNullOrEmpty(_entity.Id))
            {
                _entity.Id = Guid.NewGuid().ToString();
            }
            outParamList = new Dictionary<string, object>(_entity.Attributes.Count);

            using (var en = _entity.Attributes.GetEnumerator())
            {
                for (var i = 0; i < _entity.Attributes.Count; i++)
                {
                    if (!en.MoveNext())
                    {
                        continue;
                    }
                    var key = en.Current.Key;
                    if (string.IsNullOrWhiteSpace(key))
                    {
                        continue;
                    }
                    var v = en.Current.Value;
                    var paramName = "@" + key;

                    if (v == null)
                    {
                        continue;
                    }

                    fieldNames += "," + key;
                    paramNames += "," + paramName;

                    if (v != null)
                    {
                        if (v is JToken)
                        {
                            paramNames += " ::jsonb ";
                        }
                    }

                    outParamList.Add(paramName, GetAttributeValue(v));
                }
            }
            outSql = string.Format(InsertTemplate,
                entityTypeName + "base",
                fieldNames.Substring(1),
                paramNames.Substring(1)
            );
        }

        #endregion

        #region Delete Sql
        public string GetDeleteSql()
        {
            return string.Format(DeleteTemplate, _entity.EntityName, _entity.Id);
        }
        #endregion

        #region Update Sql
        /// <summary>
        /// 获取更新sql
        /// </summary>
        /// <param name="outSql">更新sql</param>
        /// <param name="outParams">更新参数</param>
        public void GetParameterizedUpdateSql(out string outSql, out Dictionary<string ,object> outParams)
        {
            if (string.IsNullOrEmpty(_entity.Id) || _entity.Id == Guid.Empty.ToString())
            {
                throw new Exception("update id can not be null");
            }

            if (_entity.Attributes == null || _entity.Attributes.Count ==0)
            {
                outSql = string.Empty;
                outParams = null;
                return;
            }

            var entityName = _entity.EntityName;
            var setSql = string.Empty; //字段列表
            var id = _entity.Id;

            outParams = new Dictionary<string, object>(_entity.Attributes.Count);
            foreach (var attribute in _entity.Attributes)
            {
                var key = attribute.Key;
                var v = attribute.Value;

                var paramName = "@" + key;

                setSql += "," + key + "=" + paramName + "";

                if (v != null)
                {
                    var type = v.GetType();
                    if (v is JToken)
                    {
                        setSql += " ::jsonb ";
                    }
                }

                outParams.Add(paramName, GetAttributeValue(v));
            }
            setSql = setSql.Substring(1);

            outSql = string.Format(UpdateTemplate,
                entityName,
                setSql,
                id
            );
        }
        private static object GetAttributeValue(object o)
        {
            if (o == null || o is DBNull)
            {
                return DBNull.Value;
            }

            object result = null;

            var type = o.GetType();

            if (type == typeof(bool?) || type == typeof(bool))
            {
                var b = o as bool?;
                result = b == true ? "1" : "0";
            }
            else if (type == typeof(string))
            {
                var s = o.ToString();
                result = string.IsNullOrWhiteSpace(s) ? DBNull.Value : o;
            }
            else if (o is JToken)
            {
                if (o == null || (o as JToken).Type == JTokenType.Null)
                {
                    result = DBNull.Value;
                }
                else
                {
                    return (o as JToken).ToString();
                }
            }
            else
            {
                result = o;
            }

            return result ?? DBNull.Value;
        }
        #endregion
    }
}
