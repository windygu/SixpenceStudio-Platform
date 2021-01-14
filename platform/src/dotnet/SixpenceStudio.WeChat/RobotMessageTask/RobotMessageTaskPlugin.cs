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
            if (context.EntityName != "robot_message_task")
            {
                return;
            }

            var obj = context.Entity as robot_message_task;
            switch (context.Action)
            {
                case EntityAction.PostCreate:
                case EntityAction.PostUpdate:
                    JobHelpers.RegisterJob<RobotMessageTaskJob>(obj, obj.runtime);
                    break;
                case EntityAction.PreUpdate:
                case EntityAction.PostDelete:
                    JobHelpers.DeleteJob(obj.name, obj.GetType().Namespace);
                    break;
                default:
                    break;
            }
        }
    }
}
