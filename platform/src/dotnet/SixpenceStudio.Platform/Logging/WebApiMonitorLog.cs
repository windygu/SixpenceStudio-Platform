using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Logging
{
    public class WebApiMonitorLog
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }

        public DateTime ExecuteStartTime { get; set; }
        public DateTime ExecuteEndTime { get; set; }

        /// <summary>
        /// 请求的Action 参数
        /// </summary>
        public Dictionary<string, object> ActionParams { get; set; }

        /// <summary>
        /// Http请求头
        /// </summary>
        public string HttpRequestHeaders { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        public string Url 
        { 
            get 
            {
                return System.Web.HttpContext.Current.Request.Url.ToString();
            }
        }

        /// <summary>
        /// 获取监控指标日志
        /// </summary>
        /// <param name="mtype"></param>
        /// <returns></returns>
        public string GetLoginfo()
        {
            string Msg = "WebApi: {0}\r\n开始时间:{1}\r\n结束时间:{2}\r\n总 时 间:{3}ms\r\n";
            return string.Format(Msg,
                Url,
                ExecuteStartTime,
                ExecuteEndTime,
                (ExecuteEndTime - ExecuteStartTime).TotalMilliseconds);
        }

        /// <summary>
        /// 获取Action 参数
        /// </summary>
        /// <param name="Collections"></param>
        /// <returns></returns>
        public string GetCollections(Dictionary<string, object> Collections)
        {
            string Parameters = string.Empty;
            if (Collections == null || Collections.Count == 0)
            {
                return Parameters;
            }
            foreach (string key in Collections.Keys)
            {
                Parameters += string.Format("{0}={1}&", key, Collections[key]);
            }
            if (!string.IsNullOrWhiteSpace(Parameters) && Parameters.EndsWith("&"))
            {
                Parameters = Parameters.Substring(0, Parameters.Length - 1);
            }
            return Parameters;
        }
    }
}
