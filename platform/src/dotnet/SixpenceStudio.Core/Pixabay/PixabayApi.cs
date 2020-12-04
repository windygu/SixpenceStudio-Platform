﻿using Brotli;
using Newtonsoft.Json;
using SixpenceStudio.Core.Logging;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Pixabay
{
    /// <summary>
    /// Pixabay第三方API资源（https://pixabay.com/api/docs/）
    /// </summary>
    public class PixabayApi
    {
        private static readonly string key = "19356383-2f75a9b525aa933f63ab20ab5";
        private static Logger logger = LogFactory.GetLogger("PixabayApi");
        private static string Get(string url)
        {
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "Get";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.66 Safari/537.36";
            request.ContentType = "application/json";
            var headerList = new Dictionary<string, string>() {
                { "Accept-Encoding", "gzip, deflate, br" },
                { "Accept-Language", "en-US,en;q=0.5" },
                { "Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9" }
            };
            if (headerList != null)
            {
                foreach (var header in headerList)
                {
                    if (header.Key.Equals("Accept", StringComparison.OrdinalIgnoreCase))
                    {
                        request.Accept = header.Value;
                    }
                    else
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }
            }
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();

            if (responseStream == null) return string.Empty;

            var stream = new BrotliStream(responseStream, CompressionMode.Decompress);
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        #region 获取图片资源
        public static readonly string GetImagesApi = "https://pixabay.com/api/?key={0}&q={1}&image_type=photo";
        public static ImagesModel GetImages(string searchValue)
        {
            if (string.IsNullOrEmpty(searchValue))
            {
                return null;
            }
            var cache = MemoryCacheUtil.GetCacheItem<ImagesModel>("PixabayApi_Images_" + searchValue);
            if (cache != null) return cache;

            var url = string.Format(GetImagesApi, key, searchValue);
            try
            {
                logger.Debug($"Get：{url}");
                var result = Get(url);
                var resultJson = JsonConvert.DeserializeObject<ImagesModel>(result);
                MemoryCacheUtil.Set("PixabayApi_Images_" + searchValue, resultJson, 3600 * 24);
                return resultJson;
            }
            catch (Exception ex)
            {
                logger.Error($"获取图片资源：{searchValue} 失败", ex);
                throw ex;
            }
        }
        #endregion

        #region 获取视频资源 
        public static readonly string GetVideosApi = "https://pixabay.com/api/videos/?key={0}&q={1}";
        public static VideosModel GetVideos(string searchValue)
        {
            if (string.IsNullOrEmpty(searchValue))
            {
                return null;
            }
            var cache = MemoryCacheUtil.GetCacheItem<VideosModel>("PixabayApi_videos_" + searchValue);
            if (cache != null) return cache;

            var url = string.Format(GetVideosApi, key, searchValue);
            try
            {
                logger.Debug($"Get：{url}");
                var result = Get(url);
                var resultJson = JsonConvert.DeserializeObject<VideosModel>(result);
                MemoryCacheUtil.Set("PixabayApi_videos_" + searchValue, resultJson, 3600 * 24);
                return resultJson;
            }
            catch (Exception ex)
            {
                logger.Error($"获取视频资源：{searchValue} 失败", ex);
                throw ex;
            }
        }
        #endregion

    }
}
