using Owin;
using SixpenceStudio.Core.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core
{
    /// <summary>
    /// 启动类（项目可以根据需求实现启动类）
    /// </summary>
    [UnityRegister]
    public interface IStartup
    {
        /// <summary>
        /// 启动顺序
        /// </summary>
        int OrderIndex { get; }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="app"></param>
        void Configuration(IAppBuilder app);
    }
}
