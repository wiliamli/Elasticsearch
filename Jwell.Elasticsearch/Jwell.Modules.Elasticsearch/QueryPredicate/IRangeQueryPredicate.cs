using Jwell.Modules.Elasticsearch.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Elasticsearch.QueryPredicate
{
    /// <summary>
    /// 范围查询谓语接口
    /// </summary>
    /// <typeparam name="T">查询实体</typeparam>
    /// <typeparam name="TV">查询属性</typeparam>
    public interface IRangeQueryPredicate<T, TV> : IQueryPredicate<T>
        where T : SearchEntityBase
        where TV : struct
    {

    }
}
