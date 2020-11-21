using log4net.Config;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using SixpenceStudio.Core.Logging;
using SixpenceStudio.Core.Startup;
using System.IO;
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
            var file = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + @"\log4net.config");
            var repository = log4net.LogManager.CreateRepository("NETFrameworkRepository");
            Quartz.Logging.LogProvider.IsDisabled = true;
            XmlConfigurator.Configure(repository, file);
            XmlConfigurator.ConfigureAndWatch(file);

            var config = new HttpConfiguration();

            app.UseCors(CorsOptions.AllowAll);

            var log = LogFactory.GetLogger("startup");

            log.Info("正在注册Controller");
            WebApiConfig.Register(app, config);
            log.Info("注册API成功");

            log.Info("正在注册Job");
            Job.JobHelpers.Register(log);
            log.Info("注册Job成功");
        }
    }
}
