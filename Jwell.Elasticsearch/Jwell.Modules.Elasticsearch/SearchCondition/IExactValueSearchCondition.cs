using Jwell.Modules.Elasticsearch.Entity;
using Jwell.Modules.Elasticsearch.QueryPredicate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Elasticsearch.SearchCondition
{
    /// <summary>
    /// 结构化数据查询条件接口
    /// </summary>
    /// <typeparam name="T">查询模型类型</typeparam>
    public interface IExactValueSearchCondition<T> where T : SearchEntityBase
    {
        /// <summary>
        /// 查询谓语
        /// </summary>
        IQueryPredicate<T> QueryPredicate { get; }

        /// <summary>
        /// 分页大小
        /// </summary>
        int? SizeNumber { get; }

        /// <summary>
        /// 查询跳过的数量
        /// </summary>
        int? SkipNumber { get; }

        /// <summary>
        /// 排序字段
        /// </summary>
        List<FieldSort> Sort { get; }

        /// <summary>
        /// 查询的索引
        /// </summary>
        string Index { get; }

        /// <summary>
        /// 设置查询的索引
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        IExactValueSearchCondition<T> SearchIndex(string index);

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="expression">查询谓语表达式</param>
        /// <returns></returns>
        IExactValueSearchCondition<T> Where(Expression<Func<IBoolQueryPredicate<T>, IBoolQueryPredicate<T>>> expression);

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="predicate">查询谓语</param>
        /// <returns></returns>
        IExactValueSearchCondition<T> Where(IQueryPredicate<T> predicate);

        /// <summary>
        /// 设置分页大小
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        IExactValueSearchCondition<T> Size(int size);

        /// <summary>
        /// 设置查询跳过的数量
        /// </summary>
        /// <param name="skip"></param>
        /// <returns></returns>
        IExactValueSearchCondition<T> Skip(int skip);

        /// <summary>
        /// 对某个字段正向排序
        /// </summary>
        /// <typeparam name="TKey">正向排序字段类型</typeparam>
        /// <param name="expression">lamda表达式</param>
        /// <returns></returns>
        IExactValueSearchCondition<T> OrderBy<TKey>(Expression<Func<T, TKey>> expression);

        /// <summary>
        /// 对某个字段正向排序
        /// </summary>
        /// <param name="field">正向排序字段</param>
        /// <returns></returns>
        IExactValueSearchCondition<T> OrderBy(string field);

        /// <summary>
        /// 对某个字段反向排序
        /// </summary>
        /// <typeparam name="TKey">反向排序字段类型</typeparam>
        /// <param name="expression">lamda表达式</param>
        /// <returns></returns>
        IExactValueSearchCondition<T> OrderByDescending<TKey>(Expression<Func<T, TKey>> expression);

        /// <summary>
        /// 对某个字段反向排序
        /// </summary>
        /// <param name="field">反向排序字段</param>
        /// <returns></returns>
        IExactValueSearchCondition<T> OrderByDescending(string field);
    }

    /// <summary>
    /// 结构化查询条件接口
    /// </summary>
    /// <typeparam name="T">查询模型类型</typeparam>
    /// <typeparam name="TR">查询结果模型类型</typeparam>
    public interface IExactValueSearchCondition<T, TR>
        where T : SearchEntityBase
        where TR : class
    {
        /// <summary>
        /// 查询谓语
        /// </summary>
        IQueryPredicate<T> QueryPredicate { get; }

        /// <summary>
        /// 分页大小
        /// </summary>
        int? SizeNumber { get; }

        /// <summary>
        /// 查询跳过的数量
        /// </summary>
        int? SkipNumber { get; }

        /// <summary>
        /// 排序字段
        /// </summary>
        List<FieldSort> Sort { get; }

        /// <summary>
        /// 查询的索引
        /// </summary>
        string Index { get; }

        /// <summary>
        /// 装换函数表达式
        /// </summary>
        Expression<Func<T, TR>> Selector { get; }

        /// <summary>
        /// 设置查询的索引
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        IExactValueSearchCondition<T,TR> SetIndex(string index);

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="expression">查询谓语表达式</param>
        /// <returns></returns>
        IExactValueSearchCondition<T, TR> Where(Expression<Func<IBoolQueryPredicate<T>, IBoolQueryPredicate<T>>> expression);

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="predicate">查询谓语</param>
        /// <returns></returns>
        IExactValueSearchCondition<T, TR> Where(IQueryPredicate<T> predicate);

        /// <summary>
        /// 设置分页大小
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        IExactValueSearchCondition<T, TR> Size(int size);

        /// <summary>
        /// 设置查询跳过的数量
        /// </summary>
        /// <param name="skip"></param>
        /// <returns></returns>
        IExactValueSearchCondition<T, TR> Skip(int skip);

        /// <summary>
        /// 对某个字段正向排序
        /// </summary>
        /// <typeparam name="TKey">正向排序字段类型</typeparam>
        /// <param name="expression">lamda表达式</param>
        /// <returns></returns>
        IExactValueSearchCondition<T, TR> OrderBy<TKey>(Expression<Func<T, TKey>> expression);

        /// <summary>
        /// 对某个字段正向排序
        /// </summary>
        /// <param name="field">正向排序字段</param>
        /// <returns></returns>
        IExactValueSearchCondition<T, TR> OrderBy(string field);

        /// <summary>
        /// 对某个字段反向排序
        /// </summary>
        /// <typeparam name="TKey">反向排序字段类型</typeparam>
        /// <param name="expression">lamda表达式</param>
        /// <returns></returns>
        IExactValueSearchCondition<T, TR> OrderByDescending<TKey>(Expression<Func<T, TKey>> expression);

        /// <summary>
        /// 对某个字段反向排序
        /// </summary>
        /// <param name="field">反向排序字段</param>
        /// <returns></returns>
        IExactValueSearchCondition<T, TR> OrderByDescending(string field);

        /// <summary>
        /// 设置装换函数
        /// </summary>
        /// <param name="expression">转换函数表达式</param>
        /// <returns></returns>
        IExactValueSearchCondition<T, TR> Select(Expression<Func<T, TR>> expression);
    }
}
