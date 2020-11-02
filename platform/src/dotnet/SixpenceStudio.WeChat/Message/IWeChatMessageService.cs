using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SixpenceStudio.WeChat.Message
{
    public interface IWeChatMessageService<E>
        where E : BaseWeChatMessage
    {
        /// <summary>
        /// 接收到的消息
        /// </summary>
        BaseWeChatMessage Message { get; set; }

        /// <summary>
        /// 消息模板
        /// </summary>
        string MessageTemplate { get; }

        /// <summary>
        /// 获取回复消息
        /// </summary>
        /// <returns></returns>
        string GetResponseMessage();
    }
}
