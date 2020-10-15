using Newtonsoft.Json.Linq;
using SixpenceStudio.Platform;
using SixpenceStudio.Platform.Configs;
using SixpenceStudio.Platform.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.BaseSite.WeChat
{
    public static class WeChatHttpUtil
    {
        private static readonly string appid;
        private static readonly string token;
        private static readonly string secret;

        private static string accessToken;
        private static DateTime tokenExpireDatetime;
        private static int expireSeconds = 7200;

        static WeChatHttpUtil()
        {
            var config = ConfigFactory.GetConfig<WeChatSection>();
            ExceptionUtil.CheckBoolean<SpException>(config == null, "未找到微信公众号配置", "87A36C30-3A62-457A-8D01-1A1E2C9250FC");
            appid = config.appid;
            token = config.token;
            secret = config.secret;
        }

        public static void RefreshToken()
        {
            var resp = HttpUtil.Get(string.Format(WeChatApi.GetAccessToken, appid, secret));
            var respJson = JObject.Parse(resp);
            tokenExpireDatetime = new DateTime();
            ExceptionUtil.CheckBoolean<SpException>(respJson.GetValue("errcode") != null && respJson.GetValue("errcode").ToString() != "0", "获取微信授权失败", "");
            accessToken = respJson.GetValue("access_token").ToString();
            expireSeconds = Convert.ToInt32(respJson.GetValue("expires_in").ToString());
        }

        public static string GetAccessToken()
        {   
            if (string.IsNullOrEmpty(accessToken) || DateTime.Now > tokenExpireDatetime.AddSeconds(expireSeconds))
            {
                RefreshToken();
            }
            return accessToken;
        }

    }
}