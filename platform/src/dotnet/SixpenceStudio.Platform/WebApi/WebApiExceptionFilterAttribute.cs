using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace SixpenceStudio.Platform.WebApi
{
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        // 重写基类的异常处理方法
        public override void OnException(HttpActionExecutedContext context)
        {
            // 1.异常日志记录（正式项目里面一般是用log4net记录异常日志）
            //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "——" +
            //    actionExecutedContext.Exception.GetType().ToString() + "：" + actionExecutedContext.Exception.Message + "——堆栈信息：" +
            //    actionExecutedContext.Exception.StackTrace);

            // 2.返回调用方具体的异常信息
            if (context.Exception is NotImplementedException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            }
            else if (context.Exception is TimeoutException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.RequestTimeout);
            }
            // 这里可以根据项目需要返回到客户端特定的状态码。如果找不到相应的异常，统一返回服务端错误500
            else
            {
                context.Response = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(context.Exception.Message.Replace(Environment.NewLine, string.Empty)),
                    ReasonPhrase = context.Exception.Message.Replace(Environment.NewLine, string.Empty),
                };
            }

            base.OnException(context);
        }
    }
}
