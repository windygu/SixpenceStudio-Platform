using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.BaseSite.WeChat.Message.Text
{
    public interface IWeChatTextKeyWord
    {
        string GetMessage(string message);
    }
}