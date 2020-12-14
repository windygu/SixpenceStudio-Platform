﻿using log4net.Config;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using Quartz;
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
            var file = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\log4net.config");
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
                types.ForEach(type => UnityContainerService.RegisterType(item, type, type.Name));
            });
            #endregion

            #region Job注册
            var jobTypeList = typeList.Where(type => !type.IsAbstract && !type.IsInterface && type.GetInterfaces().Contains(typeof(IJob)));
            logger.Info($"共发现{jobTypeList.Count()}个Job待注册");
            jobTypeList.Each(type =>
            {
                UnityContainerService.RegisterType(typeof(IJob), type, type.Name);
                logger.Info($"注册{type.Name}成功");
            });
            logger.Info($"注册成功，共注册{jobTypeList.Count()}个");
            JobHelpers.Register(logger);
            #endregion
        }
    }
}
