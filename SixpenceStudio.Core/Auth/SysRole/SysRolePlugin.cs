using SixpenceStudio.Core.Auth.SysRole.BasicRole;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.Extensions;
using SixpenceStudio.Core.IoC;
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
                        var providers = UnityContainerService.ResolveAll<IEntityInitialDataProvider>(e => e.Replace("InitialDataProvider", "").ToLower().Equals(obj.GetEntityName().Replace("_", "").ToLower()));
                        if (!providers.IsEmpty())
                        {
                            var basicRoleList = new List<string>();
                            providers.Each(item => basicRoleList.AddRange(item.GetInitialData().Select(e => e.Id)));
                            AssertUtil.CheckBoolean<SpException>(obj.is_basic && !basicRoleList.Contains(obj.Id), "禁止添加基础角色", "D283AEBF-60CA-4DFF-B08D-6D3DD10AFBBA");
                        }
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
