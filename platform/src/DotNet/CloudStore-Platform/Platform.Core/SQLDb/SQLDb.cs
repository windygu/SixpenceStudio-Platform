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
            return DateTime.Now;
        }


        /// <summary>
        /// 查询数据库
        /// </summary>
        /// <typeparam name="T">返回实体</typeparam>
        /// <param name="sql">Sql查询语句</param>
        /// <param name="paramList">参数</param>
        /// <param name="orderby">OrderBy排序语句</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">页数</param>
        /// <returns>IEnumerable<T>查询数据</returns>
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

        /// <summary>
        /// 查询数据库
        /// </summary>
        /// <param name="sql">Sql查询语句</param>
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
            var reader =  _conn.ExecuteReader(sql, paramList);
            dt.Load(reader);
            return dt;
        }

        /// <summary>
        /// 查询数据库
        /// </summary>
        /// <param name="sql">Sql查询语句</param>
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

        /// <summary>
        /// 查询数据库
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="sql">Sql查询语句</param>
        /// <param name="paramList">参数</param>
        /// <returns>IEnumerable<T>查询结果</returns>
        public IEnumerable<T> Select<T>(string sql, IDictionary<string, object> paramList = null)
        {
            if (string.IsNullOrEmpty(sql))
            {
                return new List<T>();
            }

            return _conn.Query<T>(sql, paramList);
        }

        /// <summary>
        /// 查询数据库
        /// </summary>
        /// <param name="sql">Sql查询语句</param>
        /// <param name="paramList">参数</param>
        /// <returns>DataTable查询结果</returns>
        public DataTable Select(string sql, IDictionary<string, object> paramList = null)
        {
            if (string.IsNullOrEmpty(sql))
            {
                return new DataTable();
            }

            DataTable dt = new DataTable();
            var reader = _conn.ExecuteReader(sql, paramList);
            dt.Load(reader);
            return dt;
        }
    }
}
