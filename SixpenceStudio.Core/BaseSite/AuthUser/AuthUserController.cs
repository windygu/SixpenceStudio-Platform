using SixpenceStudio.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SixpenceStudio.Core.AuthUser
{
    public class AuthUserController : EntityBaseController<auth_user, AuthUserService>
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public LoginResponse Login([FromBody]dynamic request)
        {
            string code = request.code;
            string pwd = request.password;
            string publicKey = request.publicKey;
            return new AuthUserService().Login(code, pwd, publicKey);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="password"></param>
        [HttpPost]
        public void EditPassword([FromBody]string password)
        {
            new AuthUserService().EditPassword(password);
        }

        /// <summary>
        /// 锁定用户
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        public void LockUser(string id)
        {
            new AuthUserService().LockUser(id);
        }

        /// <summary>
        /// 解锁用户
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        public void UnlockUser(string id)
        {
            new AuthUserService().UnlockUser(id);
        }
    }
}