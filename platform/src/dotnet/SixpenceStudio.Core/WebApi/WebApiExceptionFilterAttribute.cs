using SixpenceStudio.Core.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace SixpenceStudio.Core.WebApi
{
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        // 重写基类的异常处理方法
        public override void OnException(HttpActionExecutedContext context)
        {
            // 1.异常日志记录
            LogUtils.Error(context.Exception.Message, context.Exception);
            // 2.返回调用方具体的异常信息
            if (context.Exception is NotImplementedException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            }
            else if (context.Exception is TimeoutException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.RequestTimeout);
            }
            else if (context.Exception is FileNotFoundException)
            {
                context.Response = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent("访问资源未找到"),
                    ReasonPhrase = "访问资源未找到"
                };
            }
            // 这里可以根据项目需要返回到客户端特定的状态码。如果找不到相应的异常，统一返回服务端错误500
            else
            {
                // 统一处理报错信息
                if (context.Exception as SpException == null)
                {
                    context.Response = new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Content = new StringContent("系统异常，请联系管理员"),
                        ReasonPhrase = "系统异常，请联系管理员"
                    };
                }
                else
                {
                    context.Response = new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Content = new StringContent(context.Exception.Message.Replace(Environment.NewLine, string.Empty)),
                        ReasonPhrase = context.Exception.Message.Replace(Environment.NewLine, string.Empty)
                    };
                }
            }

            base.OnException(context);
        }
    }
}
