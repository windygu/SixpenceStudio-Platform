using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SixpenceStudio.Platform;
using SixpenceStudio.Platform.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.WeChat
{
    internal static class WeChatApi
    {
        public const string BaseUrl = "https://api.weixin.qq.com/";

        public static readonly string GetAccessTokenApi = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
        /// <summary>
        /// 获取微信Token
        /// </summary>
        public static (string AccessToken, int Expire) GetAccessToken(string appid, string secret)
        {
            var url = string.Format(GetAccessTokenApi, appid, secret);
            var resp = HttpUtil.Get(url);
            var respJson = JObject.Parse(resp);
            ExceptionUtil.CheckBoolean<SpException>(respJson.GetValue("errcode") != null && respJson.GetValue("errcode").ToString() != "0", "获取微信授权失败", "87A36C30-3A62-457A-8D01-1A1E2C9250FC");
            return (respJson.GetValue("access_token").ToString(), Convert.ToInt32(respJson.GetValue("expires_in").ToString()));
        }

        /// <summary>
        /// 批量获取微信素材
        /// </summary>
        public static readonly string BatchGetMaterialApi = "https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={0}";
        public static string BatchGetMaterial(string type, int pageIndex, int pageSize)
        {
            var url = string.Format(BatchGetMaterialApi, WeChatService.AccessToken);
            var postData = new
            {
                type,
                offset = (pageIndex - 1) * pageSize,
                count = pageSize
            };
            var result = HttpUtil.Post(url, JsonConvert.SerializeObject(postData));
            var resultJson = JObject.Parse(result);

            ExceptionUtil.CheckBoolean<SpException>(resultJson.GetValue("errcode") != null && resultJson.GetValue("errcode").ToString() != "0", "获取微信素材失败", "87A36C30-3A62-457A-8D01-1A1E2C9250FC");
            return result;
        }

        /// <summary>
        /// 发送微信消息
        /// </summary>
        public static readonly string SendMessage = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}";

        /// <summary>
        /// 获取微信素材
        /// </summary>
        public static readonly string GetMaterial = "https://api.weixin.qq.com/cgi-bin/material/get_material?access_token={0}";


        public static readonly string GetFocusUserListApi = "https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}&next_openid={1}";
        /// <summary>
        /// 获取关注用户列表
        /// <para>Access_Token</para>
        /// <para>next_openid</para>
        /// </summary>
        public static string GetFocusUserList(string nextOpenId = "")
        {
            var resp = HttpUtil.Get(string.Format(GetFocusUserListApi, WeChatService.AccessToken, nextOpenId));
            var respJson = JObject.Parse(resp);
            ExceptionUtil.CheckBoolean<SpException>(respJson.GetValue("errcode") != null && respJson.GetValue("errcode").ToString() != "0", "获取关注用户列表失败", "C84A4B94-2B34-4F9C-9B85-9A260F8F9F98");
            return resp;
        }

        /// <summary>
        /// 获取关注用户基本信息
        /// <para>Access_Token</para>
        /// <para>openid</para>
        /// </summary>
        public static readonly string GetFocusUser = "https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN";

        public static readonly string BatchGetFocusUserApi = "https://api.weixin.qq.com/cgi-bin/user/info/batchget?access_token={0}";
        /// <summary>
        /// 批量获取关注用户基本信息
        /// </summary>
        public static string BatchGetFocusUser(string postData)
        {
            var resp = HttpUtil.Post(string.Format(BatchGetFocusUserApi, WeChatService.AccessToken), postData);
            var respJson = JObject.Parse(resp);
            ExceptionUtil.CheckBoolean<SpException>(respJson.GetValue("errcode") != null && respJson.GetValue("errcode").ToString() != "0", "批量获取关注用户基本信息失败", "D8E6DEC8-6887-4EAE-B685-9F175C1FB806");
            return resp;
        }
    }
}