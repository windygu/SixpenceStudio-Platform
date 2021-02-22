using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.WeChat.Robot
{
    public class RobotService : EntityService<robot>
    {
        #region 构造函数
        public RobotService()
        {
            _cmd = new EntityCommand<robot>();
        }

        public RobotService(IPersistBroker broker)
        {
            _cmd = new EntityCommand<robot>(Broker);
        }
        #endregion
    }
}
