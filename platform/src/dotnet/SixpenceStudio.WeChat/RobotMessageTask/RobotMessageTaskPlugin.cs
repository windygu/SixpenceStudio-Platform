using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.WeChat.RobotMessageTask
{
    public class RobotMessageTaskPlugin : IPersistBrokerPlugin
    {
        public void Execute(Context context)
        {
            var obj = context.Entity as robot_message_task;
            switch (context.Action)
            {
                case EntityAction.PostCreate:
                case EntityAction.PostUpdate:
                    JobHelpers.Run<RobotMessageTaskJob>(obj.runtime, obj.name, obj.robotidName, obj).Wait();
                    break;
                case EntityAction.PreUpdate:
                case EntityAction.PostDelete:
                    JobHelpers.DeleteJob(obj.name, obj.robotidName);
                    break;
                default:
                    break;
            }
        }
    }
}
