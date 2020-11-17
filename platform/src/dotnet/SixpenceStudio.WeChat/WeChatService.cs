using SixpenceStudio.Platform;
using SixpenceStudio.Platform.Configs;
using SixpenceStudio.Platform.Utils;
using SixpenceStudio.WeChat.Message;
using SixpenceStudio.WeChat.Message.Text;
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
        private static string _appid;
        private static string _token;
        private static string _secret;
        private static string _encodingAESKey;
        private static string _accessToken;
        private static DateTime _tokenExpireDatetime;
        private static int _expireSeconds = 7200;

        /// <summary>
        /// 获取access_token
        /// </summary>
        public static string AccessToken
        {
            get
            {
                if (string.IsNullOrEmpty(_accessToken) || DateTime.Now > _tokenExpireDatetime.AddSeconds(_expireSeconds))
                {
                    RefreshToken();
                }
                return _accessToken;
            }
        }



        static WeChatService()
        {
            var config = ConfigFactory.GetConfig<WeChatSection>();
            ExceptionUtil.CheckBoolean<SpException>(config == null, "未找到微信公众号配置", "87A36C30-3A62-457A-8D01-1A1E2C9250FC");
            _appid = config.appid;
            _token = config.token;
            _secret = config.secret;
            _encodingAESKey = config.encodingAESKey;
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        public static void RefreshToken()
        {
            var result = WeChatApi.GetAccessToken(_appid, _secret);
            _tokenExpireDatetime = new DateTime();
            _accessToken = result.AccessToken;
            _expireSeconds = result.Expire;
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
            string[] arrTmp = { _token, timestamp, nonce };
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
}