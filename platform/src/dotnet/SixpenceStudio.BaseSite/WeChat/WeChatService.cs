using Newtonsoft.Json.Linq;
using SixpenceStudio.Platform;
using SixpenceStudio.Platform.Configs;
using SixpenceStudio.Platform.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace SixpenceStudio.BaseSite.WeChat
{
    public class WeChatService
    {
        private WeChat _weChat { get; set; }

        private string accessToken;
        private DateTime tokenExpireDatetime;
        private int expireSeconds = 7200;

        public WeChatService()
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

        public void RefreshToken()
        {
            var url = string.Format(WeChatApi.GetAccessToken, _weChat.appid, _weChat.secret);
            var resp = HttpUtil.Get(url);
            var respJson = JObject.Parse(resp);
            tokenExpireDatetime = new DateTime();
            ExceptionUtil.CheckBoolean<SpException>(respJson.GetValue("errcode") != null && respJson.GetValue("errcode").ToString() != "0", "获取微信授权失败", "87A36C30-3A62-457A-8D01-1A1E2C9250FC");
            accessToken = respJson.GetValue("access_token").ToString();
            expireSeconds = Convert.ToInt32(respJson.GetValue("expires_in").ToString());
        }

        public string GetAccessToken()
        {   
            if (string.IsNullOrEmpty(accessToken) || DateTime.Now > tokenExpireDatetime.AddSeconds(expireSeconds))
            {
                RefreshToken();
            }
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
        public bool CheckSignature(string signature, string timestamp, string nonce, string echostr)
        {
            string[] arrTmp = { _weChat.token, timestamp, nonce };
            Array.Sort(arrTmp);
            var tmpStr = string.Join("", arrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1").ToLower();
            return tmpStr == signature;
        }
    }
}