using Npgsql;
using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Data
{
    public static class IPersistBrokerBulkCreateOrUpdateExtension
    {
        /// <summary>
        /// 批量创建
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="broker"></param>
        /// <param name="dataList"></param>
        public static void BulkCreate<T>(this IPersistBroker broker, List<T> dataList)
        {
            var client = broker.DbClient;

            if (dataList == null || dataList.Count == 0)
            {
                return;
            }
            var tableName = dataList[0].GetType().Name;
            client.Execute(DialectSql.GetCreateTemporaryTableSql(tableName, out var tempName));

            var dt = client.Query($"select * from {tempName}");


            var commandFormat = string.Format(CultureInfo.InvariantCulture, "COPY {0} FROM STDIN BINARY", tempName);
            using (var writer = (client.DbConnection as NpgsqlConnection).BeginBinaryImport(commandFormat))
            {
                foreach (DataRow item in dataList.ToDataTable(dt.Columns).Rows)
                    writer.WriteRow(item.ItemArray);
            }

            var sql = string.Format("INSERT INTO {0} SELECT * FROM {1} WHERE NOT EXISTS(SELECT 1 FROM {0} WHERE {0}.{2}id = {1}.{2}id)", tableName, tempName, tempName);
            client.Execute(sql);

            client.Execute($"DROP TABLE IF EXISTS {tempName}");
        }

        /// <summary>
        /// 批量创建或更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="broker"></param>
        /// <param name="dataList"></param>
        public static void BulkCreateOrUpdate<T>(this IPersistBroker broker, IEnumerable<T> dataList)
            where T : BaseEntity, new()
        {
            broker.ExecuteTransaction(() =>
            {
                dataList.Each(item => broker.Save(item));
            });
        }
    }
}
