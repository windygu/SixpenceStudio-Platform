using SixpenceStudio.Core.AuthUser;
using SixpenceStudio.Core.DataService.Models;
using SixpenceStudio.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SixpenceStudio.Core.DataService
{
    [Route("api/[controller]/[action]")]
    [WebApiExceptionFilter]
    public class DataServiceController : ApiController
    {
        /// <summary>
        /// 获取公钥
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public string GetPublicKey()
        {
            return new DataService().GetPublicKey();
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="fileType">文件类型</param>
        /// <param name="objectId">对象实体</param>
        /// <returns></returns>
        [HttpPost, RequestAuthorize]
        public ImageInfo UploadImage([FromUri]string fileType, [FromUri]string objectId = "")
        {
            HttpPostedFile file = HttpContext.Current.Request.Files[0];
            return new DataService().UploadImage(file, fileType, objectId, file.ContentType);
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public bool Test()
        {
            return new DataService().Test();
        }

        /// <summary>
        /// 获取随机图片
        /// </summary>
        /// <returns>图片源</returns>
        [HttpGet, AllowAnonymous]
        public string GetRandomImage()
        {
            return new DataService().GetRandomImage();
        }
    }
}