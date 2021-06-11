using Microsoft.Owin.Hosting;
using SixpenceStudio.Core;
using System;

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
                //打印请求结果
                Console.WriteLine("Http服务初始化成功！");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Http服务初始化失败！");
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
