using log4net.Config;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using SixpenceStudio.Platform.Startup;
using System.IO;
using System.Web.Http;

[assembly: OwinStartup("ProductionConfiguration", typeof(Startup))]
[assembly: XmlConfigurator(ConfigFile = @"log4net.config", Watch = true)]
namespace SixpenceStudio.Platform.Startup
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 加载log配置
            var file = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + @"\bin\Configs\log4net.config");
            var repository = log4net.LogManager.CreateRepository("NETFrameworkRepository");
            XmlConfigurator.Configure(repository, file);
            XmlConfigurator.ConfigureAndWatch(file);

            var log = log4net.LogManager.GetLogger("Test");
            log.Debug("123");

            var config = new HttpConfiguration();

            app.UseCors(CorsOptions.AllowAll);

            WebApiConfig.Register(app, config);
        }
    }
}
