using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Data
{
    public class DialectSql
    {
        public static (string sql, Dictionary<string, object> paramsList) GetSearchCondition(SearchType type, string paramName, object value, ref int count)
        {
            switch (type)
            {
                case SearchType.Equals:
                    return ($"= @{paramName}{count}", new Dictionary<string, object>() { { $"@{paramName}{count++}", value } });
                case SearchType.Like:
                    return ($"LIKE @{paramName}{count}", new Dictionary<string, object>() { { $"@{paramName}{count++}", value } });
                case SearchType.Greater:
                    return ($"> @{paramName}{count}", new Dictionary<string, object>() { { $"@{paramName}{count++}", value } });
                case SearchType.Less:
                    return ($"< @{paramName}{count}", new Dictionary<string, object>() { { $"@{paramName}{count++}", value } });
                case SearchType.Between:
                    var param1 = $"@{paramName}{count++}";
                    var param2 = $"@{paramName}{count++}";
                    var arr = JsonConvert.DeserializeObject<List<object>>(value?.ToString());
                    return ($"BETWEEN {param1} AND {param2}", new Dictionary<string, object>() { { param1, arr[0] }, { param2, arr[1] } });
                case SearchType.Contains:
                    var param = JsonConvert.DeserializeObject<List<object>>(value?.ToString());
                    return ($"IN (in@{paramName}{count})", new Dictionary<string, object>() { { $"in@{paramName}{count++}", string.Join(",", param) } });
                case SearchType.NotContains:
                    param = JsonConvert.DeserializeObject<List<object>>(value?.ToString());
                    return ($"NOT IN (in@{paramName}{count})", new Dictionary<string, object>() { { $"in@{paramName}{count++}", string.Join(",", param) } });
                default:
                    return ("", new Dictionary<string, object>(){ });
            }
        }

        /// <summary>
        /// 格式化特殊类型
        /// </summary>
        /// <param name="originName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static (string name, object value) GetSpecialValue(string name, object value)
        {
            if (value !=null && value is JToken)
            {
                return (name + "::json", JsonConvert.SerializeObject(value));
            }
            else if (value != null && value is Boolean)
            {
                return (name, (bool)value ? 1 : 0);
            }
            return (name, value);
        }
    }
}
