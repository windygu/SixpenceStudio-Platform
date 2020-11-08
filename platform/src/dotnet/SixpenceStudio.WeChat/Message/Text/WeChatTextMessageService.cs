using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Logging;
using SixpenceStudio.Platform.Utils;
using SixpenceStudio.WeChat.WeChatReply.Focus;
using SixpenceStudio.WeChat.WeChatReply.Keywords;
using System;
using System.Linq;

namespace SixpenceStudio.WeChat.Message.Text
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

        public string GetResponseMessage()
        {
            var broker = PersistBrokerFactory.GetPersistBroker();
            var logger = LogFactory.GetLogger("wechat");
            var responseMessage = string.Empty;
            var textMessage = Message as WeChatTextMessage;

            var messageStrategy = AssemblyUtil.GetObjects<IWeChatReplyMessage>()?.FirstOrDefault();

            if (!string.IsNullOrEmpty(Message.EventName) && Message.EventName.Trim() == "subscribe")
            {
                var message = messageStrategy?.GetFocusMessage() ?? new WeChatFocusReplyService().GetData()?.content;
                responseMessage = string.IsNullOrEmpty(message) ? "感谢您的关注！" : message;
                logger.Info($"收到来自{textMessage.FromUserName}的关注");
            }
            else
            {
                logger.Info($"收到消息：{textMessage.Content}");

                // 实现了IWeChatTextKeyWord则以实现类回复
                var message = messageStrategy?.GetKeywordsMessage(textMessage.Content);
                if (message != null)
                {
                    responseMessage = message;
                }
                else
                {
                    var reply = new WeChatKeywordsService(broker).GetDataList(textMessage.Content).FirstOrDefault();
                    responseMessage = reply.reply_content;
                }

                if (string.IsNullOrEmpty(responseMessage))
                {
                    responseMessage = "对不起，我听不懂你在说什么";
                }
            }

            if (string.IsNullOrWhiteSpace(responseMessage))
            {
                return "success";
            }

            var res = string.Format(MessageTemplate, Message.FromUserName, Message.ToUserName, DateTime.Now.Ticks, responseMessage);
            logger.Info(@"回复内容：" + res);
            return res;
        }
    }
}