using SixpenceStudio.Core.SysEntity.SysAttrs;
using SixpenceStudio.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SixpenceStudio.Core.SysEntity
{
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

        /// <summary>
        /// 导出实体类
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public void Export(string entityId)
        {
            var fileInfo = new SysEntityService().Export(entityId);
            HttpContext.Current.Response.BufferOutput = true;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileInfo.Name);
            HttpContext.Current.Response.TransmitFile(fileInfo.FullName);
            HttpContext.Current.Response.End();
        }
    }
}