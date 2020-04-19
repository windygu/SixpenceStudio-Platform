using SixpenceStudio.Platform.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.BaseSite.SysEntity.SysAttrs
{
    [RequestAuthorize]
    public class SysAttrsController : EntityController<sys_attrs, SysAttrsService>
    {

    }
}