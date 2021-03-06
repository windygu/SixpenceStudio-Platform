using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Extensions;
using SixpenceStudio.Core.Store;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace SixpenceStudio.Core.SysFile
{
    public class SystemStore : IStoreStrategy
    {
        /// <summary>
        /// 批量删除文件
        /// </summary>
        /// <param name="fileName"></param>
        public void Delete(IList<string> fileName)
        {
            fileName.ToList().ForEach(item =>
            {
                var filePath = Path.Combine(FolderType.Storage.GetPath(), item);
                FileUtil.DeleteFile(filePath);
            });
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filePath"></param>
        public void DownLoad(string objectId)
        {
            var broker = PersistBrokerFactory.GetPersistBroker();
            var data =  broker.Retrieve<sys_file>(objectId) ?? broker.Retrieve<sys_file>("select * from sys_file where hash_code = @id", new Dictionary<string, object>() { { "@id", objectId } });
            var fileInfo = new FileInfo(Path.Combine(FolderType.Storage.GetPath(), data?.name ?? ""));
            if (fileInfo.Exists)
            {
                HttpContext.Current.Response.BufferOutput = true;
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileInfo.Name);
                HttpContext.Current.Response.TransmitFile(fileInfo.FullName);
                HttpContext.Current.Response.End();
            }
            else
            {
                HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.NotFound;
                HttpContext.Current.Response.End();
            }
        }

        /// <summary>
        /// 获取文件流
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Stream GetStream(string id)
        {
            var broker = PersistBrokerFactory.GetPersistBroker();
            var data = broker.Retrieve<sys_file>(id);
            return FileUtil.GetFileStream(Path.Combine(FolderType.Storage.GetPath(), data.name));
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        public void Upload(Stream stream, string fileName, out string filePath)
        {
            filePath = $"\\storage\\{fileName}"; // 相对路径
            var path = Path.Combine(FolderType.Storage.GetPath(), fileName); // 绝对路径
            FileUtil.SaveFile(stream, path);
        }
    }
}
