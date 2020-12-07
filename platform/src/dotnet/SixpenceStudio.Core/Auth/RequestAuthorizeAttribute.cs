using SixpenceStudio.Core.Auth;
using SixpenceStudio.Core.AuthUser;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace SixpenceStudio.Core.Auth
{
    public class RequestAuthorizeAttribute : AuthorizeAttribute
    {
        private int status { get; set; } // 登录状态
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();
            bool isAnonymous = attributes.Any(a => a is AllowAnonymousAttribute);
            if (isAnonymous)
            {
                base.OnAuthorization(actionContext);
                return;
            }

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
                        ApplicationContext.Current.User = new AuthUserService().GetData(userId).ToCurrentUserModel();
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
            else
            {
                HandleUnauthorizedRequest(actionContext);
            }
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);
            var response = actionContext.Response ?? new HttpResponseMessage();
            switch (status)
            {
                case 403:
                    response.StatusCode = System.Net.HttpStatusCode.Forbidden;
                    response.ReasonPhrase = "Token过期";
                    break;
                default:
                    response.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                    break;
            }
        }
    }
}