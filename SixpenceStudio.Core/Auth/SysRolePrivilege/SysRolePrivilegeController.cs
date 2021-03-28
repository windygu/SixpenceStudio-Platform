using Newtonsoft.Json;
using SixpenceStudio.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SixpenceStudio.Core.Auth.SysRolePrivilege
{
    public class SysRolePrivilegeController : EntityBaseController<sys_role_privilege, SysRolePrivilegeService>
    {
        [HttpGet]
        public IEnumerable<sys_role_privilege> GetUserPrivileges(string roleid)
        {
            return new SysRolePrivilegeService().GetUserPrivileges(roleid);
        }

        [HttpPost]
        public void BulkSave([FromBody]string dataList)
        {
            var privileges = string.IsNullOrEmpty(dataList) ? null : JsonConvert.DeserializeObject<List<sys_role_privilege>>(dataList);
            new SysRolePrivilegeService().BulkSave(privileges);
        }
    }
}
