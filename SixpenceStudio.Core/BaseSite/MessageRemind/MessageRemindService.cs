using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.MessageRemind
{
    public class MessageRemindService : EntityService<message_remind>
    {
        #region 构造函数
        public MessageRemindService()
        {
            _cmd = new EntityCommand<message_remind>();
        }

        public MessageRemindService(IPersistBroker broker)
        {
            _cmd = new EntityCommand<message_remind>(broker);
        }
        #endregion
    }
}
