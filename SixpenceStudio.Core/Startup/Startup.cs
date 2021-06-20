using log4net.Config;
using Microsoft.Owin;
using Owin;
using SixpenceStudio.Core.Auth;
using SixpenceStudio.Core.Extensions;
using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.Logging;
using SixpenceStudio.Core.Startup;
using System;
using System.IO;
using System.Linq;

[assembly: OwinStartup("ProductionConfiguration", typeof(Startup))]
[assembly: XmlConfigurator(ConfigFile = @"log4net.config", Watch = true)]
namespace SixpenceStudio.Core.Startup
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 加载log配置
            var file = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "log4net.config"));
            var repository = log4net.LogManager.CreateRepository("NETFrameworkRepository");
            Quartz.Logging.LogProvider.IsDisabled = true;
            XmlConfigurator.Configure(repository, file);
            XmlConfigurator.ConfigureAndWatch(file);

            var logger = LogFactory.GetLogger("startup");
            logger.Info("系统准备启动...");

            WebApiStartup.Configuration(app);
            logger.Info("1. Api注册成功");

            IoCStartup.Configuration(out var typeList);
            logger.Info("2. IoC注册成功");

            UserIdentityUtil.SetCurrentUser(UserIdentityUtil.GetAdmin());

            EntityStartup.Configuration();
            logger.Info("3. 实体注册成功");

            JobStartup.Configuration(typeList);
            logger.Info("4. Job注册成功");

            RoleStartup.Configuration();
            logger.Info("5. Role注册成功");

            // Extension：顺序执行项目的启动类
            UnityContainerService.ResolveAll<IStartup>()
                .OrderBy(item => item.OrderIndex)
                .Each(item => item.Configuration(app));
            logger.Info("系统启动成功");
        }
    }
}
