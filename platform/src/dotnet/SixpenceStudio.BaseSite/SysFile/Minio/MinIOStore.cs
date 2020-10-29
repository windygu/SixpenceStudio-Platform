using Minio;
using Minio.DataModel;
using Minio.Exceptions;
using SixpenceStudio.Platform.Configs;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Logging;
using SixpenceStudio.Platform.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SixpenceStudio.BaseSite.SysFile.Minio
{
    /// <summary>
    /// MinIO服务类
    /// </summary>
    public class MinIOStore : IStoreStrategy
    {
        private MinioClient minio;
        private string bucketName;
        public MinIOStore()
        {
            var config = ConfigFactory.GetConfig<MinIOSection>();
            minio = new MinioClient(config.endpoint, config.accessKey, config.secretKey)
                .WithTimeout(60000);
            bucketName = "blog";
        }

        /// <summary>
        /// 返回目录列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetBucketsList()
        {
            var getListBucketsTask = minio.ListBucketsAsync().Result.Buckets;
            return getListBucketsTask.Select(item => item.Name);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        public void Upload(Stream stream, string fileName, out string filePath)
        {
            filePath = $"{bucketName}\\{fileName}";
            UploadTask(stream, fileName);
        }

        private async void UploadTask(Stream stream, string fileName)
        {
            try
            {
                bool found = await minio.BucketExistsAsync(bucketName);
                if (!found)
                {
                    await minio.MakeBucketAsync(bucketName);
                }
                await minio.PutObjectAsync(bucketName, fileName, stream, stream.Length);
                LogUtils.Debug($"文件{fileName}上传成功");
            }
            catch (MinioException e)
            {
                LogUtils.Error($"文件{fileName}上传失败", e);
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="objectId"></param>
        public async void DownLoad(string objectId)
        {
            var broker = PersistBrokerFactory.GetPersistBroker();
            var data = broker.Retrieve<sys_file>("select * from sys_file where hash_code = @id", new Dictionary<string, object>() { { "@id", objectId } });
            try
            {
                LogUtils.Debug("开始下载文件");
                await minio.GetObjectAsync(bucketName, data.name,
                (stream) =>
                {
                    var bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, (int)stream.Length);
                    stream.Seek(0, SeekOrigin.Begin);
                    HttpContext.Current.Response.BufferOutput = true;
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + data.name);
                    HttpContext.Current.Response.BinaryWrite(bytes);
                    HttpContext.Current.Response.End();
                });
                LogUtils.Debug($"文件{data.name}下载成功");
            }
            catch (MinioException e)
            {
                LogUtils.Error($"文件{data.name}下载失败", e);
            }
        }

        public void Delete(IList<string> fileName)
        {
            throw new NotImplementedException();
        }
    }
}