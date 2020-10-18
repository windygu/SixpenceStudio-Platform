using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.BaseSite.WeChat.Message
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
        /// 发送消息
        /// </summary>
        void SendMessage();
    }
}
