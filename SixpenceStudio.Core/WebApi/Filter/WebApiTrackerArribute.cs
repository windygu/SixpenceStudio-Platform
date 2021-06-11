using SixpenceStudio.Core.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SixpenceStudio.Core.WebApi.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class WebApiTrackerAttribute : ActionFilterAttribute
    {
        private readonly string Key = "_thisWebApiOnActionMonitorLog_";
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
            WebApiMonitorLog MonLog = new WebApiMonitorLog(actionContext);
            MonLog.ExecuteStartTime = DateTime.Now;
            //获取Action 参数
            MonLog.ActionParams = actionContext.ActionArguments;
            MonLog.HttpRequestHeaders = actionContext.Request.Headers.ToString();

            actionContext.Request.Properties[Key] = MonLog;
            #region 如果参数是实体对象，获取序列化后的数据
            Stream stream = actionContext.Request.Content.ReadAsStreamAsync().Result;
            Encoding encoding = Encoding.UTF8;
            stream.Position = 0;
            string responseData = "";
            using (StreamReader reader = new StreamReader(stream, encoding))
            {
                responseData = reader.ReadToEnd().ToString();
            }
            if (!string.IsNullOrWhiteSpace(responseData) && !MonLog.ActionParams.ContainsKey("__EntityParamsList__"))
            {
                MonLog.ActionParams["__EntityParamsList__"] = responseData;
            }
            #endregion
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var logger = LogFactory.GetLogger("webapi");
            WebApiMonitorLog MonLog = actionExecutedContext.Request.Properties[Key] as WebApiMonitorLog;
            MonLog.ExecuteEndTime = DateTime.Now;
            MonLog.ActionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
            MonLog.ControllerName = actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            if (actionExecutedContext.Exception != null)
            {
                string Msg = string.Format(@"
                请求【{0}Controller】的【{1}】产生异常：
                Action参数：{2}
                    ", MonLog.ControllerName, MonLog.ActionName, MonLog.GetCollections(MonLog.ActionParams));
                LogUtils.Error(Msg, actionExecutedContext.Exception);
                logger.Error(Msg, actionExecutedContext.Exception);
            }
            else
            {
                LogUtils.Debug(MonLog.GetLoginfo());
                logger.Debug(MonLog.GetLoginfo());
            }
        }
    }
}
