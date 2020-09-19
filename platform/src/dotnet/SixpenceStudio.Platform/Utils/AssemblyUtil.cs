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
        private const string SIXPENCE_LIBS = "SixpenceStudio*.dll";

        /// <summary>
        /// 获取所有Assembly实例
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IList<Assembly> GetAssemblies(string name)
        {
            var fileList = FileUtil.GetFileList(name);

            var assemblyList = new List<Assembly>();
            fileList.ToList().ForEach(item =>
            {
                assemblyList.Add(Assembly.LoadFile(item));
            });
            return assemblyList;
        }

        /// <summary>
        /// 获取所有程序集中继承对应接口的Type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<Type> GetTypes<T>(string name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                name = SIXPENCE_LIBS;
            }

            var assmeblies = GetAssemblies(name);
            var list = new List<Type>();
            assmeblies.ToList().ForEach(assembly =>
            {
                foreach (var item in assembly.GetTypes())
                {
                    if (item.IsInterface) continue; // 判断是否是接口
                    var ins = item.GetInterfaces();
                    foreach (var ty in ins)
                    {
                        if (ty == typeof(T))
                        {
                            list.Add(item);
                        }
                    }
                }
            });

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
            var types = GetTypes<T>("SixpenceStudio*.dll");
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

    }
}
