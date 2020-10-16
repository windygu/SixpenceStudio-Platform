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
            return new WeChatService().GetAccessToken();
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
    }
}