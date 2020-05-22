using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Command
{
    public interface IEntityActionPlugin
    {
        /// <summary>
        /// 创建前
        /// </summary>
        void PreCreate(Context context);

        /// <summary>
        /// 创建后
        /// </summary>
        void PostCreate(Context context);

        /// <summary>
        /// 更新前
        /// </summary>
        void PreUpdate(Context context);

        /// <summary>
        /// 更新后
        /// </summary>
        void PostUpdate(Context context);

        /// <summary>
        /// 删除前
        /// </summary>
        /// <param name="broker"></param>
        void PreDelete(Context context);
        
        /// <summary>
        /// 删除后
        /// </summary>
        /// <param name="broker"></param>
        void PostDelete(Context context);
    }

    public class Context
    {
        public IPersistBroker Broker { get; set; }
        public BaseEntity Entity { get; set; }
        public string EntityName { get; set; }
    }
}
