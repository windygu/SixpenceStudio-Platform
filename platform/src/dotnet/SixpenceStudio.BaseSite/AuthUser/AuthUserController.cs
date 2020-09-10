using SixpenceStudio.Platform.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SixpenceStudio.BaseSite.AuthUser
{
    [RequestAuthorize]
    public class AuthUserController : EntityBaseController<auth_user, AuthUserService>
    {
        [HttpPost, AllowAnonymous]
        public LoginResponse Login([FromBody]dynamic request)
        {
            string code = request.code;
            string pwd = request.password;
            string publicKey = request.publicKey;
            return new AuthUserService().Login(code, pwd, publicKey);
        }

        [HttpPost]
        public void EditPassword([FromBody]string password)
        {
            new AuthUserService().EditPassword(password);
        }
    }
}