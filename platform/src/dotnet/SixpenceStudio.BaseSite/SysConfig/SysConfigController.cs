using SixpenceStudio.Platform.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SixpenceStudio.BaseSite.SysConfig
{
    [RequestAuthorize]
    public class SysConfigController : EntityBaseController<sys_config, SysConfigService>
    {
        [HttpGet, AllowAnonymous]
        public object GetValue(string code)
        {
            return new SysConfigService().GetValue(code);
        }
    }
}