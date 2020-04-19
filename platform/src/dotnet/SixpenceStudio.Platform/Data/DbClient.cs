using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.Common;
using System.Collections;
using Npgsql;
using System.Configuration;
using SixpenceStudio.Platform.Utils;
using SixpenceStudio.Platform.Configs;

namespace SixpenceStudio.Platform.Data
{
    public class DbClient : IDbClient
    {
        #region 构造函数
        public DbClient() { }
        public DbClient(string connectionString)
        {
            this.connectionString = connectionString;
        }
        #endregion

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// 数据库连接实例
        /// </summary>
        private DbConnection _conn
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        #region Execute
        public int Execute(string sqlText, IDictionary<string, object> paramList = null)
        {
            return _conn.Execute(sqlText, paramList);
        }
        public object ExecuteScalar(string sql, IDictionary<string, object> paramList = null)
        {
            return _conn.ExecuteScalar(sql, paramList);
        }
        #endregion

        #region Query
        public IEnumerable<T> Query<T>(string sql, IDictionary<string, object> paramList = null)
        {
            var ret = _conn.Query<T>(sql, paramList);
            return ret;
        }
        #endregion

        #region DataTable
        public DataTable Query(string sql, IDictionary<string, object> paramList = null)
        {
            DataTable dt = new DataTable();
            var reader = _conn.ExecuteReader(sql, paramList);
            dt.Load(reader);
            return dt;
        }
        #endregion
    }

    public class DbClientFactory
    {

        public static DbClient GetDbInstance()
        {
            string connectionString = ConfigurationManager.AppSettings["DbConnectrionString"];
            DecryptAndEncryptHelper helper = new DecryptAndEncryptHelper(ConfigInformation.Key, ConfigInformation.Vector);
            var decryptionString = helper.Decrypto(connectionString);
            return new DbClient(decryptionString);
        }
    }
}
