using SixpenceStudio.BaseSite;
using SixpenceStudio.Platform.WebApi;
using System;
using System.Text;
using System.Web;
using System.Web.Http;
using static SixpenceStudio.WeChat.WeChatMaterialExtension;

namespace SixpenceStudio.WeChat
{
    [RequestAuthorize]
    public class WeChatController : BaseController
    {
        /// <summary>
        /// 获取微信access_token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetAccessToken()
        {
            return WeChatService.AccessToken;
        }

        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="echostr"></param>
        [HttpGet, HttpPost, AllowAnonymous]
        public void Get()
        {
            if (HttpContext.Current.Request.HttpMethod.Equals("GET", StringComparison.OrdinalIgnoreCase))
            {
                var signature = HttpContext.Current.Request.QueryString["signature"];
                var timestamp = HttpContext.Current.Request.QueryString["timestamp"];
                var nonce = HttpContext.Current.Request.QueryString["nonce"];
                var echostr = HttpContext.Current.Request.QueryString["echostr"];
                var result = WeChatService.CheckSignature(signature, timestamp, nonce, echostr);
                if (result)
                {
                    HttpContext.Current.Response.Write(echostr);
                    HttpContext.Current.Response.End();
                }
            }
            else
            {
                // 消息接受
                var message = WeChatService.ReplyMessage(HttpContext.Current.Request.InputStream);
                HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
                HttpContext.Current.Response.Write(message);
                HttpContext.Current.Response.End();
            }
        }


        /// <summary>
        /// 获取微信素材
        /// </summary>
        /// <param name="code"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public object GetMaterial(string code, int pageIndex, int pageSize)
        {
            if (code == MaterialType.news.ToMaterialTypeString())
            {
                return WeChatService.GetWeChatNewsMaterial(code, pageIndex, pageSize);
            }
            return WeChatService.GetWeChatOtherMaterial(code, pageIndex, pageSize);
        }
    }
}