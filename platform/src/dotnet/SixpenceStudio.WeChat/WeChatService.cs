using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SixpenceStudio.BaseSite.SysParams;
using SixpenceStudio.Platform;
using SixpenceStudio.Platform.Configs;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Utils;
using SixpenceStudio.WeChat.Message;
using SixpenceStudio.WeChat.Message.Text;
using SixpenceStudio.WeChat.ResponseModel;
using SixpenceStudio.WeChat.WeChatReply.Focus;
using SixpenceStudio.WeChat.WeChatReply.Keywords;
using System;
using System.IO;
using System.Text;
using System.Web.Security;
using System.Xml;

namespace SixpenceStudio.WeChat
{
    public static class WeChatService
    {
        private static WeChat _weChat { get; set; }

        private static string _accessToken;

        /// <summary>
        /// 获取access_token
        /// </summary>
        public static string AccessToken
        {
            get
            {
                if (string.IsNullOrEmpty(_accessToken) || DateTime.Now > tokenExpireDatetime.AddSeconds(expireSeconds))
                {
                    RefreshToken();
                }
                return _accessToken;
            }
        }

        private static DateTime tokenExpireDatetime;
        private static int expireSeconds = 7200;

        static WeChatService()
        {
            var config = ConfigFactory.GetConfig<WeChatSection>();
            ExceptionUtil.CheckBoolean<SpException>(config == null, "未找到微信公众号配置", "87A36C30-3A62-457A-8D01-1A1E2C9250FC");
            _weChat = new WeChat()
            {
                appid = config.appid,
                token = config.token,
                secret = config.secret,
                encodingAESKey = config.encodingAESKey
            };
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        public static void RefreshToken()
        {
            var result = WeChatApi.GetAccessToken(_weChat.appid, _weChat.secret);
            tokenExpireDatetime = new DateTime();
            _accessToken = result.AccessToken;
            expireSeconds = result.Expire;
        }

        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="echostr"></param>
        /// <returns></returns>
        public static bool CheckSignature(string signature, string timestamp, string nonce, string echostr)
        {
            string[] arrTmp = { _weChat.token, timestamp, nonce };
            Array.Sort(arrTmp);
            var tmpStr = string.Join("", arrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1").ToLower();
            return tmpStr == signature;
        }

        /// <summary>
        /// 回复消息
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string ReplyMessage(Stream stream)
        {
            XmlDocument xml = new XmlDocument();
            var bytes = StreamUtil.StreamToBytes(stream);
            var postString = Encoding.UTF8.GetString(bytes);
            xml.LoadXml(postString);

            switch (xml.SelectSingleNode("xml").SelectSingleNode("MsgType").InnerText)
            {
                case "text":
                    return new WeChatKeywordsService().GetReplyMessage(new WeChatTextMessage(xml));
                case "event":
                    return new WeChatFocusReplyService().GetReplyMessage(new BaseWeChatMessage(xml));
                default:
                    return "success";
            }
        }
    }

    /// <summary>
    /// 微信开发者参数
    /// </summary>
    public class WeChat
    {
        public string appid { get; set; }
        public string token { get; set; }
        public string secret { get; set; }
        public string encodingAESKey { get; set; }
    }
}