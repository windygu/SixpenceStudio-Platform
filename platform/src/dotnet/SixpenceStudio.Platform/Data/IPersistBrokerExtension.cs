using SixpenceStudio.Platform.Entity;
using System.Collections.Generic;
using System.Data;

namespace SixpenceStudio.Platform.Data
{
    public static class IPersistBrokerExtension
    {
        public static IEnumerable<T> Query<T>(this IPersistBroker broker, string sql, IDictionary<string, object> paramList) where T : BaseEntity, new()
        {
            return broker.DbClient.Query<T>(sql, paramList);
        }

        public static DataTable Query(this IPersistBroker broker, string sql, IDictionary<string, object> paramList)
        {
            return broker.DbClient.Query(sql, paramList);
        }
    }
}
