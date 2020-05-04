using SixpenceStudio.BaseSite.DataService.Models;
using SixpenceStudio.BaseSite.SysFile;
using SixpenceStudio.Platform.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SixpenceStudio.Platform.Utils;

namespace SixpenceStudio.BaseSite.DataService
{
    public class DataService
    {
        public string UploadImage(HttpPostedFile image)
        {
            var id = Guid.NewGuid().ToString();
            var sysImage = new sys_file()
            {
                sys_fileId = id,
                name = $"{id}.{image.FileName.GetFileType()}",
            };
            sysImage.file_path = FileUtils.GetLocalStorage() + "\\" + sysImage.name;
            sysImage.hash_code = SHAUtils.GetFileSHA1(image.InputStream);

            #region 根据HASH值判断是否需要保存文件

            #endregion
            var _file = new SysFileService().GetDattaByCode(sysImage.hash_code);
            if (_file != null)
            {
                sysImage.name = _file.name;
                sysImage.file_path = _file.file_path;
            }
            else
            {
                image.SaveAs(sysImage.file_path);
            }

            var result = new SysFileService().CreateData(sysImage);
            
            return result;
        }


    }
}