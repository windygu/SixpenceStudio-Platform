using SixpenceStudio.BaseSite;
using SixpenceStudio.Platform.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SixpenceStudio.WeChat.Material
{
    [RequestAuthorize]
    public class WeChatMaterialController : BaseController
    {
        /// <summary>
        /// 获取微信素材
        /// </summary>
        /// <param name="code"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public WeChatOtherMaterial GetMaterial(string code, int pageIndex, int pageSize)
        {
            return new WeChatMaterialService().GetMaterial(code, pageIndex, pageSize);
        }
    }
}
