using SixpenceStudio.Core.Auth.SysRole.BasicRole;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Auth.SysRole
{
    public class SysRolePlugin : IPersistBrokerPlugin
    {
        public void Execute(PluginContext context)
        {
            if (context.EntityName != "sys_role") return;

            var obj = context.Entity as sys_role;
            switch (context.Action)
            {
                case EntityAction.PostCreate:
                    {
                        AssertUtil.CheckBoolean<SpException>(obj.is_basic, "禁止添加基础角色", "D283AEBF-60CA-4DFF-B08D-6D3DD10AFBBA");
                    }
                    break;
                case EntityAction.PostUpdate:
                    {
                        AssertUtil.CheckBoolean<SpException>(obj.is_basic, "禁止更新基础角色", "D283AEBF-60CA-4DFF-B08D-6D3DD10AFBBA");
                    }
                    break;
                case EntityAction.PostDelete:
                    {
                        AssertUtil.CheckBoolean<SpException>(obj.is_basic, "禁止删除基础角色", "D283AEBF-60CA-4DFF-B08D-6D3DD10AFBBA");
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
