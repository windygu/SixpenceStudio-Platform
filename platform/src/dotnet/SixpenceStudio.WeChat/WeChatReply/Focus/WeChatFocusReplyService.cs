using SixpenceStudio.BaseSite;
using SixpenceStudio.Platform.Configs;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Entity;
using SixpenceStudio.Platform.Logging;
using SixpenceStudio.WeChat.Message;
using System;
using System.Collections.Generic;

namespace SixpenceStudio.WeChat.WeChatReply.Focus
{
    public class WeChatFocusReplyService : EntityService<wechat_focus_reply>
    {
        #region 构造函数
        public WeChatFocusReplyService()
        {
            this._cmd = new EntityCommand<wechat_focus_reply>();
        }

        public WeChatFocusReplyService(IPersistBroker broker)
        {
            this._cmd = new EntityCommand<wechat_focus_reply>(broker);
        }
        #endregion

        private Logger Logger = LogFactory.GetLogger("wechat");

        public void Activate(string id)
        {
            var data = GetData(id);
            data.@checked = 1;
            UpdateData(data);
        }

        public void Deactivate(string id)
        {
            var data = GetData(id);
            data.@checked = 0;
            UpdateData(data);
        }

        public wechat_focus_reply GetData()
        {
            var config = ConfigFactory.GetConfig<WeChatSection>();
            var sql = @"
select * from wechat_focus_reply where wechat = @wechat
";
            var data = _cmd.Broker.Retrieve<wechat_focus_reply>(sql, new Dictionary<string, object>() { { "@wechat", config.appid } });
            return data;
        }

        /// <summary>
        /// 获取事件回复
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string GetReplyMessage(BaseWeChatMessage message)
        {
            if (!string.IsNullOrEmpty(message.EventName) && message.EventName.Trim() == "subscribe")
            {
                var reply = GetData()?.content ?? "感谢您的关注！";
                Logger.Info($"收到来自{message.FromUserName}的关注");
                return string.Format(WeChatMessageTemplate.Text, message.FromUserName, message.ToUserName, DateTime.Now.Ticks, reply);
            }
            return "";
        }
    }
}
