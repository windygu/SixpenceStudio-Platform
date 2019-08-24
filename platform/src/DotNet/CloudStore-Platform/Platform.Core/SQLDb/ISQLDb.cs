using System;
using System.Data;
using System.Collections.Generic;

namespace Platform.Core.SQLDb
{
    /// <summary>
    /// 数据库行为接口类
    /// </summary>
    public interface ISQLDb
    {
        /// <summary>
        /// 初始化数据库连接字符串
        /// </summary>
        /// <param name="connectionString">数据库的连接字符串</param>
        void Initalize(string connectionString);

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        void Open();
        
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        void Close();

        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <returns></returns>
        DateTime GetDbDateTime();

        /// <summary>
        /// 获取数据库的连接
        /// </summary>
        /// <returns></returns>
        IDbConnection DbConnection { get; }

        /// <summary>
        /// 查询数据库数据
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="paramList">参数列表</param>
        /// <returns>查询结果的对象集合</returns>
        IEnumerable<T> Select<T>(string sql, IDictionary<string, object> paramList = null);

        /// <summary>
        /// 查询数据库数据
        /// </summary>
        /// <typeparam name="T">返回的DTO数据的类型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="paramList">参数列表</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <returns>查询结果的对象集合</returns>
        IEnumerable<T> Query<T>(string sql, IDictionary<string, object> paramList, string orderby, int pageSize, int pageIndex);

        /// <summary>
        /// 执行带参数的sql语句，返回DataTable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paramList">sql参数</param>
        /// <returns>DataTable</returns>
        DataTable Select(string sql, IDictionary<string, object> paramList = null);

        /// <summary>
        /// 执行数据库的查询 (分页）
        /// </summary>
        DataTable Query(string sql, IDictionary<string, object> paramList, string orderby, int pageSize, int pageIndex);

        /// <summary>
        /// 执行数据库的查询 (分页）
        /// </summary>
        DataTable Query(string sql, IDictionary<string, object> paramList, string orderby, int pageSize, int pageIndex, out int recordCount);

        /// <summary>
        /// 执行带参数的sql语句
        /// </summary>
        /// <param name="sqlText">sql语句</param>
        /// <param name="paramList">sql参数</param>
        /// <returns>影响的数据库行数</returns>
        int Execute(string sqlText, IDictionary<string, object> paramList = null);
    }
}
