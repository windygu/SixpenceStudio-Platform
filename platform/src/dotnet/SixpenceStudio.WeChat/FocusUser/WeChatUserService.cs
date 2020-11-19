using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.WeChat.FocusUser
{
    public class WeChatUserService : EntityService<wechat_user>
    {
        #region 构造函数
        public WeChatUserService()
        {
            _cmd = new EntityCommand<wechat_user>();
        }
       
        public WeChatUserService(IPersistBroker broker)
        {
            _cmd = new EntityCommand<wechat_user>(broker);
        }
        #endregion
    }
}
