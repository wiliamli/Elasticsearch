using Jwell.Framework.Ioc;
using Jwell.Modules.Elasticsearch.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Elasticsearch.SearchProvider
{
    /// <summary>
    /// 索引操作
    /// </summary>
    [Singleton]
    public interface IIndexOperateProvider
    {
        /// <summary>
        /// 批量添加数据到索引
        /// </summary>
        /// <typeparam name="T">实体模型类型</typeparam>
        /// <param name="index">索引名称</param>
        /// <param name="entites">添加的实体模型</param>
        bool BulkIndex<T>(string indexName, params T[] entites) where T : SearchEntityBase;
    }
}
