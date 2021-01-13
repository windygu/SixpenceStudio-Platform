using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using SixpenceStudio.Core.Startup;
using SixpenceStudio.WeChat.RobotMessageTask;

namespace SixpenceStudio.WeChat
{
    public class WeChatStartup : IStartup
    {
        public void Configuration(IAppBuilder app)
        {
            new RobotMessageTaskService().RegisterJob();
        }
    }
}
