using Newtonsoft.Json;
using SixpenceStudio.Core.Logging;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
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

        #region 获取图片资源
        public static readonly string GetImagesApi = "https://pixabay.com/api/?key={0}&q={1}&image_type=photo";
        public static ImagesModel GetImages(string searchValue)
        {
            if (string.IsNullOrEmpty(searchValue))
            {
                return null;
            }

            var url = string.Format(GetImagesApi, key, searchValue);
            try
            {
                logger.Debug($"Get：{url}");
                var result = HttpUtil.Get(url);
                var resultJson = JsonConvert.DeserializeObject<ImagesModel>(result);
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
            var url = string.Format(GetVideosApi, key, searchValue);
            try
            {
                logger.Debug($"Get：{url}");
                var result = HttpUtil.Get(url);
                var resultJson = JsonConvert.DeserializeObject<VideosModel>(result);
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
