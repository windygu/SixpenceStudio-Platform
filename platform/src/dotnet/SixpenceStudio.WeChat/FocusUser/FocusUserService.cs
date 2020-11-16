using Newtonsoft.Json;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Logging;
using SixpenceStudio.Platform.Service;
using SixpenceStudio.Platform.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.WeChat.FocusUser
{
    public class FocusUserService : BaseService
    {
        public FocusUserService()
        {
            broker = PersistBrokerFactory.GetPersistBroker();
            logger = LogFactory.GetLogger("wechat");
        }

        public FocusUserListModel GetFocusUserList()
        {
            var resp = WeChatApi.GetFocusUserList();
            return JsonConvert.DeserializeObject<FocusUserListModel>(resp);
        }

        public FocusUsersModel GetFocusUsers(string userList)
        {
            var resp2 = WeChatApi.BatchGetFocusUser(JsonConvert.SerializeObject(userList));
            var focusUsers = JsonConvert.DeserializeObject<FocusUsersModel>(resp2);
            return focusUsers;
        }
    }
}
