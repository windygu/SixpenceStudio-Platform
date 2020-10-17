using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace SixpenceStudio.BaseSite.WeChat
{
    public class WeChat
    {
        public string appid { get; set; }
        public string token { get; set; }
        public string secret { get; set; }
        public string encodingAESKey { get; set; }
    }

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

    public static class MaterialTypeExtension
    {
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