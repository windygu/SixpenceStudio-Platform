using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.SysParamGroup
{
    public class SysParamGroupInitialDataProvider : IEntityInitialDataProvider
    {
        public string EntityName => "sys_paramgroup";

        public IEnumerable<BaseEntity> GetInitialData()
        {
            return new List<sys_paramgroup>()
            {
                new sys_paramgroup() { Id = "E7D80743-081D-4B51-BFFF-149FFFF8E652", name = "任务状态", code = "job_state" },
                new sys_paramgroup() { Id = "E944E20B-A463-4FE3-B2E6-ADE32C0709F3", name = "操作类型", code = "operation_type" },
                new sys_paramgroup() { Id = "CE9753B3-E86F-4DF9-A63D-8B46AD8A187C", name = "权限", code = "privilege" }
            };
        }
    }
}
