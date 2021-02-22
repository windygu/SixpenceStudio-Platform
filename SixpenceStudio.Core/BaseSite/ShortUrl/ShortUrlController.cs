using SixpenceStudio.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.ShortUrl
{
    public class ShortUrlController : EntityBaseController<short_url_log, ShortUrlService>
    {

        /// <summary>
        /// 获取短链接
        /// </summary>
        /// <param name="shortid"></param>
        /// <returns></returns>
        public short_url_log GetDataByShortId(string id)
        {
            return new ShortUrlService().GetDataByShortId(id);
        }
    }
}
