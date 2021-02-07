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
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="broker"></param>
        /// <param name="dataList"></param>
        public static void BulkCreate<TEntity>(this IPersistBroker broker, List<TEntity> dataList) where TEntity : BaseEntity, new()
        {
            var client = broker.DbClient;

            if (dataList.IsEmpty()) return;

            var tableName = dataList[0].EntityName;
            var dt = client.Query($"select * from {tableName}");
            BulkCreate(broker, dataList.ToDataTable(dt.Columns), tableName);
        }

        /// <summary>
        /// 批量创建数据
        /// </summary>
        /// <param name="broker"></param>
        /// <param name="dataTable"></param>
        /// <param name="tableName"></param>
        public static void BulkCreate(this IPersistBroker broker, DataTable dataTable, string tableName)
        {
            var client = broker.DbClient;

            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                return;
            }
            var tempName = client.CreateTemporaryTable(tableName);

            var commandFormat = string.Format(CultureInfo.InvariantCulture, "COPY {0} FROM STDIN BINARY", tempName);
            using (var writer = (client.DbConnection as NpgsqlConnection).BeginBinaryImport(commandFormat))
            {
                foreach (DataRow item in dataTable.Rows)
                    writer.WriteRow(item.ItemArray);
            }

            var sql = string.Format("INSERT INTO {0} SELECT * FROM {1} WHERE NOT EXISTS(SELECT 1 FROM {0} WHERE {0}.{2}id = {1}.{2}id)", tableName, tempName, tableName);
            client.Execute(sql);

            client.DropTable(tempName);
        }

        /// <summary>
        /// 批量创建或更新
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="broker"></param>
        /// <param name="dataList"></param>
        public static void BulkCreateOrUpdate<TEntity>(this IPersistBroker broker, IEnumerable<TEntity> dataList) where TEntity : BaseEntity, new()
        {
            broker.ExecuteTransaction(() =>
            {
                dataList.Each(item => broker.Save(item));
            });
        }
    }
}
