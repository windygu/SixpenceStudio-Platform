using Quartz;
using SixpenceStudio.Core.Extensions;
using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Startup
{
    public class JobStartup
    {
        public static void Configuration(List<Type> typeList)
        {
            typeList
.Where(type => !type.IsAbstract && !type.IsInterface && type.GetInterfaces().Contains(typeof(IJob)) && !type.IsDefined(typeof(DynamicJobAttribute), true))
.Each(type => UnityContainerService.Register(typeof(IJob), type, type.Name));
            JobHelpers.Start();
        }
    }
}
