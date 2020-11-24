using log4net.Config;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using Quartz;
using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.Job;
using SixpenceStudio.Core.Logging;
using SixpenceStudio.Core.Startup;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;

[assembly: OwinStartup("ProductionConfiguration", typeof(Startup))]
[assembly: XmlConfigurator(ConfigFile = @"log4net.config", Watch = true)]
namespace SixpenceStudio.Core.Startup
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 加载log配置
            var file = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\log4net.config");
            var repository = log4net.LogManager.CreateRepository("NETFrameworkRepository");
            Quartz.Logging.LogProvider.IsDisabled = true;
            XmlConfigurator.Configure(repository, file);
            XmlConfigurator.ConfigureAndWatch(file);

            var config = new HttpConfiguration();

            app.UseCors(CorsOptions.AllowAll);

            var log = LogFactory.GetLogger("startup");

            WebApiConfig.Register(app, config);
            log.Info("Api注册成功");

            AssemblyUtil.GetAssemblies().Each(item =>
            {
                var types = item.GetTypes().ToList();
                UnityContainerService.Register(types);
                var toBeRegisterList = types.Where(type => !type.IsAbstract && !type.IsInterface && type.GetInterfaces().Contains(typeof(IJob)));
                UnityContainerService.Register<IJob>(types);
            });
            JobHelpers.Register(log);
        }
    }
}
