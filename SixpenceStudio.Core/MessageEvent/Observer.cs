using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.MessageEvent
{
    /// <summary>
    /// 观察者抽象类
    /// </summary>
    public abstract class Observer
    {
        private List<IObserver> observers = new List<IObserver>();

        /// <summary>
        /// 添加观察者
        /// </summary>
        /// <param name="observer"></param>
        public virtual void Add(IObserver observer)
        {
            observers.Add(observer);
        }

        /// <summary>
        /// 移出观察者
        /// </summary>
        /// <param name="observer"></param>
        public virtual void Remove(IObserver observer)
        {
            observers.Remove(observer);
        }

        /// <summary>
        /// 通知更新
        /// </summary>
        public virtual void Notify()
        {
            observers.ForEach(item => item.Receive(this));
        }
    }
}
