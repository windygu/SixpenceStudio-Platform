using SixpenceStudio.Platform.Command;
using SixpenceStudio.Platform.Configs;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var data = _cmd.broker.Retrieve<wechat_focus_reply>(sql, new Dictionary<string, object>() { { "@wechat", config.appid } });
            return data;
        }

        public override string CreateData(wechat_focus_reply t)
        {
            t.wechat = ConfigFactory.GetConfig<WeChatSection>().appid;
            return base.CreateData(t);
        }
    }
}
