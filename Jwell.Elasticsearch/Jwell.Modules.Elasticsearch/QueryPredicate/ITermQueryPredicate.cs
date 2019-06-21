using Jwell.Modules.Elasticsearch.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Elasticsearch.QueryPredicate
{
    /// <summary>
    /// term查询谓语接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TV"></typeparam>
    public interface ITermQueryPredicate<T,TV> : IQueryPredicate<T> where T: SearchEntityBase
    {
        
    }
}
