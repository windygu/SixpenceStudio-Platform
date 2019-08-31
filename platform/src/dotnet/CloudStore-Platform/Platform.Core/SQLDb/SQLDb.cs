using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Platform.Core.SQLDb
{
    internal sealed class SQLDb : ISQLDb
    {
        private SqlConnection _conn;
        public IDbConnection DbConnection => _conn;
        public ConnectionState ConnectionState => DbConnection.State;

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Close()
        {
            _conn.Close();
        }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public void Open()
        {
            _conn.Open();
        }

        /// <summary>
        /// 初始化数据库连接
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        public void Initalize(string connectionString)
        {
            _conn.ConnectionString = connectionString;
        }

        /// <summary>
        /// 执行Sql
        /// </summary>
        /// <param name="sqlText">sql语句</param>
        /// <param name="paramList">参数</param>
        /// <returns></returns>
        public int Execute(string sqlText, IDictionary<string, object> paramList = null)
        {
            return _conn.Execute(sqlText, paramList);
        }

        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetDbDateTime()
        {
            //应该使用sql方式去获取服务器时间
            return DateTime.Now;
        }

        #region Dapper Query
        /// <summary>
        /// 执行数据库的查询(分页行数）
        /// </summary>
        public int QueryRecordCount(string sqlText, IDictionary<string, object> paramList = null)
        {
            var pagersql = $"select count(*) as RecordCount from ({sqlText} )SOURCEQUERY";
            return (int)_conn.ExecuteScalar(sqlText, paramList);
        }

        /// <summary>
        /// 查询数据库带参数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paramList">参数</param>
        /// <returns>查询数据</returns>
        public IEnumerable<T> Query<T>(string sql, IDictionary<string, object> paramList = null)
        {
            return _conn.Query<T>(sql, paramList);
        }

        public IEnumerable<T> Query<T>(string sql, IDictionary<string, object> paramList, string orderby, int pageSize, int pageIndex)
        {
            if (!string.IsNullOrEmpty(orderby))
            {
                sql += $" ORDER BY {orderby}";
            }

            if (!string.IsNullOrEmpty(pageSize.ToString()) && !string.IsNullOrEmpty(pageIndex.ToString()))
            {
                sql += $" LIMIT {pageIndex * pageSize}, {pageSize}";
            }
            return _conn.Query<T>(sql, paramList);
        }

        public IEnumerable<T> Query<T>(string sql, IDictionary<string, object> paramList, string orderby, int pageSize, int pageIndex, out int recordCount)
        {
            if (!string.IsNullOrEmpty(orderby))
            {
                sql += $" ORDER BY {orderby}";
            }

            if (!string.IsNullOrEmpty(pageSize.ToString()) && !string.IsNullOrEmpty(pageIndex.ToString()))
            {
                sql += $" LIMIT {pageIndex * pageSize}, {pageSize}";
            }
            recordCount = QueryRecordCount(sql, paramList);
            return _conn.Query<T>(sql, paramList);
        }
        #endregion

        #region Dapper Query DataTable
        /// <summary>
        /// 查询数据库，返回DataTable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paramList">参数</param>
        /// <returns></returns>
        public DataTable Query(string sql, IDictionary<string, object> paramList = null)
        {
            using (SqlDataAdapter adp = new SqlDataAdapter(sql, _conn))
            {
                DataSet ds = new DataSet();
                adp.Fill(ds);
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// 查询数据库，返回DataTable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paramList">参数</param>
        /// <param name="orderby">OrderBy排序语句</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">页数</param>
        /// <returns>DataTable查询结果</returns>
        public DataTable Query(string sql, IDictionary<string, object> paramList, string orderby, int pageSize, int pageIndex)
        {
            if (!string.IsNullOrEmpty(orderby))
            {
                sql += $" ORDER BY {orderby}";
            }

            if (!string.IsNullOrEmpty(pageSize.ToString()) && !string.IsNullOrEmpty(pageIndex.ToString()))
            {
                sql += $" LIMIT {pageIndex * pageSize}, {pageSize}";
            }

            DataTable dt = new DataTable();
            var reader = _conn.ExecuteReader(sql, paramList);
            dt.Load(reader);
            return dt;
        }

        /// <summary>
        /// 查询数据库，返回DataTable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paramList">参数</param>
        /// <param name="orderby">OrderBy排序语句</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="recordCount">结果数量</param>
        /// <returns>DataTable查询结果</returns>
        public DataTable Query(string sql, IDictionary<string, object> paramList, string orderby, int pageSize, int pageIndex, out int recordCount)
        {
            if (string.IsNullOrEmpty(sql))
            {
                recordCount = 0;
                return new DataTable();
            }

            var result = _conn.ExecuteScalar(sql, paramList);
            recordCount = result == null ? 0 : Convert.ToInt32(result);

            if (!string.IsNullOrEmpty(orderby))
            {
                sql += $" ORDER BY {orderby}";
            }

            if (!string.IsNullOrEmpty(pageSize.ToString()) && !string.IsNullOrEmpty(pageIndex.ToString()))
            {
                sql += $" LIMIT {pageIndex * pageSize}, {pageSize}";
            }

            DataTable dt = new DataTable();
            var reader = _conn.ExecuteReader(sql, paramList);
            dt.Load(reader);
            return dt;
        }
        #endregion

        public IDbTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void BeginTransaction(string id)
        {
            throw new NotImplementedException();
        }

        public void BeginTransaction(IList<string> idList)
        {
            throw new NotImplementedException();
        }

        public void CommitTransaction()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public void SetCommandTimeOutSQL(int timeOut)
        {
            throw new NotImplementedException();
        }

        public void SetCommandTimeOutProcedure(int timeOut)
        {
            throw new NotImplementedException();
        }

        #region Dapper ExecuteReader
        public IDataReader ExecuteReader(string sql, IDictionary<string, object> paramList = null)
        {
            return _conn.ExecuteReader(sql, paramList);
        }

        public IDataReader ExecuteReader(string sql, IDictionary<string, object> paramList, string orderby, int pageSize, int pageIndex)
        {
            if (!string.IsNullOrEmpty(orderby))
            {
                sql += $" ORDER BY {orderby}";
            }

            if (!string.IsNullOrEmpty(pageSize.ToString()) && !string.IsNullOrEmpty(pageIndex.ToString()))
            {
                sql += $" LIMIT {pageIndex * pageSize}, {pageSize}";
            }
            return _conn.ExecuteReader(sql, paramList);
        }

        public IDataReader ExecuteReader(string sql, IDictionary<string, object> paramList, string orderby, int pageSize, int pageIndex, out int recordCount)
        {
            if (!string.IsNullOrEmpty(orderby))
            {
                sql += $" ORDER BY {orderby}";
            }

            if (!string.IsNullOrEmpty(pageSize.ToString()) && !string.IsNullOrEmpty(pageIndex.ToString()))
            {
                sql += $" LIMIT {pageIndex * pageSize}, {pageSize}";
            }
            recordCount = QueryRecordCount(sql, paramList);
            return _conn.ExecuteReader(sql, paramList);
        }
        #endregion

    }
}
