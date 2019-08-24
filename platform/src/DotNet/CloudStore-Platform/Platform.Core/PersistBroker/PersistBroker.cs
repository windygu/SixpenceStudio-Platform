using System;
using System.Collections.Generic;
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
        public PersistBroker() { _sqlDb = new SQLDb.SQLDb(); }

        #endregion

        private ISQLDb _sqlDb;
        public ISQLDb SqlDb => _sqlDb;
        public void Assign(BaseEntity obj)
        {
            throw new NotImplementedException();
        }

        public string Create(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public int Delete(string typeName, string id)
        {
            throw new NotImplementedException();
        }

        public int Delete(BaseEntity obj)
        {
            if (obj == null) return 0; 

            return  _sqlDb.Execute(new EntityObjectMapper(obj).GetDeleteSql());
        }

        public int Delete(BaseEntity[] objArray)
        {
            if (objArray == null || objArray.Length == 0)
            {
                return 0;
            }
            return objArray.Sum(Delete);
        }


        public int DeleteByWhere(string typeName, string where, Dictionary<string, object> paramList = null)
        {
            throw new NotImplementedException();
        }

        public BaseEntity Retrieve(string typeName, string id)
        {
            throw new NotImplementedException();
        }

        public T Retrieve<T>(string id) where T : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        public T Retrieve<T>(string sql, Dictionary<string, object> paramList) where T : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        public IList<T> RetrieveMultiple<T>(string sql, Dictionary<string, object> paramList = null) where T : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        public IList<T> RetrieveMultiple<T>(string sql, Dictionary<string, object> paramList, string orderby, int pageSize, int pageIndex) where T : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        public IList<T> RetrieveMultiple<T>(string sql, Dictionary<string, object> paramList, string orderby, int pageSize, int pageIndex, out int recordCount) where T : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        public string Save(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public int Update(BaseEntity entity)
        {
            throw new NotImplementedException();
        }
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
