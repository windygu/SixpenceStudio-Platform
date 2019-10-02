using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Platform.Core.Utils
{
    /// <summary>
    /// DataTable 工具类
    /// </summary>
    public static class DataTableUtil
    {
        /// <summary>
        /// 将List<T>转换为DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T> (List<T> entitys)
        {
            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                throw new CSException("3EF84AD1-97F8-463F-BA2F-F6BE115AF969", "需转换的集合为空");
            }
            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable();
            for (int i = 0; i < entityProperties.Length; i++)
            {
                //dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
                dt.Columns.Add(entityProperties[i].Name);
            }
            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new CSException("67438035-E4B1-4784-8AAA-A23BF29F8B65", "要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);
                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }

        static Func<T, object[]> GetGetDelegate<T>(PropertyInfo[] ps)
        {
            var param_obj = Expression.Parameter(typeof(T), "obj");
            Expression newArrayExpression = Expression.NewArrayInit(typeof(object), ps.Select(p => Expression.Property(param_obj, p)));
            return Expression.Lambda<Func<T, object[]>>(newArrayExpression, param_obj).Compile();
        }

        public static DataTable ToTable<T>(this IEnumerable<T> collection)
        {
            var props = typeof(T).GetProperties();
            var func = GetGetDelegate<T>(props);
            var dt = new DataTable();
            dt.Columns.AddRange(
                props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray()
            );
            collection.ToList().ForEach(i => dt.Rows.Add(func(i)));

            return dt;
        }

    }
}
