using Microsoft.Owin.Cors;
using Owin;
using SixpenceStudio.Core.WebApi.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SixpenceStudio.Core.Startup
{
    public class WebApiStartup
    {
        public static void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);

            HttpConfiguration config = new HttpConfiguration();

            // Web API 路由
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new WebApiTrackerAttribute());
            config.Filters.Add(new WebApiExceptionFilterAttribute());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseWebApi(config);
        }
    }
}
