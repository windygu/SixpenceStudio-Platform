using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SixpenceStudio.BaseSite.WeChat
{
    public static class WeChatMaterialExtension
    {

        /// <summary>
        /// 素材类型枚举
        /// </summary>
        public enum MaterialType
        {
            [Description("图片")]
            image,
            [Description("视频")]
            video,
            [Description("语音")]
            voice,
            [Description("图文")]
            news
        }

        /// <summary>
        /// 获取素材类型字符串值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ToMaterialTypeString(this MaterialType type)
        {
            switch (type)
            {
                case MaterialType.image:
                    return "image";
                case MaterialType.video:
                    return "video";
                case MaterialType.voice:
                    return "voice";
                case MaterialType.news:
                    return "news";
                default:
                    return "";
            }
        }
    }
}