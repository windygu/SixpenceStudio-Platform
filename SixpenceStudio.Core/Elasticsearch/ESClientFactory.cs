using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Elasticsearch
{
    public static class ESClientFactory
    {
        /// <summary>
        /// 获取ElasticClient实例（单点连接）
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static ElasticClient GetClient(string address)
        {
            var node = new Uri(address);
            var settings = new ConnectionSettings(node);
            var client = new ElasticClient(settings);
            return client;
        }

        /// <summary>
        /// 获取ElasticClient实例（连接池链接）
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static ElasticClient GetClient(string[] address)
        {
            var nodes = new Uri[address.Length];
            for (int i = 0; i < address.Length; i++)
            {
                nodes[i] = new Uri(address[i]);
            }
            var pool = new StaticConnectionPool(nodes);
            var settings = new ConnectionSettings(pool);
            return new ElasticClient(settings);
        }
    }
}
