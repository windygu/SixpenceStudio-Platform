using SixpenceStudio.Platform.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.BaseSite.SysParams
{
    [RequestAuthorize]
    public class SysParamController : EntityBaseController<sys_param, SysParamService>
    {
    }
}