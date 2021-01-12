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
    public class RobotMessageTaskJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
                var entity = context.JobDetail.JobDataMap.Get("Context") as robot_message_task;
                var broker = PersistBrokerFactory.GetPersistBroker();
                var robot = broker.Retrieve<robot>(entity.robotid);
                var client = RobotClientFacotry.GetClient(robot.robot_type, robot.hook);
                client.SendTextMessage(entity.content);
            });
        }
    }
}
