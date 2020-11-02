﻿#region 类文件描述
/*********************************************************
Copyright @ Sixpence Studio All rights reserved. 
Author   : Karl Du
Created: 2020/11/1 20:32:16
Description：微信关键词服务 Controller
********************************************************/
#endregion

using SixpenceStudio.BaseSite;
using SixpenceStudio.Platform.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.WeChat.WeChatReply.Keywords
{
    [RequestAuthorize]
    public class WeChatKeywordsController : EntityBaseController<wechat_keywords, WeChatKeywordsService>
    {

    }
}