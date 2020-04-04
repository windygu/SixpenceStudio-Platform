using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.Http.Controllers;
using SixpenceStudio.BaseSite.UserInfo;
using SixpenceStudio.BaseSite.AuthUser;

namespace SixpenceStudio.BaseSite
{
    public class RequestAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //从http请求的头里面获取身份验证信息，验证是否是请求发起方的ticket
            var authorization = actionContext.Request.Headers.Authorization;
            //请求头Authorization不为空且验证里的值不为空
            if ((authorization != null) && (authorization.Parameter != null))
            {
                //解密用户ticket,并校验用户名密码是否匹配
                var encryptTicket = authorization.Parameter;
                //验证是否正确用户名密码
                if (ValidateTicket(encryptTicket))
                {
                    //指定已授权
                    base.IsAuthorized(actionContext);
                }
                else
                {
                    //返回401
                    HandleUnauthorizedRequest(actionContext);
                }
            }
            //如果取不到身份验证信息，并且不允许匿名访问，则返回未验证401
            else
            {
                var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();
                bool isAnonymous = attributes.Any(a => a is AllowAnonymousAttribute);
                if (isAnonymous) base.OnAuthorization(actionContext);
                else HandleUnauthorizedRequest(actionContext);
            }
        }

        /// <summary>
        /// 检验 Ticket
        /// </summary>
        /// <param name="encryptTicket"></param>
        /// <returns></returns>
        private bool ValidateTicket(string encryptTicket)
        {
            //解密Ticket
            var strTicket = FormsAuthentication.Decrypt(encryptTicket);

            var user = strTicket.UserData;
            var expireation = strTicket.Expiration;
            var expired = strTicket.Expired;
            var issueDate = strTicket.IssueDate;

            // 从Ticket里面获取用户名和密码
            var index = user.IndexOf("&");
            string userStr = user.Substring(0, index);
            string pwdStr = user.Substring(index + 1);

            var data = new AuthUserService().GetData(userStr, pwdStr);
            return data != null && !expired;
        }

    }
}