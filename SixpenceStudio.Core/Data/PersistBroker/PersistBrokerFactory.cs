using SixpenceStudio.Core.Configs;
using SixpenceStudio.Core.Data.DBClient;
using SixpenceStudio.Core.Extensions;
using SixpenceStudio.Core.Utils;
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
        private static readonly object lockObj = new object();

        /// <summary>
        /// 缓存数据库链接字符串，避免重复读取开销
        /// </summary>
        private static Dictionary<DBType, DBModel> dbDic = new Dictionary<DBType, DBModel>();

        /// <summary>
        /// 获取Broker
        /// </summary>
        /// <param name="dBType"></param>
        /// <returns></returns>
        public static IPersistBroker GetPersistBroker(DBType dBType = DBType.Main)
        {
            if (!dbDic.ContainsKey(dBType))
            {
                lock (lockObj)
                {
                    if (!dbDic.ContainsKey(dBType))
                    {
                        var config = ConfigFactory.GetConfig<DBSection>();
                        AssertUtil.CheckBoolean<SpException>(config == null || !config.ConfigCollection.AllKeys.Contains(DBType.Main.ToString()), "未找到数据库配置", "AD4BC4F2-CF8D-4A4E-ACE8-F68EBD89DE42");
                        var dbConfig = config.ConfigCollection[DBType.Main.ToString()];
                        AssertUtil.CheckIsNullOrEmpty<SpException>(dbConfig.Value, "数据库连接字符串为空", "AD4BC4F2-CF8D-4A4E-ACE8-F68EBD89DE42");
                        AssertUtil.CheckBoolean<SpException>(!Enum.TryParse<DriverType>(dbConfig.DriverType, out var driverType), "数据库类型错误", "AD4BC4F2-CF8D-4A4E-ACE8-F68EBD89DE42");
                        dbDic.Add(dBType, new DBModel(DecryptAndEncryptHelper.AESDecrypt(dbConfig.Value), driverType));
                    }
                }
            }

            var section = dbDic[dBType];
            return new PersistBroker(section.ConnectionString, section.DriverType);
        }

        /// <summary>
        /// 获取Broker新实例
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="isEncrypted">是否加密（仅支持AES）</param>
        /// <returns></returns>
        public static IPersistBroker GetPersistBroker(string connectionString, bool isEncrypted = false, DriverType driverType = DriverType.Postgresql)
        {
            AssertUtil.CheckIsNullOrEmpty<SpException>(connectionString, "数据库连接字符串为空", "AD4BC4F2-CF8D-4A4E-ACE8-F68EBD89DE42");
            if (isEncrypted)
            {
                return new PersistBroker(DecryptAndEncryptHelper.AESDecrypt(connectionString), driverType);
            }
            return new PersistBroker(connectionString, driverType);
        }

        /// <summary>
        /// 数据库节点
        /// </summary>
        class DBModel
        {
            public DBModel(string connectionString, DriverType driverType)
            {
                this.ConnectionString = connectionString;
                this.DriverType = driverType;
            }

            public string ConnectionString { get; set; }
            public DriverType DriverType { get; set; }
        }
    }

    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DBType
    {
        /// <summary>
        /// 主库（负责读和写）
        /// </summary>
        [Description("主库")]
        Main,
        /// <summary>
        /// 从库（负责读）
        /// </summary>
        [Description("从库")]
        StandBy
    }

    public enum DriverType
    {
        Postgresql,
        Mysql
    }
}
