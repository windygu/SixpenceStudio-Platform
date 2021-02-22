using SixpenceStudio.Core.AuthUser;
using SixpenceStudio.Core.SysFile.Minio;
using SixpenceStudio.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SixpenceStudio.Core.SysFile
{
    public class SysFileController : EntityBaseController<sys_file, SysFileService>
    {
        [HttpGet, AllowAnonymous]
        public void Download(string objectId)
        {
            new SysFileService().Download(objectId);
        }

        [HttpGet]
        public IEnumerable<string> GetBucketsList()
        {
            return new MinIOStore().GetBucketsList();
        }
    }
}