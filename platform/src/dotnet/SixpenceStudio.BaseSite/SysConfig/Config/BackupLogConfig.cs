using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.BaseSite.SysConfig.Config
{
    public class BackupLogConfig : BaseConfig
    {
        public override string Name => "备份天数";

        public override object DefaultValue { get => 30; }

        public override string Code => "log_backup_days";
    }
}