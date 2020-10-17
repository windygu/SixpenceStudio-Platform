using SixpenceStudio.BaseSite.SysParams;
using SixpenceStudio.BaseSite.WeChat.ResponseModel;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SixpenceStudio.BaseSite.WeChat
{
    public class WeChatController : BaseController
    {
        /// <summary>
        /// 获取微信access_token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetAccessToken()
        {
            return new WeChatService().AccessToken;
        }

        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="echostr"></param>
        [HttpGet]
        public void Get(string signature, string timestamp, string nonce, string echostr)
        {
            var result = new WeChatService().CheckSignature(signature, timestamp, nonce, echostr);
            if (result)
            {
                HttpContext.Current.Response.Write(echostr);
                HttpContext.Current.Response.End();
            }
        }

        /// <summary>
        /// 获取微信素材
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public object GetMaterial(string typeId, int pageIndex, int pageSize)
        {
            var service = new WeChatService();
            var type = service.GetMaterialType(typeId);
            if (type == MaterialType.news.ToMaterialTypeString())
            {
                return new WeChatService().GetWeChatNewsMaterial(type, pageIndex, pageSize);
            }
            return new WeChatService().GetWeChatOtherMaterial(type, pageIndex, pageSize);
        }
    }
}