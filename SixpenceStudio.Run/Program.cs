using Microsoft.Owin.Hosting;
using SixpenceStudio.Core.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SixpenceStudio.Run
{
    class Program
    {
        static void Main(string[] args)
        {
            ApiInit();
            Console.ReadKey();
        }

        /// <summary>
        /// 初始化webApi
        /// </summary>
        public static void ApiInit()
        {
            try
            {
                //端口号
                string port = "";
                string baseAddress = "http://localhost:9111/";
                //启动OWIN host 
                WebApp.Start<Startup>(url: baseAddress);
                //打印服务所用端口号
                HttpClient client = new HttpClient();
                //通过get请求数据
                var response = client.GetAsync("http://localhost:9111/api/test/test").Result;
                //打印请求结果
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Console.WriteLine("Http服务初始化成功！");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Http服务初始化失败！");
                Console.WriteLine(ex.ToString());
            }
        }


        [Route("api/[controller]/[action]")]
        public class TestController : ApiController
        {
            [HttpGet]
            public string Test()
            {
                return "Hello World！";
            }
        }
    }
}
