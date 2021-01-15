using Owin;
using Quartz;
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
            var logger = LogFactory.GetLogger("startup");
            var typeList = new List<Type>();
            AssemblyUtil.GetAssemblies("SixpenceStudio.*.dll").ForEach(item => typeList.AddRange(item.GetTypes()));

            #region Job注册
            var jobTypeList = typeList.Where(type => !type.IsAbstract && !type.IsInterface && type.GetInterfaces().Contains(typeof(IJob)));
            logger.Info($"共发现{jobTypeList.Count()}个Job待注册");
            jobTypeList.Each(type =>
            {
                UnityContainerService.RegisterType(typeof(IJob), type, type.Name);
                logger.Info($"注册{type.Name}成功");
            });
            logger.Info($"注册成功，共注册{jobTypeList.Count()}个");
            JobHelpers.Start();
            #endregion
        }
    }
}
