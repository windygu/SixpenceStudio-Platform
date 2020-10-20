using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.BaseSite.WeChat
{
    public static class WeChatApi
    {
        public const string BaseUrl = "https://api.weixin.qq.com/";

        /// <summary>
        /// 获取微信Token
        /// </summary>
        public static string GetAccessToken = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";

        /// <summary>
        /// 获取微信素材
        /// </summary>
        public static string GetMaterial = "https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={0}";

        /// <summary>
        /// 发送微信消息
        /// </summary>
        public static string SendMessage = "https://api.weixin.qq.com/cgi-bin/template/api_add_template?access_token={0}";
    }
}