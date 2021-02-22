﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.WeChat.Robot
{
    public class RobotClientFacotry
    {
        public static IRobotClient GetClient(string name, string hook)
        {
            switch (name)
            {
                case "enterprise_wechat":
                    return new EnterpriseWechatRobotClient(hook);
                default:
                    return null;
            }
        }
    }
}
