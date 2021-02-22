using SixpenceStudio.Core.Configs;
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
        public static IPersistBroker GetPersistBroker(DBType dBType = DBType.MainDB)
        {
            switch (dBType)
            {
                case DBType.MainDB:
                    return new PersistBroker(new DbConnectionConfig().GetValue());
                case DBType.StandByDB:
                    return new PersistBroker(new StandByDbConnectionConfig().GetValue());
                default:
                    return null;
            }
        }

        /// <summary>
        /// 获取只读库
        /// </summary>
        /// <returns></returns>
        public static IPersistBroker GetReadonlyPersistBroker()
        {
            return new PersistBroker(new StandByDbConnectionConfig().GetValue());
        }
    }

    public enum DBType
    {
        [Description("主库")]
        MainDB,
        [Description("从库")]
        StandByDB
    }
}
