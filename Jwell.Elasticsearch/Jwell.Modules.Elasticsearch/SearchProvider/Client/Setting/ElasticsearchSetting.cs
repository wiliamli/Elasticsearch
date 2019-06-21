using Elasticsearch.Net;
using Jwell.Modules.Elasticsearch.Constant;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Elasticsearch.SearchProvider.Client.Setting
{
    /// <summary>
    /// Elasticsearch客户端设置
    /// </summary>
    public class ElasticsearchSetting
    {
        /// <summary>
        /// Elasticsearch参数
        /// </summary>
        private static ElasticsearchConstant Constant { get; } = new ElasticsearchConstant();
        private static Uri[] Nodes => Constant.Uris.Select(x => new Uri(x)).ToArray();

        /// <summary>
        /// 创建连接设置实例
        /// </summary>
        /// <returns></returns>
        public static ConnectionSettings CreateInstance()
        {
            var connectPool = new StaticConnectionPool(Nodes);
            return new ConnectionSettings(connectPool)
                .DefaultIndex(Constant.DefaultIndex)
                .RequestTimeout(TimeSpan.FromMinutes(2))
                .EnableHttpCompression()
                .PrettyJson();
        }
    }
}
