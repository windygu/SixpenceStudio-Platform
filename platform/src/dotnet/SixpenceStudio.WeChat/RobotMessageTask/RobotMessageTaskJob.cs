using Quartz;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Job;
using SixpenceStudio.Core.Logging;
using SixpenceStudio.WeChat.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.WeChat.RobotMessageTask
{
    public class RobotMessageTaskJob : DynamicJobBase
    {
        public RobotMessageTaskJob() { }
        public RobotMessageTaskJob(string name, string group, string cron) : base(name, group, cron) { }

        public override void Executing(IJobExecutionContext context)
        {
            var entity = context.JobDetail.JobDataMap.Get("Entity") as robot_message_task;
            var broker = PersistBrokerFactory.GetPersistBroker();
            var robot = broker.Retrieve<robot>(entity.robotid);
            //var client = RobotClientFacotry.GetClient(robot.robot_type, robot.hook);
            //client.SendTextMessage(entity.content);
            Logger.Debug($"机器人[{robot.name}]发送了一条消息[{entity.content}]");
        }
    }
}
