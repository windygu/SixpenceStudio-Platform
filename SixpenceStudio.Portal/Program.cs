using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SixpenceStudio.Portal
{
    class Program
    {
        static void Main(string[] args)
        {
            ApiInit();
            Console.ReadKey();
        }
        private static void ApiInit()
        {
            try
            {
                //端口号
                string port = "9111";
                //电脑所有ip地址都启用该端口服务
                string baseAddress = "http://localhost:" + port + "/";
                //启动OWIN host 
                WebApp.Start<Startup>(url: baseAddress);
                //打印服务所用端口号
                Console.WriteLine("Http服务端口：" + port);
                //创建HttpCient测试webapi 
                HttpClient client = new HttpClient();
                //通过get请求数据
                var response = client.GetAsync("http://localhost:" + port + "/api/home").Result;
                //打印请求结果
                Console.WriteLine(response);
                Console.WriteLine("Http服务初始化成功！");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Http服务初始化失败！");
                Console.WriteLine(ex.ToString());
            }
        }
    }

    public class Startup
    {
        //需要nuget owin、cors、hosting、listener
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        /// <summary>
        /// 配置webApi文本格式、路由规则、跨域规则等参数
        /// </summary>
        /// <param name="appBuilder"></param>
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));//解决跨域问题，需要nuget Cors
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }

            );
            appBuilder.UseWebApi(config);
        }
    }

    public class HomeController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok("Hello World!");
        }
    }
}
