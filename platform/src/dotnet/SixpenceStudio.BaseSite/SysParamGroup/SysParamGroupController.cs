using SixpenceStudio.Platform.Entity;
using SixpenceStudio.Platform.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SixpenceStudio.BaseSite.SysParamGroup
{
    [RequestAuthorize]
    public class SysParamGroupController : EntityController<sys_paramgroup, SysParamGroupService>
    {
        [HttpGet]
        public IList<SelectModel> GetParams(string code)
        {
            return new SysParamGroupService().GetParams(code);
        }
    }
}