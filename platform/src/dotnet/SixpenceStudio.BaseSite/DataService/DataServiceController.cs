using SixpenceStudio.BaseSite.DataService.Models;
using SixpenceStudio.Platform.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SixpenceStudio.BaseSite.DataService
{
    [Route("api/[controller]/[action]")]
    [WebApiExceptionFilter, RequestAuthorize]
    public class DataServiceController : ApiController
    {
        [HttpPost]
        public ImageInfo UploadImage([FromUri]string fileType, [FromUri]string objectId = "")
        {
            HttpPostedFile file = HttpContext.Current.Request.Files[0];
            return new DataService().UploadImage(file, fileType, objectId);
        }

        [HttpGet]
        public string Test()
        {
            return "Your authorization is right！";
        }
    }
}