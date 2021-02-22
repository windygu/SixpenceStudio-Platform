using Owin;
using SixpenceStudio.Core.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Startup
{
    [UnityRegister]
    public interface IStartup
    {
        void Configuration(IAppBuilder app);
    }
}
