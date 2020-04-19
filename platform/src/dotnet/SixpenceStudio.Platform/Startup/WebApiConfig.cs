using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;
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

            RouteTable.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            ).RouteHandler = new SessionControllerRouteHandler();

            app.UseWebApi(config);

        }
    }

    public class SessionRouteHandler : HttpControllerHandler, IRequiresSessionState
    {
        public SessionRouteHandler(RouteData routeData) : base(routeData) { }
    }

    public class SessionControllerRouteHandler : HttpControllerRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new SessionRouteHandler(requestContext.RouteData);
        }
    }
}
