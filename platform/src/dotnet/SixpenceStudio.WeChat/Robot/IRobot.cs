using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.WeChat.Robot
{
    interface IRobot
    {
        void SendTextMessage(string text);
    }
}
