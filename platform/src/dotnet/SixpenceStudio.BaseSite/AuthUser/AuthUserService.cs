using SixpenceStudio.Platform.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SixpenceStudio.Platform.Command;
using SixpenceStudio.Platform.Data;
using System.Web.Security;
using SixpenceStudio.Platform.Utils;
using System.Web.Services;

namespace SixpenceStudio.BaseSite.AuthUser
{
    public class AuthUserService : EntityService<auth_user>
    {
        #region 构造函数
        public AuthUserService()
        {
            _cmd = new EntityCommand<auth_user>();
        }

        public AuthUserService(IPersistBroker broker)
        {
            _cmd = new EntityCommand<auth_user>(broker);
        }
        #endregion

        public auth_user GetData(string  code, string pwd)
        {
            var sql = @"
SELECT * FROM auth_user WHERE code = @code AND password = @password;
";
            var encryptionPwd = SHAUtils.SHA256Encrypt(pwd);
            var paramList = new Dictionary<string, object>() { { "@code", code }, { "@password", encryptionPwd } };
            var authUser = _cmd.broker.Retrieve<auth_user>(sql, paramList);
            return authUser;
        }

        public LoginResponse Login(string code, string pwd)
        {
            var authUser = GetData(code, pwd);
            if (authUser == null)
            {
                return new LoginResponse() { result = false };
            }
            // 定义票据信息
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(0, code, DateTime.Now,
                            DateTime.Now.AddHours(12), true, string.Format("{0}&{1}", code, pwd),
                            FormsAuthentication.FormsCookiePath);

            #region 保存用户登录 Cookie
            var cookie = new HttpCookie("LoginUser");
            cookie.Values.Add("UserId", code);
            cookie.Expires = DateTime.Now.AddDays(1);
            cookie.Path = "/";
            HttpContext.Current.Response.Cookies.Add(cookie);
            #endregion

            // 返回登录结果、用户信息、用户验证票据信息
            var oUser = new LoginResponse { result = true, UserName = code, Ticket = FormsAuthentication.Encrypt(ticket) };
            return oUser;
        }
    }
}