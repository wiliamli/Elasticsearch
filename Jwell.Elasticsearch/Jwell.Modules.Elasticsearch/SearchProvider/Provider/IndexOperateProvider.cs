using Elasticsearch.Net;
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
    /// <summary>
    /// 索引操作
    /// </summary>
    public class IndexOperateProvider : IIndexOperateProvider
    {
        private ElasticClient Client;

        private ICacheClient CacheClient { get; set; }

        private IndexOperate IndexOperate { get; set; }
             
        public IndexOperateProvider(ICacheClient cacheClient)
        {
            this.CacheClient = cacheClient;
            this.Client = ElasticsearchClient.Instance;
            this.IndexOperate = new IndexOperate(this.Client, this.CacheClient);
        }

        /// <summary>
        /// 批量添加数据到索引
        /// </summary>
        /// <typeparam name="T">实体模型类型</typeparam>
        /// <param name="index">索引名称</param>
        /// <param name="entites">添加的实体模型</param>
        public bool BulkIndex<T>(string indexName, params T[] entites) where T : SearchEntityBase
        {
            var indexMappingName = IndexNameHelper.GetIndexMappingName(SetupConfig.SetupConfig.ServiceSign, indexName);
            var status = this.CacheClient.GetCache<IndexOperateStatus>(indexMappingName);

            this.IndexOperate.CkeckIndexStatus<T>(status, indexMappingName);

            var sizeKey = IndexNameHelper.GetIndexSizeName(SetupConfig.SetupConfig.ServiceSign, indexName);
            var indexSize = this.CacheClient.GetCache<int>(sizeKey);
            if (indexSize == 0) return false;
            
            var catResponse = this.Client.CatIndices(index => index.Index(indexMappingName).Bytes(Bytes.Mb));
            var record = catResponse.Records.FirstOrDefault();

            if (record != null)
            {
                long storeSize;
                if (long.TryParse(record.StoreSize, out storeSize))
                {
                    if(storeSize>=indexSize)
                        throw new ElasticsearchException("该索引存储空间已满");

                    var response = this.Client.IndexMany(entites, indexMappingName);
                    return response.IsValid;
                }
            }
            return false;
        }
    }
}
