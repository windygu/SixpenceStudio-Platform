using SixpenceStudio.Core.Auth;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;

namespace SixpenceStudio.Core.AuthUser
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

        /// <summary>
        /// 获取用户登录信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="pwd">MD5密码</param>
        /// <returns></returns>
        public auth_user GetData(string code, string pwd)
        {
            var sql = @"
SELECT * FROM auth_user WHERE code = @code AND password = @password;
";
            var paramList = new Dictionary<string, object>() { { "@code", code }, { "@password", pwd } };
            var authUser = _cmd.Broker.Retrieve<auth_user>(sql, paramList);
            return authUser;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="code"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public LoginResponse Login(string code, string pwd, string publicKey)
        {
            var authUser = _cmd.Broker.Retrieve<auth_user>("SELECT * FROM auth_user WHERE code = @code", new Dictionary<string, object>() { { "@code", code } });

            if (authUser == null ||
                string.IsNullOrEmpty(pwd) ||
                string.IsNullOrEmpty(publicKey) ||
                !string.Equals(authUser.password, RSAUtil.Decrypt(pwd, publicKey))
                )
            {
                return new LoginResponse() { result = false, Message = "用户名或密码错误" };
            }

            if (authUser.is_lock)
            {
                return new LoginResponse() { result = false, Message = "用户已被锁定，请联系管理员" };
            }

            // 定义票据信息
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(0, code, DateTime.Now,
                            DateTime.Now.AddHours(12), true, string.Format("{0}&{1}", code, authUser.password),
                            FormsAuthentication.FormsCookiePath);

            #region 保存用户登录 Cookie
            var cookie = new HttpCookie("LoginUser");
            cookie.Values.Add("UserId", code);
            cookie.Expires = DateTime.Now.AddDays(1);
            cookie.Path = "/";
            HttpContext.Current.Response.Cookies.Add(cookie);
            #endregion

            // 返回登录结果、用户信息、用户验证票据信息
            var oUser = new LoginResponse
            {
                result = true,
                UserName = code,
                Ticket = FormsAuthentication.Encrypt(ticket),
                UserId = authUser.user_infoid,
                Message = "登录成功"
            };
            return oUser;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="password"></param>
        public void EditPassword(string password)
        {
            var sql = $@"
UPDATE auth_user
SET password = @password
WHERE user_infoid = @id;
";
            var user = UserIdentityUtil.GetCurrentUser();
            var paramList = new Dictionary<string, object>() { { "@id",  user.Id}, { "@password", password } };
            _cmd.Broker.Execute(sql, paramList);
        }


        /// <summary>
        /// 检验 Ticket
        /// </summary>
        /// <param name="encryptTicket"></param>
        /// <returns></returns>
        public int ValidateTicket(string encryptTicket, out string userId)
        {
            // 解密Ticket
            var strTicket = FormsAuthentication.Decrypt(encryptTicket);

            var user = strTicket.UserData;
            var expireation = strTicket.Expiration;
            var expired = strTicket.Expired;
            var issueDate = strTicket.IssueDate;

            // 从Ticket里面获取用户名和密码
            var index = user.IndexOf("&");
            string userStr = user.Substring(0, index);
            string pwdStr = user.Substring(index + 1);

            var data = GetData(userStr, pwdStr);
            userId = data.Id;
            if (expired)
            {
                return 403;
            }
            else if (data == null)
            {
                return 401;
            }
            else
            {
                return 200;
            }
        }

        public auth_user GetDataByCode(string code)
        {
            var data = _cmd.Broker.Retrieve<auth_user>("select * from auth_user where code = @code", new Dictionary<string, object>(){ { "@code", code } });
            return data;
        }

        public auth_user GetDataById(string id)
        {
            return Broker.Retrieve<auth_user>(id);
        }

        /// <summary>
        /// 锁定用户
        /// </summary>
        /// <param name="id"></param>
        public void LockUser(string id)
        {
            Broker.ExecuteTransaction(() =>
            {
                var userId = UserIdentityUtil.GetCurrentUserId();
                AssertUtil.CheckBoolean<SpException>(userId == id, "请勿锁定自己", "4B1DD6F4-977B-43B4-BA48-C02668A661B3");
                var data = Broker.Retrieve<auth_user>("select * from auth_user where user_infoid = @id", new Dictionary<string, object>() { { "@id", id } });
                data.is_lock = true;
                UpdateData(data);
            });
        }

        /// <summary>
        /// 解锁用户
        /// </summary>
        /// <param name="id"></param>
        public void UnlockUser(string id)
        {
            Broker.ExecuteTransaction(() =>
            {
                var data = Broker.Retrieve<auth_user>("select * from auth_user where user_infoid = @id", new Dictionary<string, object>() { { "@id", id } });
                data.is_lock = false;
                UpdateData(data);
            });
        }
    }
}