using Dapper;
using Npgsql;
using SixpenceStudio.Platform.Logging;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace SixpenceStudio.Platform.Data
{
    /// <summary>
    /// 数据库实例
    /// </summary>
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
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public int Execute(string sql, IDictionary<string, object> paramList = null)
        {
            var paramListClone = new Dictionary<string, object>();
            if (paramList != null)
            {
                paramListClone = paramListClone.Concat(paramList).ToDictionary(k => k.Key, v => v.Value);
            }
            sql = ConvertSqlToDialectSql(sql, paramListClone);
            return _conn.Execute(sql, paramListClone);
        }

        /// <summary>
        /// 执行SQL语句，并返回第一行第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public object ExecuteScalar(string sql, IDictionary<string, object> paramList = null)
        {
            var paramListClone = new Dictionary<string, object>();
            if (paramList != null)
            {
                paramListClone = paramListClone.Concat(paramList).ToDictionary(k => k.Key, v => v.Value);
            }
            sql = ConvertSqlToDialectSql(sql, paramListClone);
            return _conn.ExecuteScalar(sql, paramListClone);
        }
        #endregion

        #region Query
        /// <summary>
        /// 执行SQL语句，并返回查询结果集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sql, IDictionary<string, object> paramList = null)
        {
            var paramListClone = new Dictionary<string, object>();
            if (paramList != null)
            {
                paramListClone = paramListClone.Concat(paramList).ToDictionary(k => k.Key, v => v.Value);
            }
            sql = ConvertSqlToDialectSql(sql, paramListClone);
            var ret = _conn.Query<T>(sql, paramListClone);
            return ret;
        }
        #endregion

        #region DataTable
        /// <summary>
        /// 执行SQL语句，并返回查询结果集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public DataTable Query(string sql, IDictionary<string, object> paramList = null)
        {
            var paramListClone = new Dictionary<string, object>();
            if (paramList != null)
            {
                paramListClone = paramListClone.Concat(paramList).ToDictionary(k => k.Key, v => v.Value);
            }
            sql = ConvertSqlToDialectSql(sql, paramListClone);
            DataTable dt = new DataTable();
            var reader = _conn.ExecuteReader(sql, paramListClone);
            dt.Load(reader);
            return dt;
        }
        #endregion

        /// <summary>
        /// 将SQL转换为本地化SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paramsList"></param>
        /// <returns></returns>
        public string ConvertSqlToDialectSql(string sql, IDictionary<string, object> paramsList)
        {
            if (paramsList == null || paramsList.Count == 0)
            {
                return sql;
            }
            if (sql.Contains("in@"))
            {
                var toRemovedParamNameList = new Dictionary<string, Dictionary<string, object>>();
                var paramValueNullList = new List<string>(); // 记录传入的InList参数的Value如果为空或者没有值的特殊情况

                foreach (var paramName in paramsList.Keys)
                {
                    if (!paramName.ToLower().StartsWith("in")) continue;
                    var paramValue = paramsList[paramName]?.ToString();
                    if (string.IsNullOrWhiteSpace(paramValue))
                    {
                        paramValueNullList.Add(paramName);
                        continue;
                    }

                    toRemovedParamNameList.Add(paramName, new Dictionary<string, object>());
                    var inListValues = paramValue.Split(',');
                    for (var i = 0; i < inListValues.Length; i++)
                    {
                        toRemovedParamNameList[paramName].Add(paramName.Substring(2, paramName.Length - 2) + i, inListValues[i]);
                    }
                }

                foreach (var paramNameRemoved in toRemovedParamNameList.Keys)
                {
                    paramsList.Remove(paramNameRemoved);
                    foreach (var paramNameAdd in toRemovedParamNameList[paramNameRemoved].Keys)
                    {
                        paramsList.Add(paramNameAdd, toRemovedParamNameList[paramNameRemoved][paramNameAdd]);
                    }

                    var newParamNames = toRemovedParamNameList[paramNameRemoved].Keys.Aggregate((l, n) => l + "," + n);
                    sql = sql.Replace(paramNameRemoved, newParamNames);
                }

                foreach (var paramValueNullName in paramValueNullList)
                {
                    paramsList.Remove(paramValueNullName);
                    sql = sql.Replace(paramValueNullName, "null");
                }

                return sql;
            }
            return sql;
        }
    }
}
