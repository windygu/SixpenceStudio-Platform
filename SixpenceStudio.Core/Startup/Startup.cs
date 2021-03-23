using log4net.Config;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using Quartz;
using SixpenceStudio.Core.Auth;
using SixpenceStudio.Core.Extensions;
using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.Job;
using SixpenceStudio.Core.Logging;
using SixpenceStudio.Core.Startup;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            var file = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "log4net.config"));
            var repository = log4net.LogManager.CreateRepository("NETFrameworkRepository");
            Quartz.Logging.LogProvider.IsDisabled = true;
            XmlConfigurator.Configure(repository, file);
            XmlConfigurator.ConfigureAndWatch(file);

            var config = new HttpConfiguration();

            app.UseCors(CorsOptions.AllowAll);

            var logger = LogFactory.GetLogger("startup");

            WebApiConfig.Register(app, config);
            logger.Info("Api注册成功");

            var typeList = new List<Type>();
            AssemblyUtil.GetAssemblies("SixpenceStudio.*.dll").ForEach(item => typeList.AddRange(item.GetTypes()));
            #region IoC注册
            var interfaces = typeList.Where(item => item.IsInterface && item.IsDefined(typeof(UnityRegisterAttribute), false)).ToList();
            interfaces.ForEach(item =>
            {
                var types = typeList.Where(type => !type.IsInterface && !type.IsAbstract && type.GetInterfaces().Contains(item)).ToList();
                types.ForEach(type => UnityContainerService.Register(item, type, type.Name));
            });
            logger.Info("IoC注册成功");
            #endregion

            UserIdentityUtil.SetCurrentUser(UserIdentityUtil.GetAdmin());
            // 调用所有的启动类
            UnityContainerService.ResolveAll<IStartup>().Each(item => item.Configuration(app));
        }
    }
}
