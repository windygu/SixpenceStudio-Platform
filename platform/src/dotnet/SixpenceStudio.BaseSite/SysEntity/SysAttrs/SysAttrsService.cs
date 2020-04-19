using SixpenceStudio.Platform.Command;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.BaseSite.SysEntity.SysAttrs
{
    public class SysAttrsService : EntityService<sys_attrs>
    {
        #region 构造函数
        public SysAttrsService()
        {
            _cmd = new EntityCommand<sys_attrs>();
        }

        public SysAttrsService(IPersistBroker broker)
        {
            _cmd = new EntityCommand<sys_attrs>(broker);
        }
        #endregion


    }
}