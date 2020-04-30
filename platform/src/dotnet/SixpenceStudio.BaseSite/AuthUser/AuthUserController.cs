using SixpenceStudio.Platform.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SixpenceStudio.BaseSite.AuthUser
{
    public class AuthUserController : EntityController<auth_user, AuthUserService>
    {
        [HttpPost]
        public LoginResponse Login([FromBody]dynamic request)
        {
            string code = request.code;
            string pwd = request.password;
            return new AuthUserService().Login(code, pwd);
        }

        [RequestAuthorize, HttpPost]
        public void EditPassword([FromBody]string password)
        {
            new AuthUserService().EditPassword(password);
        }
    }
}