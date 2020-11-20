using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform
{
    public static class IEnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> ts)
        {
            return ts == null || ts.Count() == 0;
        }

        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> ts)
        {
            return !ts.IsNullOrEmpty();
        }

        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public static DataTable ToDataTable<T>(this IList<T> data, DataColumnCollection columns)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (DataColumn item in columns)
            {
                var prop = properties.Find(item.ColumnName, true);
                if (prop?.PropertyType == typeof(JToken))
                {
                    table.Columns.Add(new DataColumn(item.ColumnName, typeof(JToken)));
                }
                else
                {
                    table.Columns.Add(new DataColumn(item.ColumnName, item.DataType));
                }
            }

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (DataColumn c in columns)
                {
                    row[c.ColumnName] = DBNull.Value;

                    var prop = properties.Find(c.ColumnName, true);
                    var propValue = prop?.GetValue(item);
                    if (propValue != null)
                    {
                        if (propValue is bool)
                        {
                            row[c.ColumnName] = (bool)(propValue) ? "1" : "0";
                        }
                        else if (propValue is JToken)
                        {
                            row[c.ColumnName] = (propValue as JToken);
                        }
                        else
                        {
                            row[c.ColumnName] = Convert.ChangeType(propValue, c.DataType);
                        }
                    }
                }
                table.Rows.Add(row);
            }
            return table;
        }
        /// <summary>
        /// 针对IEnumerable的各项进行处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        /// <param name="callback"></param>
        public static void Each<T>(this IEnumerable<T> src, Action<T> callback)
        {
            if (src == null) return;
            foreach (var item in src)
            {
                callback(item);
            }
        }
    }
}
