using Owin;
using Quartz;
using SixpenceStudio.Core.Auth;
using SixpenceStudio.Core.Auth.SysRole;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Extensions;
using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.Job;
using SixpenceStudio.Core.Logging;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            #region 初始化角色
            var broker = PersistBrokerFactory.GetPersistBroker();
            var user = UserIdentityUtil.GetAdmin();
            foreach (var item in Enum.GetValues(typeof(SystemRole)))
            {
                var roleName = item.ToString();
                var role = broker.Retrieve<sys_role>("select * from sys_role where name = @name", new Dictionary<string, object>() { { "@name", roleName } });
                if (role == null)
                {
                    role = new sys_role()
                    {
                        Id = Guid.NewGuid().ToString(),
                        name = roleName,
                        createdBy = user.Id,
                        createdByName = user.Name,
                        createdOn = DateTime.Now,
                        modifiedBy = user.Id,
                        modifiedByName = user.Name,
                        modifiedOn = DateTime.Now,
                        description = (item as Enum).GetDescription()
                    };
                    new SysRoleService(broker).CreateData(role);
                }
                MemoryCacheUtil.Set(roleName, role, 3600 * 12);
            }
            #endregion
        }
    }
}
