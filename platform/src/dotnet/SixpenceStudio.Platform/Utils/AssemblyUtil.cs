using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SixpenceStudio.Platform.Utils
{
    /// <summary>
    /// 反射帮助类（SixpenceStudio*.dll）
    /// </summary>
    public class AssemblyUtil
    {
        private const string SIXPENCE_LIBS = "SixpenceStudio.*.dll";
        private static readonly IList<Assembly> assemblies;

        static AssemblyUtil()
        {
            assemblies = GetAssemblies(SIXPENCE_LIBS);
        }

        /// <summary>
        /// 获取所有Assembly实例
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IList<Assembly> GetAssemblies(string name = "")
        {
            if (string.IsNullOrEmpty(name)) return new List<Assembly>() { Assembly.GetCallingAssembly() };

            var assemblyList = FileUtil.GetFileList(name)?.Select(item => Assembly.LoadFile(item))?.ToList();
            return assemblyList;
        }

        /// <summary>
        /// 获取所有程序集中继承对应接口的Type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<Type> GetTypes<T>(string name = SIXPENCE_LIBS)
        {
            var list = (string.IsNullOrEmpty(name) || name.Equals(SIXPENCE_LIBS, StringComparison.OrdinalIgnoreCase) ? assemblies : GetAssemblies(name))
                .SelectMany(assembly => assembly.GetTypes().Where(item => !item.IsInterface && item.GetInterfaces().Contains(typeof(T))))
                .ToList();

            return list;
        }

        /// <summary>
        /// 执行所有继承该接口的实例的方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodName">方法名</param>
        /// <param name="param">方法参数</param>
        public static void Execute<T>(string methodName, object[] param, string className = "")
        {
            var types = GetTypes<T>();
            className = className.Replace("_", "");
            foreach (var item in types)
            {
                // 筛选类名（TestPlugin)
                if (!string.IsNullOrEmpty(className) && item.Name.Contains(className, StringComparison.OrdinalIgnoreCase))
                {
                    var obj = Activator.CreateInstance(item);
                    var mi = item.GetMethod(methodName);
                    mi.Invoke(obj, param);
                }
            }
        }

        /// <summary>
        /// 获取继承该接口的特例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetObject<T>(string name)
            where T : class
        {
            var types = GetTypes<T>();
            var type = types.Where(item => item.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (type == null)
            {
                return null;
            }
            var obj = (T)Activator.CreateInstance(type);
            return obj;
        }

    }
}
