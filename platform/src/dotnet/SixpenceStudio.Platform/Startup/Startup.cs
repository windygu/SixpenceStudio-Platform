using Microsoft.Owin;
using Owin;
using SixpenceStudio.Platform.Startup;
using System.Web.Http;
using Microsoft.Owin.Cors;

[assembly: OwinStartup("ProductionConfiguration", typeof(SixpenceStudio.Platform.Startup.Startup))]
namespace SixpenceStudio.Platform.Startup
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            app.UseCors(CorsOptions.AllowAll);
            WebApiConfig.Register(app, config);

        }
    }
}
