using Jwell.Framework.Application.Service;
using Jwell.Modules.Cache;
using Jwell.Modules.Elasticsearch.Helper;
using Jwell.Repository.Repositories;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services
{
    /// <summary>
    /// 缓存数据同步-服务
    /// </summary>
    public class SyncCacheDataService : ApplicationService, ISyncCacheDataService
    {
        private IIndexManagerRepository IndexManagerRepository { get; set; }

        private ICacheClient CacheClient { get; set; }

        public SyncCacheDataService(IIndexManagerRepository indexManagerRepository, ICacheClient cacheClient)
        {
            this.IndexManagerRepository = indexManagerRepository;
            this.CacheClient = cacheClient;
        }
        public void SyncCacheData(int retryTime = 2)
        {
            Policy.Handle<Exception>().Retry(retryTime).Execute(() => TrySyncData());
        }

        private void TrySyncData()
        {
            var query = this.IndexManagerRepository.Queryable().Where(x => x.IsDeleted == false).Select(x => new
            {
                ServiceSign = x.ServiceSign,
                IndexName = x.IndexName,
                Status = x.Status,
                Size = x.Size,
                ExpensionNum =x.ExpensionNum
            }).ToList();
            foreach (var indexData in query)
            {
                var indexMappingName = IndexNameHelper.GetIndexMappingName(indexData.ServiceSign, indexData.IndexName);
                this.CacheClient.SetCache(indexMappingName, indexData.Status, 173000);
                var indexMappingNameForSize = IndexNameHelper.GetIndexSizeName(indexData.ServiceSign, indexData.IndexName);
                this.CacheClient.SetCache(indexMappingNameForSize, indexData.Size * (indexData.ExpensionNum + 1), 173000);
            }
        }
    }
}
