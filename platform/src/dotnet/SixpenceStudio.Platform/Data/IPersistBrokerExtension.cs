using SixpenceStudio.Platform.Entity;
using System;
using System.Collections.Generic;
using System.Data;

namespace SixpenceStudio.Platform.Data
{
    public static class IPersistBrokerExtension
    {
        /// <summary>
        /// 通过lambda表达式的方式执行数据库事务
        /// </summary>
        public static void ExecuteTransaction(this IPersistBroker broker, Action func)
        {
            try
            {
                broker.DbClient.Open();
                broker.DbClient.BeginTransaction();

                func?.Invoke();

                broker.DbClient.CommitTransaction();
            }
            catch
            {
                broker.DbClient.Rollback();
                throw;
            }
            finally
            {
                broker.DbClient.Close();
            }
        }

        /// <summary>
        /// 通过lambda表达式的方式执行数据库事务
        /// </summary>
        public static T ExecuteTransaction<T>(this IPersistBroker broker, Func<T> func, string transId = null)
        {
            try
            {
                broker.DbClient.Open();
                broker.DbClient.BeginTransaction();

                var t = default(T);

                if (func != null)
                {
                    t = func();
                }

                broker.DbClient.CommitTransaction();

                return t;
            }
            catch
            {
                broker.DbClient.Rollback();
                throw;
            }
            finally
            {
                broker.DbClient.Close();
            }

        }

        public static IEnumerable<T> Query<T>(this IPersistBroker broker, string sql, IDictionary<string, object> paramList = null) where T : BaseEntity, new()
        {
            return broker.DbClient.Query<T>(sql, paramList);
        }

        public static DataTable Query(this IPersistBroker broker, string sql, IDictionary<string, object> paramList = null)
        {
            return broker.DbClient.Query(sql, paramList);
        }
    }
}
