using Owin;
using Quartz;
using SixpenceStudio.Core.Auth.SysRole.BasicRole;
using SixpenceStudio.Core.Auth.SysRolePrivilege;
using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.Job;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SixpenceStudio.Core.Startup
{
    public class CoreStartup : IStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var typeList = new List<Type>();
            AssemblyUtil.GetAssemblies("SixpenceStudio.*.dll").ForEach(item => typeList.AddRange(item.GetTypes()));
            typeList
                .Where(type => !type.IsAbstract && !type.IsInterface && type.GetInterfaces().Contains(typeof(IJob)) && !type.IsDefined(typeof(DynamicJobAttribute), true))
                .Each(type => UnityContainerService.Register(typeof(IJob), type, type.Name));
            JobHelpers.Start();

            // 注册基本角色和权限
            UnityContainerService.ResolveAll<IBasicRole>().Each(item => MemoryCacheUtil.Set(item.GetRoleKey, new RolePrivilegeModel() { Role = item.GetRole(), Privileges = item.GetRolePrivilege() }, 3600 * 12));
        }
    }
}
