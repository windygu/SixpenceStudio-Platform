using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.WeChat
{
    internal static class WeChatApi
    {
        public const string BaseUrl = "https://api.weixin.qq.com/";

        /// <summary>
        /// 获取微信Token
        /// </summary>
        public static readonly string GetAccessToken = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";

        /// <summary>
        /// 批量获取微信素材
        /// </summary>
        public static readonly string BatchGetMaterial = "https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={0}";

        /// <summary>
        /// 发送微信消息
        /// </summary>
        public static readonly string SendMessage = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}";

        /// <summary>
        /// 获取微信素材
        /// </summary>
        public static readonly string GetMaterial = "https://api.weixin.qq.com/cgi-bin/material/get_material?access_token={0}";

        /// <summary>
        /// 获取关注用户列表
        /// <para>Access_Token</para>
        /// <para>next_openid</para>
        /// </summary>
        public static readonly string GetFocusUserList = "https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}&next_openid={1}";

        /// <summary>
        /// 获取关注用户基本信息
        /// <para>Access_Token</para>
        /// <para>openid</para>
        /// </summary>
        public static readonly string GetFocusUser = "https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN";

        /// <summary>
        /// 批量获取关注用户基本信息
        /// </summary>
        public static readonly string BatchGetFocusUser = "https://api.weixin.qq.com/cgi-bin/user/info/batchget?access_token={0}";
    }
}