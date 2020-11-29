using SixpenceStudio.Core.AuthUser;
using SixpenceStudio.Core.DataService.Models;
using SixpenceStudio.Core.Store;
using SixpenceStudio.Core.SysFile;
using SixpenceStudio.Core.Configs;
using SixpenceStudio.Core.Utils;
using System;
using System.Web;
using System.Web.Http;
using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.Logging;

namespace SixpenceStudio.Core.DataService
{
    public class DataService
    {
        /// <summary>
        /// 获取公钥
        /// </summary>
        /// <returns></returns>
        public string GetPublicKey()
        {
            return RSAUtil.GetKey();
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [HttpPost, RequestAuthorize]
        public ImageInfo UploadImage(HttpPostedFile image, string fileType, string objectId, string contentType)
        {
            // 获取文件哈希码，将哈希码作为文件名
            var hash_code = SHAUtil.GetFileSHA1(image.InputStream);
            var fileName = $"{hash_code}.{image.FileName.GetFileType()}";

            // 保存图片到本地
            // TODO：执行失败回滚操作
            var config = ConfigFactory.GetConfig<StoreSection>();
            UnityContainerService.Resolve<IStoreStrategy>(config?.type).Upload(image.InputStream, fileName, out var filePath);
            var sysImage = new sys_file()
            {
                sys_fileId = Guid.NewGuid().ToString(),
                name = fileName,
                hash_code = hash_code,
                file_path = filePath,
                file_type = fileType,
                content_type = contentType
            };
            if (!string.IsNullOrEmpty(objectId))
            {
                sysImage.objectId = objectId;
            }

            new SysFileService().CreateData(sysImage);

            return new ImageInfo()
            {
                id = sysImage.sys_fileId,
                name = sysImage.name,
                downloadUrl = $"api/SysFile/Download?objectId={sysImage.sys_fileId}"
            };
        }

        /// <summary>
        /// 获取随机图片
        /// </summary>
        /// <returns></returns>
        public string GetRandomImage()
        {
            var result = HttpUtil.Get("https://api.ixiaowai.cn/api/api.php?return=json");
            return result;
        }

        /// <summary>
        /// 测试是否是合法用户
        /// </summary>
        /// <returns></returns>
        public bool Test()
        {
            var authorization = HttpContext.Current.Request.Headers["Authorization"];
            if (authorization != null)
            {
                authorization = authorization.Replace("BasicAuth ", "");
                try
                {
                    return new AuthUserService().ValidateTicket(authorization, out var userId) == 200;
                }
                catch (Exception ex)
                {
                    LogUtils.Error("验证身份失败", ex);
                    return false;
                }
            }
            return false;
        }
    }
}