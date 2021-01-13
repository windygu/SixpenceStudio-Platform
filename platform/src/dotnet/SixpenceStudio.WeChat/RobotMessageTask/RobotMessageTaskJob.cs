using Quartz;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Job;
using SixpenceStudio.WeChat.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.WeChat.RobotMessageTask
{
    public class RobotMessageTaskJob : JobBase
    {
        public override string Name => "机器人消息任务";

        public override string Description => "机器人消息的任务";

        public override string CronExperssion => "";

        public override void Executing(IJobExecutionContext context)
        {
            var entity = context.JobDetail.JobDataMap.Get("Context") as robot_message_task;
            var broker = PersistBrokerFactory.GetPersistBroker();
            var robot = broker.Retrieve<robot>(entity.robotid);
            var client = RobotClientFacotry.GetClient(robot.robot_type, robot.hook);
            client.SendTextMessage(entity.content);
        }
    }
}
