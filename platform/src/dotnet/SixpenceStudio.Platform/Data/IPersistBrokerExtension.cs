using SixpenceStudio.Platform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Data
{
    public static class IPersistBrokerExtension
    {
        public static IEnumerable<T> Query<T>(this IPersistBroker broker, string sql, Dictionary<string, object> paramList) where T : BaseEntity, new()
        {
            return broker.DbClient.Query<T>(sql, paramList);
        }
    }
}
