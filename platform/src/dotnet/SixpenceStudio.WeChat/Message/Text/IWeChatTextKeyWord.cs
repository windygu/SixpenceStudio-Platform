using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.WeChat.Message.Text
{
    public interface IWeChatTextKeyWord
    {
        string GetMessage(string message);
    }
}