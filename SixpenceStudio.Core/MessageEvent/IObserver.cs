using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.MessageEvent
{
    /// <summary>
    /// 订阅者接口
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// 接受消息
        /// </summary>
        /// <param name="obj"></param>
        void Receive(Object obj);
    }
}
