using SixpenceStudio.Platform.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SixpenceStudio.BaseSite.SysFile
{
    [RequestAuthorize]
    public class SysFileController : EntityBaseController<sys_file, SysFileService>
    {
        [HttpGet, AllowAnonymous]
        public void Download(string objectId)
        {
            var fileInfo = new SysFileService().GetFile(objectId);
            if (fileInfo != null)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileInfo.Name);
                HttpContext.Current.Response.TransmitFile(fileInfo.FullName);
                HttpContext.Current.Response.End();
            }
        }

    }
}