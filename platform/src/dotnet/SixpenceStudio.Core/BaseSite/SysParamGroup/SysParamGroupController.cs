using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SixpenceStudio.Core.SysParamGroup
{
    [RequestAuthorize]
    public class SysParamGroupController : EntityBaseController<sys_paramgroup, SysParamGroupService>
    {
        [HttpGet]
        public IList<SelectModel> GetParams(string code)
        {
            return new SysParamGroupService().GetParams(code);
        }
    }
}