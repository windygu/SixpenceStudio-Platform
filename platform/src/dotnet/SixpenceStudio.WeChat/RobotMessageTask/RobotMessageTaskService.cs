using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.Job;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.WeChat.RobotMessageTask
{
    public class RobotMessageTaskService : EntityService<robot_message_task>
    {
        #region 构造函数
        public RobotMessageTaskService()
        {
            _cmd = new EntityCommand<robot_message_task>();
        }

        public RobotMessageTaskService(IPersistBroker broker)
        {
            _cmd = new EntityCommand<robot_message_task>(broker);
        }
        #endregion

        public IEnumerable<robot_message_task> GetAllData()
        {
            return _cmd.GetAllEntity();
        }

        public void RegisterJob()
        {
            var dataList = _cmd.GetAllEntity();
            dataList.Each(item =>
            {
                
            });
        }
    }
}
