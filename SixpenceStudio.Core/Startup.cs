using log4net.Config;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using Quartz;
using SixpenceStudio.Core;
using SixpenceStudio.Core.Auth;
using SixpenceStudio.Core.Auth.SysRole.BasicRole;
using SixpenceStudio.Core.Auth.SysRolePrivilege;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.Extensions;
using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.Job;
using SixpenceStudio.Core.Logging;
using SixpenceStudio.Core.SysEntity;
using SixpenceStudio.Core.SysEntity.SysAttrs;
using SixpenceStudio.Core.Utils;
using SixpenceStudio.Core.WebApi.Filter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;

[assembly: OwinStartup("ProductionConfiguration", typeof(Startup))]
[assembly: XmlConfigurator(ConfigFile = @"log4net.config", Watch = true)]
namespace SixpenceStudio.Core
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 加载log配置
            var file = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "log4net.config"));
            var repository = log4net.LogManager.CreateRepository("NETFrameworkRepository");
            Quartz.Logging.LogProvider.IsDisabled = true;
            XmlConfigurator.Configure(repository, file);
            XmlConfigurator.ConfigureAndWatch(file);

            app.UseCors(CorsOptions.AllowAll);

            var logger = LogFactory.GetLogger("startup");

            #region API注册
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
            logger.Info("Api注册成功");
            #endregion

            #region IoC注册
            var typeList = new List<Type>();
            AssemblyUtil.GetAssemblies("SixpenceStudio.*.dll").ForEach(item => typeList.AddRange(item.GetTypes()));
            var interfaces = typeList.Where(item => item.IsInterface && item.IsDefined(typeof(UnityRegisterAttribute), false)).ToList();
            interfaces.ForEach(item =>
            {
                var types = typeList.Where(type => !type.IsInterface && !type.IsAbstract && type.GetInterfaces().Contains(item) && !type.IsDefined(typeof(IgnoreRegisterAttribute), false)).ToList();
                types.ForEach(type => UnityContainerService.Register(item, type, type.Name));
            });
            logger.Info("IoC注册成功");
            #endregion

            UserIdentityUtil.SetCurrentUser(UserIdentityUtil.GetAdmin());

            #region 实体注册
            var broker = PersistBrokerFactory.GetPersistBroker();
            var dialect = broker.DbClient.Dialect;
            var entityList = UnityContainerService.ResolveAll<IEntity>();
            broker.ExecuteTransaction(() =>
            {
                // 创建表和初始化数据
                entityList.Each(item =>
                {
                    var entity = broker.Query(dialect.GetTable(item.GetEntityName()));
                    var attrs = item.GetAttrs();
                    if (entity == null || entity.Rows.Count == 0)
                    {
                        var attrSql =attrs
                            .Select(e =>
                            {
                                return $"{e.Name} {e.Type.GetDescription()}{(e.Length != null ? $"({e.Length.Value})" : "")} {(e.IsRequire.HasValue && e.IsRequire.Value ? "NOT NULL" : "")}{(e.Name == $"{item.GetEntityName()}id" ? " PRIMARY KEY" : "")}";
                            })
                            .Aggregate((a, b) => a + ",\r\n" + b);

                        var sql = $@"
CREATE TABLE public.{item.GetEntityName()} (
{attrSql}
)
";
                        // 创建表
                        broker.Execute(sql);

                        // 初始化表数据
                        var initialData = item.GetInitialData().ToList();
                        if (initialData != null && initialData.Count() > 0)
                        {
                            initialData.ForEach(e => broker.Create(e));
                        }
                    }
                });

                // 创建实体记录和实体字段数据
                entityList.Each(item =>
                {
                    #region 实体添加自动写入记录
                    var entity = broker.Retrieve<sys_entity>("select * from sys_entity where code = @code", new Dictionary<string, object>() { { "@code", item.GetEntityName() } });
                    if (entity == null)
                    {
                        entity = new sys_entity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            name = item.GetLogicalName(),
                            code = item.GetEntityName(),
                            is_sys = item.IsSystemEntity()
                        };
                        broker.Create(entity);
                    }
                    #endregion

                    #region 字段变更自动写入记录（仅支持新增字段）
                    var attrs = item.GetAttrs();
                    var attrsList = new SysEntityService(broker).GetEntityAttrs(entity.Id).Select(e => e.code);
                    attrs.Each(attr =>
                    {
                        if (!attrsList.Contains(attr.Name))
                        {
                            var _attr = new sys_attrs()
                            {
                                Id = Guid.NewGuid().ToString(),
                                name = attr.LogicalName,
                                code = attr.Name,
                                entityid = entity.Id,
                                entityidname = entity.name,
                                attr_type = attr.Type.GetDescription(),
                                attr_length = attr.Length,
                                isrequire = attr.IsRequire.HasValue && attr.IsRequire.Value
                            };
                            broker.Create(_attr);
                        }
                    });
                    #endregion
                });
            });
            #endregion

            #region Job注册
            typeList
    .Where(type => !type.IsAbstract && !type.IsInterface && type.GetInterfaces().Contains(typeof(IJob)) && !type.IsDefined(typeof(DynamicJobAttribute), true))
    .Each(type => UnityContainerService.Register(typeof(IJob), type, type.Name));
            JobHelpers.Start();
            #endregion

            #region Role注册
            UnityContainerService.ResolveAll<IBasicRole>().Each(item => MemoryCacheUtil.Set(item.GetRoleKey, new RolePrivilegeModel() { Role = item.GetRole(), Privileges = item.GetRolePrivilege() }, 3600 * 12));
            #endregion

            // 顺序执行项目的启动类
            UnityContainerService.ResolveAll<IStartup>()
                .OrderBy(item => item.OrderIndex)
                .Each(item => item.Configuration(app));
        }
    }
}
