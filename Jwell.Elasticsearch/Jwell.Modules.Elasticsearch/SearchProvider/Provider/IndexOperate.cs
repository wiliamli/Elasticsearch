using Jwell.Modules.Cache;
using Jwell.Modules.Elasticsearch.Entity;
using Jwell.Modules.Elasticsearch.Helper;
using Jwell.Modules.Elasticsearch.SearchProvider.Client;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Elasticsearch.SearchProvider.Provider
{
    internal class IndexOperate 
    {
        private ElasticClient Client { get; set; }

        private ICacheClient CacheClient { get; set; }

        public IndexOperate(ElasticClient client,ICacheClient cacheClient)
        {
            this.Client = client;
            this.CacheClient = cacheClient;
        }
        public void CreateIndex<T>(string indexName) where T : SearchEntityBase
        {
            var response = this.Client.CreateIndex(indexName, i => i
                .Settings(setting => setting
                    .Analysis(analysis => analysis
                        .Analyzers(analyzers => analyzers
                            .Custom("ik", custom => custom
                                 .CharFilters("html_strip")
                                 .Tokenizer("ik_max_word")
                                 .Filters("lowercase")
                            )
                        )
                    )
                )
                .Mappings(mapping => mapping
                    .Map<T>(m => m
                        .AutoMap()
                        )
                    )
                );
            if (!response.IsValid)
                throw new ElasticsearchException("创建Index失败：" + response.OriginalException.Message);
        }
       
        public void CkeckIndexStatus<T>(IndexOperateStatus status,string indexName) where T : SearchEntityBase
        {
            if (status == IndexOperateStatus.Started)
            {
                if (!this.Client.IndexExists(indexName).Exists)
                    this.CreateIndex<T>(indexName);
                this.CacheClient.SetCache(indexName, IndexOperateStatus.Starting, 173000);
            }

            if (!(status == IndexOperateStatus.Started || status == IndexOperateStatus.Starting))
                throw new ElasticsearchException("该索引不可用");
        }
    }
}
