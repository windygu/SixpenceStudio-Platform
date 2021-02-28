using SixpenceStudio.Core.Configs;
using SixpenceStudio.Core.Data.DBClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Data
{
    /// <summary>
    /// Broker工厂类（所有获取Borker实例应该从工厂类获取）
    /// </summary>
    public static class PersistBrokerFactory
    {
        /// <summary>
        /// 获取Broker
        /// </summary>
        /// <param name="dBType"></param>
        /// <returns></returns>
        public static IPersistBroker GetPersistBroker(DBType dBType = DBType.Main)
        {
            var config = ConfigFactory.GetConfig<DBSection>();
            return new PersistBroker(config.ConfigCollection[dBType.ToString()].Value);
        }
    }

    public enum DBType
    {
        [Description("主库")]
        Main,
        [Description("从库")]
        StandBy
    }
}
