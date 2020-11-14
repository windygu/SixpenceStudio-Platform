using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Utils
{
    /// <summary>
    /// 字符串扩展类
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 判断字符串是否存在于一个字符串中
        /// </summary>
        /// <param name="source"></param>
        /// <param name="toCheck"></param>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        /// <summary>
        /// 转换Dictionary参数为日志文本格式
        /// </summary>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public static string ToLogString(this Dictionary<string, object> paramList)
        {
            if (paramList == null)
            {
                return "";
            }
            var list = new List<string>();
            foreach (var item in paramList)
            {
                var str = $"{item.Key}: {item.Value}";
                list.Add(str);

            }
            return "\r\n" + string.Join("\r\n", list);
        }

    }
}
