using Jwell.Modules.Elasticsearch.SearchProvider.Client.Setting;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Elasticsearch.SearchProvider.Client
{
    /// <summary>
    /// Elasticsearch客户端
    /// </summary>
    public class ElasticsearchClient
    {
        private static  ElasticClient client;

        private static readonly object locker = new object();

        private ElasticsearchClient()
        { }

        /// <summary>
        /// Elasticsearch客户端实例
        /// </summary>
        //public static ElasticClient Instance { get; } =
        //    new ElasticClient(ElasticsearchSetting.CreateInstance());
        public static ElasticClient Instance
        {
            get {
                if (client == null)
                {
                    lock (locker)
                    {
                        if(client==null)
                            client = new ElasticClient(ElasticsearchSetting.CreateInstance());
                    }
                }
                return client;
            }
        }
    }
}
