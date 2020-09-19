using SixpenceStudio.Platform.Configs;
using SixpenceStudio.Platform.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Data
{
    /// <summary>
    /// 主库连接配置
    /// </summary>
    [Description("默认数据库连接节点")]
    public class DbConnectionConfig : BaseAppSettingsConfig
    {
        public override string Key => "DbConnectrionString";

        public override string GetValue()
        {
            return DecryptAndEncryptHelper.AESDecrypt(base.GetValue());
        }
    }

    /// <summary>
    /// 从库连接配置
    /// </summary>
    public class StandByDbConnectionConfig : BaseAppSettingsConfig
    {
        public override string Key => "StandByDbConnection";
        public override string GetValue()
        {
            return DecryptAndEncryptHelper.AESDecrypt(base.GetValue());
        }
    }
}
