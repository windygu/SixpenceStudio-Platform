using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using SixpenceStudio.Core;
using SixpenceStudio.Core.Extensions;
using SixpenceStudio.Core.Job;
using SixpenceStudio.Core.Logging;
using SixpenceStudio.Core.Startup;
using SixpenceStudio.WeChat.RobotMessageTask;

namespace SixpenceStudio.WeChat
{
    public class WeChatStartup : IStartup
    {
        public int OrderIndex => 200;

        public void Configuration(IAppBuilder app)
        {
            var logger = LogFactory.GetLogger("startup");
            logger.Info("正在启动机器人作业...");
            try
            {
                new RobotMessageTaskService().GetAllData().Each(item =>
                {
                    JobHelpers.RegisterJob(new RobotMessageTaskJob(item.name, item.robotidName, item.runtime), item, item.job_state.ToTriggerState());
                    logger.Info($"机器人[{item.robotidName}]的[{item.name}]作业已启动");
                });
                logger.Info("机器人启动完毕");
            }
            catch (Exception e)
            {
                logger.Error("注册机器人失败", e);
                throw e;
            }
        }
    }
}
