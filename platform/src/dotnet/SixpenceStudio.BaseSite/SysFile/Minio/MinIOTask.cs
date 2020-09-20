using Minio;
using Minio.Exceptions;
using SixpenceStudio.Platform.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SixpenceStudio.BaseSite.SysFile.Minio
{
    public static class MinIOTask
    {
        /// <summary>
        /// 上传Task
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        internal async static Task Upload(MinioClient minio, string bucketName, string filePath, string objectName)
        {
            var contentType = "application/zip";

            try
            {
                bool found = await minio.BucketExistsAsync(bucketName);
                if (!found)
                {
                    await minio.MakeBucketAsync(bucketName);
                }
                await minio.PutObjectAsync(bucketName, objectName, filePath, contentType);
                LogUtils.DebugLog($"文件{objectName}上传成功");
            }
            catch (MinioException e)
            {
                LogUtils.ErrorLog($"文件{objectName}上传失败", e);
            }
        }

        /// <summary>
        /// 下载文件Task
        /// </summary>
        /// <param name="minio"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <returns></returns>
        internal async static Task Download(MinioClient minio, string bucketName, string objectName)
        {
            try
            {
                LogUtils.DebugLog("开始下载文件");
                await minio.GetObjectAsync(bucketName, objectName,
                (stream) =>
                {
                    // Uncomment to print the file on output console
                    // stream.CopyTo(Console.OpenStandardOutput());
                });
                LogUtils.DebugLog($"文件{objectName}下载成功");
            }
            catch (Exception e)
            {
                LogUtils.ErrorLog($"文件{objectName}下载失败", e);
            }
        }
    }
}