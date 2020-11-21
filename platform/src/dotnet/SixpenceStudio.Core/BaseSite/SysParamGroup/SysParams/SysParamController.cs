using SixpenceStudio.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.Core.SysParams
{
    [RequestAuthorize]
    public class SysParamController : EntityBaseController<sys_param, SysParamService>
    {
    }
}