using Newtonsoft.Json;
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

        #region 获取图片资源
        public static readonly string GetImagesApi = "https://pixabay.com/api/?key={0}&q={1}&image_type=photo";
        public static ImagesModel GetImages(string searchValue)
        {
            if (string.IsNullOrEmpty(searchValue))
            {
                return null;
            }

            var result = HttpUtil.Get(string.Format(GetImagesApi, key, searchValue));
            var resultJson = JsonConvert.DeserializeObject<ImagesModel>(result);
            return resultJson;
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

            var result = HttpUtil.Get(string.Format(GetVideosApi, key, searchValue));
            var resultJson = JsonConvert.DeserializeObject<VideosModel>(result);
            return resultJson;
        }
        #endregion

    }
}
