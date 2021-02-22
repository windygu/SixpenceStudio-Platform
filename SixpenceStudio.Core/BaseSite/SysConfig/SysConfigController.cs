using SixpenceStudio.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SixpenceStudio.Core.SysConfig
{
    public class SysConfigController : EntityBaseController<sys_config, SysConfigService>
    {
        [HttpGet, AllowAnonymous]
        public object GetValue(string code)
        {
            return new SysConfigService().GetValue(code);
        }
    }
}