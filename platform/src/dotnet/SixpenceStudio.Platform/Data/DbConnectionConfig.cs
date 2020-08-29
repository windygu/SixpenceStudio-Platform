using SixpenceStudio.Platform.Configs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Data
{
    [Description("默认数据库连接节点")]
    public class DbConnectionConfig : BaseAppSettingsConfig
    {
        public override string Key => "DbConnectrionString";
    }
}
