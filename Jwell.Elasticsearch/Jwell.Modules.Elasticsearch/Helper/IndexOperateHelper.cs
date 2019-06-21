using Jwell.Modules.Elasticsearch.Entity;
using Jwell.Modules.Elasticsearch.SearchProvider.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Elasticsearch.Helper
{
    public class IndexOperateHelper
    {
        public static void RemoveIndex(string indexName)
        {
            var response = ElasticsearchClient.Instance.DeleteIndex(indexName);
            if (!response.IsValid)
                throw new ElasticsearchException("删除index失败：" + response.OriginalException.Message);
        }
    }
}
