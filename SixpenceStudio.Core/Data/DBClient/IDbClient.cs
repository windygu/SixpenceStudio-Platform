﻿using SixpenceStudio.Core.Data.Dialect;
using System.Collections.Generic;
using System.Data;

namespace SixpenceStudio.Core.Data
{
    public interface IDbClient
    {
        /// <summary>
        /// 初始化数据库的连接
        /// </summary>
        /// <param name="connectinString">数据库的连接字符串</param>
        void Initialize(string connectinString);

        /// <summary>
        /// 数据库方言
        /// </summary>
        IDBDialect Dialect { get; }

        /// <summary>
        /// 获取数据库的连接
        /// </summary>
        /// <returns></returns>
        IDbConnection DbConnection { get; }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        void Open();

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        void Close();

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        IDbTransaction BeginTransaction();
        
        /// <summary>
        /// 提交事务
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// 回滚
        /// </summary>
        void Rollback();

        /// <summary>
        /// 连接状态
        /// </summary>
        ConnectionState ConnectionState { get; }

        /// <summary>
        /// 执行SQL，返回受影响行数
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        int Execute(string sqlText, IDictionary<string, object> paramList = null);

        /// <summary>
        /// 根据SQL查询，返回传入类型集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string sql, IDictionary<string, object> paramList = null);

        /// <summary>
        /// 根据SQL查询，返回DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        DataTable Query(string sql, IDictionary<string, object> paramList = null);

        /// <summary>
        /// 执行SQL，返回第一行第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        object ExecuteScalar(string sql, IDictionary<string, object> paramList = null);

        /// <summary>
        /// 创建临时表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="newTableName"></param>
        /// <returns></returns>
        string CreateTemporaryTable(string tableName);

        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="tableName"></param>
        void DropTable(string tableName);

        /// <summary>
        /// 拷贝数据
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="tableName"></param>
        void BulkCopy(DataTable dataTable, string tableName);
    }
}
