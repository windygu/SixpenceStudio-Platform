using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiThrottle;

namespace SixpenceStudio.Platform.Startup
{
    public static class WebApiConfig
    {
        public static void Register(IAppBuilder app, HttpConfiguration config)
        {
            //config.MessageHandlers.Add(new ThrottlingHandler
            //{
            //    Policy = ThrottlePolicy.FromStore(new PolicyConfigurationProvider()),
            //    Repository = new CacheRepository()
            //});

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            app.UseWebApi(config);

        }
    }
}
