using Minio;
using Minio.DataModel;
using SixpenceStudio.Platform.Configs;
using System.Collections.Generic;
using System.Linq;

namespace SixpenceStudio.BaseSite.SysFile.Minio
{
    /// <summary>
    /// MinIO服务类
    /// </summary>
    public class MinIOService
    {
        private MinioClient minio;
        public MinIOService()
        {
            var config = ConfigFactory.GetConfig<MinIOSection>();
            minio = new MinioClient(config.endpoint, config.accessKey, config.secretKey)
                .WithTimeout(60000);
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
        public void Upload(string filePath, string fileName)
        {
            MinIOTask.Upload(minio, "blog", filePath, fileName).Wait();
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="objectName"></param>
        public void Download(string objectName)
        {
            MinIOTask.Download(minio, "blog", objectName).Wait();
        }
    }
}