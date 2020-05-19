using Dapper;
using log4net;
using Npgsql;
using SixpenceStudio.Platform.Logging;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace SixpenceStudio.Platform.Data
{
    internal sealed class DbClient : IDbClient
    {

        /// <summary>
        /// 数据库连接实例
        /// </summary>
        private DbConnection _conn;
        public IDbConnection DbConnection => _conn;

        /// <summary>
        /// 初始化数据库连接
        /// </summary>
        /// <param name="connectinString"></param>
        public void Initialize(string connectinString)
        {
            _conn = new NpgsqlConnection(connectinString);
        }

        /// <summary>
        ///获取数据库连接状态 
        /// </summary>
        /// <returns></returns>
        public ConnectionState ConnectionState => _conn.State;

        #region 开启数据库连接
        //数据库打开、关闭的计数器
        private int _dbOpenCounter;

        /// <summary>
        ///打开数据库的连接（如果已经Open，则忽略）
        /// </summary>
        public void Open()
        {
            //counter = 0代表没有打开过，否则说明已经打开过了，不需要再打开
            if (_dbOpenCounter++ == 0)
            {
                if (_conn.State != ConnectionState.Open)
                    _conn.Open();
            }

        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Close()
        {
            //counter先自减1，然后判断是否=0，是的话代表是最后一次关闭
            if (--_dbOpenCounter == 0)
            {
                if (_conn.State != ConnectionState.Closed)
                {
                    _conn?.Close();
                }
            }
        }
        #endregion

        #region 事务

        private DbTransaction _trans;
        private int _transCounter;

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        public IDbTransaction BeginTransaction()
        {
            if (_transCounter++ == 0)
            {
                _trans = _conn.BeginTransaction();
            }
            return _trans;
        }

        /// <summary>
        /// 提交数据库的事务
        /// </summary>
        public void CommitTransaction()
        {
            if (--_transCounter == 0)
            {
                _trans?.Commit();
                _trans?.Dispose();
                _trans = null;
            }
        }

        /// <summary>
        /// 回滚数据库的事务
        /// </summary>
        public void Rollback()
        {
            try
            {
                if (--_transCounter == 0)
                {
                    _trans?.Rollback();
                    _trans?.Dispose();
                    _trans = null;
                }
            }
            finally
            {
                if (_transCounter == 0)
                    _trans = null;
            }
        }
        #endregion

        #region Execute
        public int Execute(string sqlText, IDictionary<string, object> paramList = null)
        {
            LogUtils.DebugLog(sqlText + LogUtils.FormatDictonary(paramList));
            return _conn.Execute(sqlText, paramList);
        }
        public object ExecuteScalar(string sql, IDictionary<string, object> paramList = null)
        {
            LogUtils.DebugLog(sql + LogUtils.FormatDictonary(paramList));
            return _conn.ExecuteScalar(sql, paramList);
        }
        #endregion

        #region Query
        public IEnumerable<T> Query<T>(string sql, IDictionary<string, object> paramList = null)
        {
            LogUtils.DebugLog(sql + LogUtils.FormatDictonary(paramList));
            var ret = _conn.Query<T>(sql, paramList);
            return ret;
        }
        #endregion

        #region DataTable
        public DataTable Query(string sql, IDictionary<string, object> paramList = null)
        {
            LogUtils.DebugLog(sql + LogUtils.FormatDictonary(paramList));
            DataTable dt = new DataTable();
            var reader = _conn.ExecuteReader(sql, paramList);
            dt.Load(reader);
            return dt;
        }
        #endregion
    }
}
