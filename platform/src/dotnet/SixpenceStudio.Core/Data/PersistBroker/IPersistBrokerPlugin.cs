using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.IoC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Data
{
    [UnityRegister]
    public interface IPersistBrokerPlugin
    {
        /// <summary>
        /// 执行
        /// </summary>
        void Execute(Context context);
    }

    /// <summary>
    /// 上下文
    /// </summary>
    public class Context
    {
        public IPersistBroker Broker { get; set; }
        public BaseEntity Entity { get; set; }
        public string EntityName { get; set; }
        public EntityAction Action { get; set; }
    }

    public enum EntityAction
    {
        [Description("创建前")]
        PreCreate,
        [Description("创建后")]
        PostCreate,
        [Description("更新前")]
        PreUpdate,
        [Description("更新后")]
        PostUpdate,
        [Description("删除前")]
        PreDelete,
        [Description("删除后")]
        PostDelete
    }
}
