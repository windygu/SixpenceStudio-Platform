﻿using SixpenceStudio.Platform.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SixpenceStudio.BaseSite.UserInfo
{
    [RequestAuthorize]
    public class UserInfoController : EntityController<user_info, UserInfoService>
    {
        [HttpGet, AllowAnonymous]
        public override user_info GetData(string id)
        {
            return base.GetData(id);
        }
    }
}
