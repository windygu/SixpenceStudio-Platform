﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.BaseSite.SysConfig.Config
{
    /// <summary>
    /// 备份设置
    /// </summary>
    public class BackupLogConfig : ISysConfig
    {
        public string Name => "备份天数";

        public object DefaultValue { get => 30; }

        public string Code => "log_backup_days";
    }
}