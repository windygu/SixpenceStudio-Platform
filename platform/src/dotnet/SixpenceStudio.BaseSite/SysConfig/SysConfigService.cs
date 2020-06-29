using SixpenceStudio.Platform.Command;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Entity;
using SixpenceStudio.Platform.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.BaseSite.SysConfig
{
    public class SysConfigService : EntityService<sys_config>
    {
        #region 构造函数
        public SysConfigService()
        {
            _cmd = new EntityCommand<sys_config>();
        }

        public SysConfigService(IPersistBroker broker)
        {
            _cmd = new EntityCommand<sys_config>(broker);
        }
        #endregion

    }
}