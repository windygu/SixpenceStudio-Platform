using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using SixpenceStudio.BaseSite.SysParams;
using SixpenceStudio.BaseSite.WeChat.ResponseModel;
using SixpenceStudio.Platform;
using SixpenceStudio.Platform.Configs;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace SixpenceStudio.BaseSite.WeChat
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
            var url = string.Format(WeChatApi.GetAccessToken, _weChat.appid, _weChat.secret);
            var resp = HttpUtil.Get(url);
            var respJson = JObject.Parse(resp);
            tokenExpireDatetime = new DateTime();
            ExceptionUtil.CheckBoolean<SpException>(respJson.GetValue("errcode") != null && respJson.GetValue("errcode").ToString() != "0", "获取微信授权失败", "87A36C30-3A62-457A-8D01-1A1E2C9250FC");
            _accessToken = respJson.GetValue("access_token").ToString();
            expireSeconds = Convert.ToInt32(respJson.GetValue("expires_in").ToString());
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
        /// 根据选项集id获取素材类型
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public static string GetMaterialType(string typeId)
        {
            var broker = PersistBrokerFactory.GetPersistBroker();
            var materialType = broker.Retrieve<sys_param>(typeId)?.code;
            return materialType;
        }

        /// <summary>
        /// 获取图文素材
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static string GetWeChatMaterial(string type, int pageIndex, int pageSize)
        {
            var url = string.Format(WeChatApi.GetMaterial, AccessToken);
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
        /// 获取图文素材
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static WeChatNewsMaterial GetWeChatNewsMaterial(string type, int pageIndex, int pageSize)
        {
            var result = GetWeChatMaterial(type, pageIndex, pageSize);
            var materialList = JsonConvert.DeserializeObject<WeChatNewsMaterial>(result);
            if (materialList == null || materialList.item == null || materialList.item.Count <= 0)
            {
                return materialList;
            }

            materialList.item.ForEach(item =>
            {
                var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                item.UpdateTime = start.AddMilliseconds(item.update_time * 1000).ToLocalTime().ToString("yyyy-MM-dd HH:mm");
            });
            return materialList;
        }

        /// <summary>
        /// 获取视频、图片、语音素材
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static WeChatOtherMaterial GetWeChatOtherMaterial(string type, int pageIndex, int pageSize)
        {
            var result = GetWeChatMaterial(type, pageIndex, pageSize);
            var materialList = JsonConvert.DeserializeObject<WeChatOtherMaterial>(result);
            if (materialList == null || materialList.item == null || materialList.item.Count <= 0)
            {
                return materialList;
            }

            materialList.item.ForEach(item =>
            {
                var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                item.UpdateTime = start.AddMilliseconds(item.update_time * 1000).ToLocalTime().ToString("yyyy-MM-dd HH:mm");
            });
            return materialList;
        }
    }
}