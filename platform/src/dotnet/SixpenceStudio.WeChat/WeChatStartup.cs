using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using SixpenceStudio.Core.Job;
using SixpenceStudio.Core.Logging;
using SixpenceStudio.Core.Startup;
using SixpenceStudio.Core.Utils;
using SixpenceStudio.WeChat.RobotMessageTask;

namespace SixpenceStudio.WeChat
{
    public class WeChatStartup : IStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var logger = LogFactory.GetLogger("startup");
            logger.Info("正在启动机器人作业...");
            new RobotMessageTaskService().GetAllData().Each(item =>
            {
                JobHelpers.Run<RobotMessageTaskJob>(item.runtime, item.name, item.GetType().Namespace, item).Wait();
                logger.Info($"机器人[{item.robotidName}]的[{item.name}]作业已启动");
            });
            logger.Info("机器人启动完毕");
        }
    }
}
