using SixpenceStudio.Platform.Command;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Entity;
using SixpenceStudio.Platform.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.BaseSite.SysParamGroup
{
    public class SysParamGroupService : EntityService<sys_paramgroup>
    {
        #region 构造函数
        public SysParamGroupService()
        {
            this._cmd = new EntityCommand<sys_paramgroup>();
        }
        public SysParamGroupService(IPersistBroker broker)
        {
            this._cmd = new EntityCommand<sys_paramgroup>(broker);
        }
        #endregion

        public IList<SelectModel> GetParams(string code)
        {
            var sql = @"
SELECT 
	sys_param.code AS Value,
	sys_param.name AS Name
FROM sys_param
INNER JOIN sys_paramgroup ON sys_param.sys_paramgroupid = sys_paramgroup.sys_paramgroupid
WHERE sys_paramgroup.code = @code
";
            return _cmd.broker.DbClient.Query<SelectModel>(sql, new Dictionary<string, object>() { { "@code", code } }).ToList();
        }
    }
}