#region 类文件描述
/*********************************************************
Copyright @ Sixpence Studio All rights reserved. 
Author   : Karl Du
Created: 2020/11/8 17:38:08
Description：微信自动回复消息
********************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.WeChat.Message
{
    interface IWeChatReplyMessage
    {

        /// <summary>
        /// 获取关注回复消息
        /// </summary>
        /// <returns></returns>
        string GetFocusMessage();

        /// <summary>
        /// 获取关键词回复消息
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        string GetKeywordsMessage(string content);
    }
}
