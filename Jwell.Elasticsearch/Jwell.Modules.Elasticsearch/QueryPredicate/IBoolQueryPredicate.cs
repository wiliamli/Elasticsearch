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
    /// bool条件查询谓语接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBoolQueryPredicate<T> : IQueryPredicate<T> where T : SearchEntityBase
    {
        /// <summary>
        /// 添加必须匹配term条件谓语
        /// </summary>
        /// <typeparam name="TV">谓词字段类型</typeparam>
        /// <param name="expression">谓语表达式</param>
        /// <param name="value">匹配值</param>
        /// <returns></returns>
        IBoolQueryPredicate<T> AddTermPredicateForMust<TV>(Expression<Func<T, TV>> expression, TV value);

        /// <summary>
        /// 添加不能匹配term条件谓语
        /// </summary>
        /// <typeparam name="TV">谓词字段类型</typeparam>
        /// <param name="expression">谓语表达式</param>
        /// <param name="value">匹配值</param>
        /// <returns></returns>
        IBoolQueryPredicate<T> AddTermPredicateForMustNot<TV>(Expression<Func<T, TV>> expression, TV value);

        /// <summary>
        /// 添加应匹配term条件谓语
        /// </summary>
        /// <typeparam name="TV">谓词字段类型</typeparam>
        /// <param name="expression">谓语表达式</param>
        /// <param name="value">匹配值</param>
        /// <returns></returns>
        IBoolQueryPredicate<T> AddTermPredicateForShuld<TV>(Expression<Func<T, TV>> expression, TV value);

        /// <summary>
        /// 添加必须匹配terms条件谓语
        /// </summary>
        /// <typeparam name="TV">谓词字段类型</typeparam>
        /// <param name="expression">谓语表达式</param>
        /// <param name="values">配置值数组</param>
        /// <returns></returns>
        IBoolQueryPredicate<T> AddTermsPredicateForMust<TV>(Expression<Func<T, TV>> expression, params TV[] values);

        /// <summary>
        /// 添加不能匹配terms条件谓语
        /// </summary>
        /// <typeparam name="TV">谓词字段类型</typeparam>
        /// <param name="expression">谓语表达式</param>
        /// <param name="values">配置值数组</param>
        /// <returns></returns>
        IBoolQueryPredicate<T> AddTermsPredicateForMustNot<TV>(Expression<Func<T, TV>> expression, params TV[] values);

        /// <summary>
        /// 添加应匹配terms条件谓语
        /// </summary>
        /// <typeparam name="TV">谓词字段类型</typeparam>
        /// <param name="expression">谓语表达式</param>
        /// <param name="values">配置值数组</param>
        /// <returns></returns>
        IBoolQueryPredicate<T> AddTermsPredicateForShuld<TV>(Expression<Func<T, TV>> expression, params TV[] values);

        /// <summary>
        /// 添加必须匹配的range条件谓语
        /// </summary>
        /// <typeparam name="TV">谓词字段类型</typeparam>
        /// <param name="expression">谓词表达式</param>
        /// <param name="greaterThan">大于</param>
        /// <param name="greaterThanOrEqualTo">大于等于</param>
        /// <param name="lessThan">小于</param>
        /// <param name="lessThanOrEqualTo">小于等于</param>
        /// <returns></returns>
        IBoolQueryPredicate<T> AddRangePredicateForMust<TV>(
            Expression<Func<T, TV>> expression,
            TV? GreaterThan = null,
            TV? GreaterThanOrEqualTo = null,
            TV? LessThan = null,
            TV? LessThanOrEqualTo = null) where TV : struct;

        /// <summary>
        /// 添加不能匹配的range条件谓语
        /// </summary>
        /// <typeparam name="TV">谓词字段类型</typeparam>
        /// <param name="expression">谓词表达式</param>
        /// <param name="greaterThan">大于</param>
        /// <param name="greaterThanOrEqualTo">大于等于</param>
        /// <param name="lessThan">小于</param>
        /// <param name="lessThanOrEqualTo">小于等于</param>
        /// <returns></returns>
        IBoolQueryPredicate<T> AddRangePredicateForMustNot<TV>(
            Expression<Func<T, TV>> expression,
           TV? GreaterThan = null,
            TV? GreaterThanOrEqualTo = null,
            TV? LessThan = null,
            TV? LessThanOrEqualTo = null) where TV : struct;

        /// <summary>
        /// 添加应匹配的range条件谓语
        /// </summary>
        /// <typeparam name="TV">谓词字段类型</typeparam>
        /// <param name="expression">谓词表达式</param>
        /// <param name="greaterThan">大于</param>
        /// <param name="greaterThanOrEqualTo">大于等于</param>
        /// <param name="lessThan">小于</param>
        /// <param name="lessThanOrEqualTo">小于等于</param>
        /// <returns></returns>
        IBoolQueryPredicate<T> AddRangePredicateForShuld<TV>(
            Expression<Func<T, TV>> expression,
            TV? GreaterThan = null,
            TV? GreaterThanOrEqualTo = null,
            TV? LessThan = null,
            TV? LessThanOrEqualTo = null) where TV : struct;

        /// <summary>
        /// 添加必须必须匹配的bool条件谓词
        /// </summary>
        /// <param name="expression">条件谓词表达式</param>
        /// <returns></returns>
        IBoolQueryPredicate<T> AddBoolPredicateForMust(Expression<Func<BoolQueryPredicate<T>, IBoolQueryPredicate<T>>> expression);

        /// <summary>
        /// 添加不能匹配的bool条件谓词
        /// </summary>
        /// <param name="expression">条件谓词表达式</param>
        /// <returns></returns>
        IBoolQueryPredicate<T> AddBoolPredicateForMustNot(Expression<Func<BoolQueryPredicate<T>, IBoolQueryPredicate<T>>> expression);

        /// <summary>
        /// 添加应该匹配的bool条件谓词
        /// </summary>
        /// <param name="expression">条件谓词表达式</param>
        /// <returns></returns>
        IBoolQueryPredicate<T> AddBoolPredicateForShuld(Expression<Func<BoolQueryPredicate<T>, IBoolQueryPredicate<T>>> expression);
    }
}
