using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Startup
{
    public class IoCStartup
    {
        public static void Configuration(out List<Type> typeList)
        {
            var types = new List<Type>();
            AssemblyUtil.GetAssemblies("SixpenceStudio.*.dll").ForEach(item => types.AddRange(item.GetTypes()));
            var interfaces = types.Where(item => item.IsInterface && item.IsDefined(typeof(UnityRegisterAttribute), false)).ToList();
            interfaces.ForEach(item =>
            {
                var _types = types.Where(type => !type.IsInterface && !type.IsAbstract && type.GetInterfaces().Contains(item) && !type.IsDefined(typeof(IgnoreRegisterAttribute), false)).ToList();
                _types.ForEach(type => UnityContainerService.Register(item, type, type.Name));
            });
            typeList = types;
        }
    }
}
