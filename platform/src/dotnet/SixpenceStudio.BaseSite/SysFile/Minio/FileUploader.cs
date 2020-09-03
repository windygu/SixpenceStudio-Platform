using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using Minio;
using Minio.Exceptions;
using SixpenceStudio.Platform.Logging;

namespace SixpenceStudio.BaseSite.SysFile.Minio
{
    public class FileUploader
    {
        private readonly static string endpoint = "http://www.dumiaoxin.top:9000";
        private readonly static string accessKey = "admin";
        private readonly static string secretKey = "p@ssw0rd";

        public async static void Upload(string fileName, string filePath, string contentType)
        {
            var minio = new MinioClient(endpoint, accessKey, secretKey);

            var bucketName = "blog";
            var location = "us-east-1";

            try
            {
                bool found = await minio.BucketExistsAsync(bucketName);
                if (!found)
                {
                    await minio.MakeBucketAsync(bucketName, location);
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