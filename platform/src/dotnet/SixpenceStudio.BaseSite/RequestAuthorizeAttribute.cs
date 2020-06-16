using SixpenceStudio.BaseSite.AuthUser;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace SixpenceStudio.BaseSite
{
    public class RequestAuthorizeAttribute : AuthorizeAttribute
    {
        private int status { get; set; } // 登录状态
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // 从http请求的头里面获取身份验证信息，验证是否是请求发起方的ticket
            var authorization = actionContext.Request.Headers.Authorization;
            // 请求头Authorization不为空且验证里的值不为空
            if ((authorization != null) && (authorization.Parameter != null))
            {
                // 解密用户ticket,并校验用户名密码是否匹配
                var encryptTicket = authorization.Parameter;
                // 验证是否正确用户名密码
                try
                {
                    status = new AuthUserService().ValidateTicket(encryptTicket, out var userId);
                    if (status == 200)
                    {
                        base.IsAuthorized(actionContext);
                        HttpContext.Current.Session["UserId"] = userId;
                    }
                    else
                    {
                        HandleUnauthorizedRequest(actionContext);
                    }
                }
                catch
                {
                    HandleUnauthorizedRequest(actionContext);
                }
            }
            // 如果取不到身份验证信息，并且不允许匿名访问，则返回未验证401
            else
            {
                var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();
                bool isAnonymous = attributes.Any(a => a is AllowAnonymousAttribute);
                if (isAnonymous) base.OnAuthorization(actionContext);
                else HandleUnauthorizedRequest(actionContext);
            }
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);
            var response = actionContext.Response ?? new HttpResponseMessage();
            switch (status)
            {
                case 403:
                    actionContext.Response.StatusCode = System.Net.HttpStatusCode.Forbidden;
                    actionContext.Response.ReasonPhrase = "Token过期";
                    break;
                default:
                    actionContext.Response.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                    break;
            }
        }
    }
}