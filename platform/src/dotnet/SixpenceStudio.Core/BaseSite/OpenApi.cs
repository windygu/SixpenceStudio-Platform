using SixpenceStudio.Core.ShortUrl;
using SixpenceStudio.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.Core
{
    public class OpenApi : BaseController
    {
        /// <summary>
        /// 获取短链接
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>短地址</returns>
        public string GetShortUrl(string id)
        {
            return new ShortUrlService().GetShortUrl(id);
        }

        /// <summary>
        /// 创建短链接
        /// </summary>
        /// <param name="longUrl">长地址</param>
        /// <returns>短地址</returns>
        public string CreateShortUrl(string longUrl)
        {
            return new ShortUrlService().CreateData(longUrl);
        }
    }
}