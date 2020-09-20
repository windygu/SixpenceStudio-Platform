using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Minio;
using Minio.DataModel;
using Minio.Exceptions;
using SixpenceStudio.Platform.Configs;
using SixpenceStudio.Platform.Logging;

namespace SixpenceStudio.BaseSite.Storage
{
    public static class MinIOService
    {
        private static MinioClient minio;
        static MinIOService()
        {
            var config = ConfigFactory.GetConfig<MinIOSection>();
            minio = new MinioClient(config.endpoint, config.accessKey, config.secretKey).WithSSL();
        }


        /// <summary>
        /// 返回目录列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetBucketsList()
        {
            var getListBucketsTask = minio.ListBucketsAsync();
            var list = new List<string>();
            foreach (Bucket bucket in getListBucketsTask.Result.Buckets)
            {
                list.Add(bucket.Name + " " + bucket.CreationDateDateTime);
            }
            return list;
        }
        
        /// <summary>
        /// 上传文件
        /// </summary>
        public static void UploadFile(string filePath, string fileName)
        {
            Run(filePath, fileName).Wait();
        }

        // File uploader task.
        private async static Task Run(string filePath, string fileName)
        {
            var bucketName = "blog";
            var contentType = "application/zip";

            try
            {
                // Make a bucket on the server, if not already present.
                bool found = await minio.BucketExistsAsync(bucketName);
                if (!found)
                {
                    await minio.MakeBucketAsync(bucketName);
                }
                await minio.PutObjectAsync(bucketName, fileName, filePath, contentType);
                LogUtils.DebugLog("Successfully uploaded " + fileName);
            }
            catch (MinioException e)
            {
                LogUtils.ErrorLog(string.Format("File Upload Error: {0}", e.Message));
            }
        }
    }
}