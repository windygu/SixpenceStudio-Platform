#region 类文件描述
/*********************************************************
Copyright @ Sixpence Studio All rights reserved. 
Author   : Karl Du
Created: 2020/11/18 20:56:58
Description：WeChatModel
********************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.WeChat
{
    public class BaseWeChatMaterial<T>
        where T : BaseWeChatMaterialItem, new()
    {
        public int total_count { get; set; }

        public int item_count { get; set; }

        public List<T> item { get; set; }
    }

    /// <summary>
    /// 微信图文素材
    /// </summary>
    public class WeChatNewsMaterial : BaseWeChatMaterial<WeChatNewsMaterialItem> { }

    /// <summary>
    /// 微信其他素材
    /// </summary>
    public class WeChatOtherMaterial : BaseWeChatMaterial<MediaMaterial> { }

    /// <summary>
    /// 微信素材内容基类
    /// </summary>
    public class BaseWeChatMaterialItem
    {
        /// <summary>
        /// 媒体Id
        /// </summary>
        public string media_id { get; set; }

        /// <summary>
        /// 更新时间（新增格式化后时间）
        /// </summary>
        public string UpdateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public long update_time { get; set; }
    }

    #region 图文消息素材

    /// <summary>
    /// 图文消息
    /// </summary>
    public class WeChatNewsMaterialItem : BaseWeChatMaterialItem
    {
        public NewsContent content { get; set; }
    }

    public class NewsContent
    {
        public List<NewsItem> news_item { get; set; }
    }

    public class NewsItem
    {
        /// <summary>
        /// 图文消息的标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string author { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string digest { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }
    }
    #endregion

    #region 其他素材
    /// <summary>
    /// 其他类型（图片、语音、视频）
    /// </summary>
    public class MediaMaterial : BaseWeChatMaterialItem
    {
        public string name { get; set; }

        public string url { get; set; }
    }
    #endregion

    public class WeChatErrorResponse
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
    }

    public class WeChatSuccessUploadResponse
    {
        public string media_id { get; set; }
        public string url { get; set; }
    }

    public class AccessTokenResponse
    {
        public string AccessToken { get; set; }
        public int Expire { get; set; }
    }
}
