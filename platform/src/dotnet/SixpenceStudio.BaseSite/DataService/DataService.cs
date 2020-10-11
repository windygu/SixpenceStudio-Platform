using SixpenceStudio.BaseSite.AuthUser;
using SixpenceStudio.BaseSite.DataService.Models;
using SixpenceStudio.Platform.Store;
using SixpenceStudio.BaseSite.SysFile;
using SixpenceStudio.Platform.Configs;
using SixpenceStudio.Platform.Utils;
using System;
using System.Web;

namespace SixpenceStudio.BaseSite.DataService
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
        public ImageInfo UploadImage(HttpPostedFile image, string fileType, string objectId)
        {
            // 获取文件哈希码，将哈希码作为文件名
            var hash_code = SHAUtil.GetFileSHA1(image.InputStream);
            var fileName = $"{hash_code}.{image.FileName.GetFileType()}";

            // 保存图片到本地
            // TODO：执行失败回滚操作
            var config = ConfigFactory.GetConfig<StoreSection>();
            AssemblyUtil.GetObject<IStoreStrategy>(config?.type).Upload(image.InputStream, fileName, out var filePath);

            var sysImage = new sys_file()
            {
                sys_fileId = Guid.NewGuid().ToString(),
                name = fileName,
                hash_code = hash_code,
                file_path = filePath,
                file_type = fileType,
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
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }
    }
}