using SixpenceStudio.BaseSite;
using SixpenceStudio.Platform.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.WeChat.FocusUser
{
    [RequestAuthorize]
    public class WeChatUserController : EntityBaseController<wechat_user, WeChatUserService>
    {
        public FocusUserListModel GetFocusUserList()
        {
            return new FocusUserService().GetFocusUserList();
        }
    }
}
