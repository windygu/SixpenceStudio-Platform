using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Platfrom.SQLDBLib.SQLDB
{
    internal sealed class SQLDB : ISQLDB
    {
        private DbConnection _conn;
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

        public int Execute(string sqlText, IDictionary<string, object> paramList = null)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDbDateTime()
        {
            throw new NotImplementedException();
        }



        public IEnumerable<T> Query<T>(string sql, IDictionary<string, object> paramList, string orderby, int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public DataTable Query(string sql, IDictionary<string, object> paramList, string orderby, int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public DataTable Query(string sql, IDictionary<string, object> paramList, string orderby, int pageSize, int pageIndex, out int recordCount)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Select<T>(string sql, IDictionary<string, object> paramList = null)
        {
            throw new NotImplementedException();
        }

        public DataTable Select(string sql, IDictionary<string, object> paramList = null)
        {
            throw new NotImplementedException();
        }
    }
}
