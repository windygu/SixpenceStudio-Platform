﻿using SixpenceStudio.Platform.Service;
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

        public void EditPassword(string password)
        {
            var sql = $@"
UPDATE auth_user
SET password = @password
WHERE userid = @id;
";
            var encryptionPwd = SHAUtils.SHA256Encrypt(password);
            var user = _cmd.GetCurrentUser();
            var paramList = new Dictionary<string, object>() { { "@id",  user.userId}, { "@password", encryptionPwd } };
            _cmd.broker.DbClient.Execute(sql, paramList);
        }


        /// <summary>
        /// 检验 Ticket
        /// </summary>
        /// <param name="encryptTicket"></param>
        /// <returns></returns>
        public bool ValidateTicket(string encryptTicket, out string userId)
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

            userId = userStr;

            var data = GetData(userStr, pwdStr);
            return data != null && !expired;
        }

    }
}