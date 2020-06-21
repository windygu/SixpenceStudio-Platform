using SixpenceStudio.BaseSite.SysEntity.SysAttrs;
using SixpenceStudio.Platform.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SixpenceStudio.BaseSite.SysEntity
{
    [RequestAuthorize]
    public class SysEntityController : EntityBaseController<sys_entity, SysEntityService>
    {
        /// <summary>
        /// 根据实体 id 查询字段
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<sys_attrs> GetEntityAttrs(string id)
        {
            return new SysEntityService().GetEntityAttrs(id);
        }
    }
}