using SixpenceStudio.BaseSite.WeChat.Message.Text;
using SixpenceStudio.Platform.WebApi;
using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Xml;
using static SixpenceStudio.BaseSite.WeChat.WeChatMaterialExtension;

namespace SixpenceStudio.BaseSite.WeChat
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
                StreamReader str = new StreamReader(HttpContext.Current.Request.InputStream, Encoding.UTF8);
                XmlDocument xml = new XmlDocument();
                xml.Load(str);
                str.Close();
                str.Dispose();

                switch (xml.SelectSingleNode("xml").SelectSingleNode("MsgType").InnerText)
                {
                    case "text":
                        new WeChatTextMessageService(new WeChatTextMessage(xml)).SendMessage();
                        break;
                    default:
                        break;
                }
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
            var type = WeChatService.GetMaterialType(typeId);
            if (type == MaterialType.news.ToMaterialTypeString())
            {
                return WeChatService.GetWeChatNewsMaterial(type, pageIndex, pageSize);
            }
            return WeChatService.GetWeChatOtherMaterial(type, pageIndex, pageSize);
        }
    }
}