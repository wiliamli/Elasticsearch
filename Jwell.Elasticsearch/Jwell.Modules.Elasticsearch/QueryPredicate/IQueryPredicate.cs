using Jwell.Modules.Elasticsearch.Entity;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Elasticsearch.QueryPredicate
{
    /// <summary>
    /// 查询谓语基类接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQueryPredicate<T> where T : SearchEntityBase
    {
        /// <summary>
        /// 获取查询表达式容器
        /// </summary>
        /// <returns></returns>
        QueryContainer GetQuery();
    }
}
