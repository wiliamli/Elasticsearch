using Jwell.Framework.Ioc;
using Jwell.Modules.Elasticsearch.Entity;
using Jwell.Modules.Elasticsearch.QueryPredicate;
using Jwell.Modules.Elasticsearch.SearchCondition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Elasticsearch.SearchProvider
{
    /// <summary>
    /// 全文检索查询接口
    /// </summary>
    [Singleton]
    public interface IFullTextSearchProvider
    {
        /// <summary>
        /// 全文检索
        /// </summary>
        /// <typeparam name="T">查询模型类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        IEnumerable<T> FullTextSearch<T>(IFullTextSearchCondition<T> condition) where T : SearchEntityBase;

        /// <summary>
        /// 全文检索
        /// </summary>
        /// <typeparam name="T">查询模型类型</typeparam>
        /// <param name="searchExpression">查询条件表达式</param>
        /// <returns></returns>
        IEnumerable<T> FullTextSearch<T>(Expression<Func<IFullTextSearchCondition<T>,IFullTextSearchCondition<T>>> searchExpression) where T : SearchEntityBase;

        /// <summary>
        /// 全文检索
        /// </summary>
        /// <typeparam name="T">查询模型类型</typeparam>
        /// <typeparam name="TR">查询结果模型类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        IEnumerable<TR> FullTextSearch<T, TR>(IFullTextSearchCondition<T, TR> condition) where T : SearchEntityBase where TR : class;

        /// <summary>
        /// 全文检索
        /// </summary>
        /// <typeparam name="T">查询模型类型</typeparam>
        /// <typeparam name="TR">查询结果模型类型</typeparam>
        /// <param name="searchExpression">查询条件表达式</param>
        /// <returns></returns>
        IEnumerable<TR> FullTextSearch<T, TR>(Expression<Func<IFullTextSearchCondition<T, TR>, IFullTextSearchCondition<T, TR>>> searchExpression) where T : SearchEntityBase where TR : class;
    }
}
