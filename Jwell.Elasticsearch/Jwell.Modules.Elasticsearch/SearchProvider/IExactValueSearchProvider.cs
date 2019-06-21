using Jwell.Framework.Ioc;
using Jwell.Modules.Elasticsearch.Entity;
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
    /// 结构化查询接口
    /// </summary>
    [Singleton]
    public interface IExactValueSearchProvider
    {
        /// <summary>
        /// 结构化查询
        /// </summary>
        /// <typeparam name="T">查询模型类型</typeparam>
        /// <param name="searchCondition">查询条件</param>
        /// <returns></returns>
        IEnumerable<T> ExactValueSearch<T>(IExactValueSearchCondition<T> searchCondition) where T : SearchEntityBase;

        /// <summary>
        /// 结构化查询
        /// </summary>
        /// <typeparam name="T">查询模型类型</typeparam>
        /// <param name="expression">查询条件表达式</param>
        /// <returns></returns>
        IEnumerable<T> ExactValueSearch<T>(Expression<Func<IExactValueSearchCondition<T>, IExactValueSearchCondition<T>>> expression) where T : SearchEntityBase;

        /// <summary>
        /// 结构化查询
        /// </summary>
        /// <typeparam name="T">查询模型类型</typeparam>
        /// <typeparam name="TR">查询结果模型类型</typeparam>
        /// <param name="searchCondition">查询条件</param>
        /// <returns></returns>
        IEnumerable<TR> ExactValueSearch<T, TR>(IExactValueSearchCondition<T, TR> searchCondition) where T : SearchEntityBase where TR : class;

        /// <summary>
        /// 结构化查询
        /// </summary>
        /// <typeparam name="T">查询模型类型</typeparam>
        /// <typeparam name="TR">查询结果模型类型</typeparam>
        /// <param name="expression">查询条件表达式</param>
        /// <returns></returns>
        IEnumerable<TR> ExactValueSearch<T, TR>(Expression<Func<IExactValueSearchCondition<T, TR>, IExactValueSearchCondition<T, TR>>> expression) where T : SearchEntityBase where TR : class;
    }
}
