using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Platform.Core.PersistBroker
{
    public static class PersisBrokerExtension
    {
        #region Query
        /// <summary>
        /// 执行数据库的查询
        /// </summary>
        public static DataTable Query(this IPersistBroker broker, string sql, IDictionary<string, object> paramList = null) =>
            broker.SqlDb.Query(sql, paramList);

        /// <summary>
        /// 执行数据库的查询 (分页）
        /// </summary>
        public static DataTable Query(this IPersistBroker broker, string sql, IDictionary<string, object> paramList, string orderby, int pageSize, int pageIndex) =>
            broker.SqlDb.Query(sql, paramList, orderby, pageSize, pageIndex);

        /// <summary>
        /// 执行数据库的查询 (分页）
        /// </summary>
        public static DataTable Query(this IPersistBroker broker, string sql, IDictionary<string, object> paramList, string orderby, int pageSize, int pageIndex, out int recordCount) =>
            broker.SqlDb.Query(sql, paramList, orderby, pageSize, pageIndex, out recordCount);

        /// <summary>
        /// 执行数据库的查询(分页）
        /// </summary>
        public static DataTable Query(this IPersistBroker broker, string sql, string orderby, int pageSize, int pageIndex) =>
            Query(broker, sql, null, orderby, pageSize, pageIndex);

        /// <summary>
        /// 执行数据库的查询(分页）
        /// </summary>
        public static DataTable Query(this IPersistBroker broker, string sql, string orderby, int pageSize, int pageIndex, out int recordCount) =>
            Query(broker, sql, null, orderby, pageSize, pageIndex, out recordCount);

        /// <summary>
        /// 执行数据库的查询(分页行数）
        /// </summary>
        public static int QueryRecordCount(this IPersistBroker broker, string sql, IDictionary<string, object> paramList = null) =>
            broker.SqlDb.QueryRecordCount(sql, paramList);

        /// <summary>
        /// 基于Dapper封装的查询
        /// </summary>
        public static IEnumerable<T> Query<T>(this IPersistBroker broker, string sql, IDictionary<string, object> paramList = null) =>
            broker.SqlDb.Query<T>(sql, paramList);

        /// <summary>
        /// 基于Dapper封装的查询
        /// </summary>
        public static IEnumerable<T> Query<T>(this IPersistBroker broker, string sql, IDictionary<string, object> paramList, string orderby, int pageSize, int pageIndex) =>
            broker.SqlDb.Query<T>(sql, paramList, orderby, pageSize, pageIndex);

        /// <summary>
        /// 基于Dapper封装的查询
        /// </summary>
        public static IEnumerable<T> Query<T>(this IPersistBroker broker, string sql, IDictionary<string, object> paramList, string orderby, int pageSize, int pageIndex, out int recordCount) =>
            broker.SqlDb.Query<T>(sql, paramList, orderby, pageSize, pageIndex, out recordCount);

        #endregion

        #region Execute
        /// <summary>
        /// 执行SQL
        /// </summary>
        public static int Execute(this IPersistBroker broker, string sql, Dictionary<string, object> paramList = null) =>
            broker.SqlDb.Execute(sql, paramList);
        #endregion
    }
}
