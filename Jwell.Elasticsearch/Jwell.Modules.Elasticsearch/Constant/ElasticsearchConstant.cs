using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Elasticsearch.Constant
{
    /// <summary>
    /// Elasticsearch参数
    /// </summary>
    internal class ElasticsearchConstant
    {
        /// <summary>
        /// 集群Uri集合
        /// </summary>
        public List<string> Uris { get; } = new List<string> { "http://10.130.0.253:9200" };

        /// <summary>
        /// 默认索引
        /// </summary>
        public string DefaultIndex { get; } = "content";
    }
}
