using SixpenceStudio.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SixpenceStudio.Core.SysEntity.SysAttrs
{
    [RequestAuthorize]
    public class SysAttrsController : EntityBaseController<sys_attrs, SysAttrsService>
    {
        /// <summary>
        /// 实体添加系统字段
        /// </summary>
        [HttpPost]
        public void AddSystemAttrs([FromBody]string id)
        {
            new SysAttrsService().AddSystemAttrs(id);
        }
    }
}