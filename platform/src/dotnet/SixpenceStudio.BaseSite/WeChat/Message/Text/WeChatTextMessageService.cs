using SixpenceStudio.Platform.Logging;
using SixpenceStudio.Platform.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.BaseSite.WeChat.Message.Text
{
    /// <summary>
    /// 文本消息服务类
    /// </summary>
    public class WeChatTextMessageService : IWeChatMessageService<WeChatTextMessage>
    {
        public WeChatTextMessageService(WeChatTextMessage xml)
        {
            Message = xml;
        }

        public BaseWeChatMessage Message { get; set; }

        public string MessageTemplate
        {
            get
            {
                return @"
<xml>
    <ToUserName><![CDATA[{0}]]></ToUserName>
    <FromUserName><![CDATA[{1}]]></FromUserName>
    <CreateTime>{2}</CreateTime>
    <MsgType><![CDATA[text]]></MsgType>
    <Content><![CDATA[{3}]]></Content>
</xml>
";
            }
        }

        public void SendMessage()
        {
            var responseMessage = string.Empty;

            if (!string.IsNullOrEmpty(Message.EventName) && Message.EventName.Trim() == "subscribe")
            {
                responseMessage = @"
您好，欢迎关注六便士公众号！
";
            }
            else
            {
                responseMessage = "对不起，我不能识别你的命令";
            }

            var res = string.Format(MessageTemplate, Message.ToUserName, Message.FromUserName, DateTime.Now.Ticks, responseMessage);
            LogUtils.DebugLog(@"回复内容：" + res);
            HttpContext.Current.Response.Write("");
            HttpContext.Current.Response.End();
        }
    }
}