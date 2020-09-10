using SixpenceStudio.BaseSite.DataService.Models;
using SixpenceStudio.BaseSite.SysFile;
using SixpenceStudio.Platform.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SixpenceStudio.Platform.Utils;
using SixpenceStudio.BaseSite.AuthUser;

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
            return RSAUtils.GetKey();
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public ImageInfo UploadImage(HttpPostedFile image, string fileType, string objectId)
        {
            // 获取文件哈希码，将哈希码作为文件名
            var hash_code = SHAUtils.GetFileSHA1(image.InputStream);
            var id = Guid.NewGuid().ToString();
            var fileName = $"{hash_code}.{image.FileName.GetFileType()}";
            var filePath = FileUtils.GetLocalStorage() + "\\" + fileName;

            // 保存图片到本地
            // TODO：执行失败回滚操作
            FileUtils.SaveFile(image, filePath);

            var sysImage = new sys_file()
            {
                sys_fileId = id,
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
                path = $"{FileUtils.FILE_FOLDER}/{sysImage.name}"
            };
        }

        /// <summary>
        /// 获取随机图片
        /// </summary>
        /// <returns></returns>
        public string GetRandomImage()
        {
            var result = HttpUtils.Get("https://api.ixiaowai.cn/api/api.php?return=json");
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