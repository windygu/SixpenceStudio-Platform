#region 类文件描述
/*********************************************************
Copyright @ Sixpence Studio All rights reserved. 
Author   : Karl Du
Created: 2020/11/23 20:02:47
Description：UnityContainerService
********************************************************/
#endregion

using SixpenceStudio.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace SixpenceStudio.Core.IoC
{
    public static class UnityContainerService
    {
        private static UnityContainer container = new UnityContainer();
        public static UnityContainer Container
        {
            get
            {
                return container;
            }
        }

        /// <summary>
        /// 注册实例
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="name"></param>
        public static void RegisterType(Type from , Type to, string name = "")
        {
            var logger = LogFactory.GetLogger("startup");
            container.RegisterType(from, to, name);
            logger.Info($"{to.Name}注册成功");
        }

        /// <summary>
        /// 注册接口
        /// </summary>
        public static void Register()
        {
            Register(Assembly.GetCallingAssembly().GetTypes().ToList());
        }

        /// <summary>
        /// 注册接口
        /// </summary>
        public static void Register(List<Type> typeList)
        {
            if (typeList == null || typeList.Count == 0)
            {
                return;
            }

            var logger = LogFactory.GetLogger("startup");
            foreach (var item in typeList)
            {
                if (item.IsInterface && (item.IsDefined(typeof(CustomStrategyAttribute))))
                {
                    typeList.ForEach(type =>
                    {
                        if (!type.IsInterface && !type.IsAbstract && type.GetInterfaces().Contains(item))
                        {
                            container.RegisterType(item, type, type.Name);
                            logger.Info($"{type.Name}注册成功");
                        }
                    });
                }
            }
        }

        /// <summary>
        /// 查找实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T Resolve<T>(string name = "")
        {
            return container.Resolve<T>(name);
        }

        /// <summary>
        /// 查找所有实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ResolveAll<T>()
        {
            return container.ResolveAll<T>();
        }

        /// <summary>
        /// 查找所有实现
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<Object> ResolveAll(Type type)
        {
            return container.ResolveAll(type);
        }
    }
}
