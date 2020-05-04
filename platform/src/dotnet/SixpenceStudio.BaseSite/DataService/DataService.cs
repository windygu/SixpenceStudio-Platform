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
            var result = new SysFileService().CreateData(sysImage);
            image.SaveAs(sysImage.file_path);
            return result;
        }
    }
}