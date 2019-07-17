using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Data.Entity;
using Platfrom.SQLDBLib.SQLDB;

namespace Platform.Data.PersistBroker
{
    internal sealed class PersistBroker : IPersistBroker
    {
        #region 构造函数
        public PersistBroker() { }

        #endregion

        private ISQLDB _sqlDb;
        public ISQLDB SqlDb => _sqlDb;
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
            if (obj == null)
            {
                return 0;
            }

            var result = _sqlDb.Execute(new EntityObjectMapper(obj).);
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
        public EntityObjectMapper(BaseEntity entity)
        {
            _entity = entity;
        }
        #region Template
        private const string DeleteTemplate = "delete from {0}Base  where {0}id='{1}'";
        #endregion

        #region Delete Sql
        public string GetDeleteSql()
        {
            return string.Format(DeleteTemplate, _entity.EntityName, _entity.Id);
        }
        #endregion
    }
}
