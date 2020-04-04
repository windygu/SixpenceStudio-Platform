using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Data
{
    public interface IDbClient
    {
        int Execute(string sqlText, IDictionary<string, object> paramList = null);

        IEnumerable<T> Query<T>(string sql, IDictionary<string, object> paramList = null);

        object ExecuteScalar(string sql, IDictionary<string, object> paramList = null);
    }
}
