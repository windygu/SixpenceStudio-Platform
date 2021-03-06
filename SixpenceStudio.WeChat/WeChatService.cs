using SixpenceStudio.Core;
using SixpenceStudio.Core.Configs;
using SixpenceStudio.Core.Logging;
using SixpenceStudio.Core.Utils;
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
        private static readonly object lockObject = new Object();
        /// <summary>
        /// 获取access_token
        /// </summary>
        public static string AccessToken
        {
            get
            {
                var accessToken = MemoryCacheUtil.GetCacheItem<AccessTokenResponse>("AccessToken");
                if (accessToken == null)
                {
                    lock (lockObject)
                    {
                        if (accessToken == null)
                        {
                            accessToken = RefreshToken();
                        }
                    }
                }
                return accessToken.AccessToken;
            }
        }



        static WeChatService()
        {
            var config = ConfigFactory.GetConfig<WeChatSection>();
            AssertUtil.CheckBoolean<SpException>(config == null, "未找到微信公众号配置", "87A36C30-3A62-457A-8D01-1A1E2C9250FC");
            _appid = config.appid;
            _token = config.token;
            _secret = config.secret;
            _encodingAESKey = config.encodingAESKey;
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        public static AccessTokenResponse RefreshToken()
        {
            var result = WeChatApi.GetAccessToken(_appid, _secret);
            var accessToken = new AccessTokenResponse()
            {
                AccessToken = result.AccessToken,
                Expire = result.Expire
            };
            MemoryCacheUtil.RemoveCacheItem("AccessToken");
            MemoryCacheUtil.Set("AccessToken", accessToken);
            var logger = LogFactory.GetLogger("wechat");
            logger.Debug("获取微信access_token成功：" + accessToken.AccessToken);
            return accessToken;
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