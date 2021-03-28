using SixpenceStudio.Core.Configs;
using SixpenceStudio.Core.Data.DBClient;
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
        private static Dictionary<DBType, string> dbDic = new Dictionary<DBType, string>();

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
                        var encryptionStr = config.ConfigCollection[DBType.Main.ToString()].Value;
                        AssertUtil.CheckIsNullOrEmpty<SpException>(encryptionStr, "数据库连接字符串为空", "AD4BC4F2-CF8D-4A4E-ACE8-F68EBD89DE42");
                        dbDic.Add(dBType, DecryptAndEncryptHelper.AESDecrypt(encryptionStr));
                    }
                }
            }

            var connectionString = dbDic[dBType];
            return new PersistBroker(connectionString);
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
}
