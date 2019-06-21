using Jwell.Modules.Elasticsearch.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Elasticsearch.QueryPredicate
{
    /// <summary>
    /// terms查询谓语接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TV"></typeparam>
    interface ITermsQueryPredicate<T,TV>: IQueryPredicate<T> where T: SearchEntityBase
    {
    }
}
