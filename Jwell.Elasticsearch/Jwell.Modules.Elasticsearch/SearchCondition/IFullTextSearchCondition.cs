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
    /// 全文检索查询条件接口
    /// </summary>
    /// <typeparam name="T">查询模型类型</typeparam>
    public interface IFullTextSearchCondition<T> where T : SearchEntityBase
    {
        /// <summary>
        /// 过滤条件谓语
        /// </summary>
        IQueryPredicate<T> FilterPredicate { get; }

        /// <summary>
        /// 查询字段
        /// </summary>
        string[] SearchFields { get; }

        /// <summary>
        /// 查询值
        /// </summary>
        string SearchValue { get; }

        /// <summary>
        /// 分页大小
        /// </summary>
        int? SizeNumber { get; }

        /// <summary>
        /// 跳过的数量
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
        /// 查询结果是否高亮
        /// </summary>
        bool IsHighLight { get; }

        /// <summary>
        /// 设置是否高亮
        /// </summary>
        /// <param name="isHighLight">是否高亮，默认为false</param>
        /// <returns></returns>
        IFullTextSearchCondition<T> IsUseHighLight(bool isHighLight);

        /// <summary>
        /// 设置查询索引
        /// </summary>
        /// <param name="index">索引名称</param>
        /// <returns></returns>
        IFullTextSearchCondition<T> SetIndex(string index);

        /// <summary>
        /// 设置查询条件
        /// </summary>
        /// <param name="value">查询的值</param>
        /// <param name="searchKeyExpressions">查询字段表达式</param>
        /// <returns></returns>
        IFullTextSearchCondition<T> Where(string value, params Expression<Func<T, string>>[] searchKeyExpressions);

        /// <summary>
        /// 设置查询条件
        /// </summary>
        /// <param name="value">查询的值</param>
        /// <param name="searchField">查询字段</param>
        /// <returns></returns>
        IFullTextSearchCondition<T> Where(string value, params string[] searchField);

        /// <summary>
        /// 设置分页大小
        /// </summary>
        /// <param name="size">分页大小</param>
        /// <returns></returns>
        IFullTextSearchCondition<T> Size(int size);

        /// <summary>
        /// 设置跳过的数量
        /// </summary>
        /// <param name="skip">跳过的数量</param>
        /// <returns></returns>
        IFullTextSearchCondition<T> Skip(int skip);

        /// <summary>
        /// 设置过滤条件
        /// </summary>
        /// <param name="expression">过滤条件谓语表达式</param>
        /// <returns></returns>
        IFullTextSearchCondition<T> Filter(Expression<Func<IBoolQueryPredicate<T>, IBoolQueryPredicate<T>>> expression);

        /// <summary>
        /// 设置过滤条件
        /// </summary>
        /// <param name="predicate">过滤条件谓语</param>
        /// <returns></returns>
        IFullTextSearchCondition<T> Filter(IQueryPredicate<T> predicate);

        /// <summary>
        /// 对某个字段正向排序
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="expression">培训字段表达式</param>
        /// <returns></returns>
        IFullTextSearchCondition<T> OrderBy<TKey>(Expression<Func<T, TKey>> expression);

        /// <summary>
        /// 对某个字段正向排序
        /// </summary>
        /// <param name="field">字段名称</param>
        /// <returns></returns>
        IFullTextSearchCondition<T> OrderBy(string field);

        /// <summary>
        /// 对某个字段反向排序
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="expression">排序字段表达式</param>
        /// <returns></returns>
        IFullTextSearchCondition<T> OrderByDescending<TKey>(Expression<Func<T, TKey>> expression);

        /// <summary>
        /// 对某个字段反向排序
        /// </summary>
        /// <param name="field">排序字段名称</param>
        /// <returns></returns>
        IFullTextSearchCondition<T> OrderByDescending(string field);
    }

    /// <summary>
    /// 全文检索查询条件接口
    /// </summary>
    /// <typeparam name="T">查询模型类型</typeparam>
    /// <typeparam name="TR">返回结果模型类型</typeparam>
    public interface IFullTextSearchCondition<T, TR>
        where T : SearchEntityBase
        where TR : class
    {
        /// <summary>
        /// 过滤条件谓语
        /// </summary>
        IQueryPredicate<T> FilterPredicate { get; }

        /// <summary>
        /// 查询字段
        /// </summary>
        string[] SearchFields { get; }

        /// <summary>
        /// 查询值
        /// </summary>
        string SearchValue { get; }

        /// <summary>
        /// 分页大小
        /// </summary>
        int? SizeNumber { get; }

        /// <summary>
        /// 跳过的数量
        /// </summary>
        int? SkipNumber { get; }

        /// <summary>
        /// 排序字段
        /// </summary>
        List<FieldSort> Sort { get; }

        /// <summary>
        /// 查询的索引名称
        /// </summary>
        string Index { get; }

        /// <summary>
        /// 是否高亮
        /// </summary>
        bool IsHighLight { get; }

        /// <summary>
        /// 转换表达式
        /// </summary>
        Expression<Func<T, TR>> Selector { get; }

        /// <summary>
        /// 设置是否高亮
        /// </summary>
        /// <param name="isHighLight"></param>
        /// <returns></returns>
        IFullTextSearchCondition<T, TR> IsUseHighLight(bool isHighLight);

        /// <summary>
        /// 设置查询索引
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        IFullTextSearchCondition<T, TR> SetIndex(string index);

        /// <summary>
        /// 设置查询条件
        /// </summary>
        /// <param name="value">查询值</param>
        /// <param name="searchKeyExpressions">查询字段表达式</param>
        /// <returns></returns>
        IFullTextSearchCondition<T, TR> Where(string value, params Expression<Func<T, string>>[] searchKeyExpressions);

        /// <summary>
        /// 设置查询条件
        /// </summary>
        /// <param name="value">查询值</param>
        /// <param name="searchFields">查询字段</param>
        /// <returns></returns>
        IFullTextSearchCondition<T, TR> Where(string value, params string[] searchFields);

        /// <summary>
        /// 设置分页大小
        /// </summary>
        /// <param name="size">分页大小</param>
        /// <returns></returns>
        IFullTextSearchCondition<T, TR> Size(int size);

        /// <summary>
        /// 设置跳过的数量
        /// </summary>
        /// <param name="skip">跳过的数量</param>
        /// <returns></returns>
        IFullTextSearchCondition<T, TR> Skip(int skip);

        /// <summary>
        /// 设置过滤条件谓语表达式
        /// </summary>
        /// <param name="expression">过滤条件谓语表达式</param>
        /// <returns></returns>
        IFullTextSearchCondition<T, TR> Filter(Expression<Func<IBoolQueryPredicate<T>, IBoolQueryPredicate<T>>> expression);

        /// <summary>
        /// 设置过滤条件谓语
        /// </summary>
        /// <param name="predicate">过滤条件谓语</param>
        /// <returns></returns>
        IFullTextSearchCondition<T, TR> Filter(IQueryPredicate<T> predicate);

        /// <summary>
        /// 设置正向排序字段
        /// </summary>
        /// <typeparam name="TKey">字段类型</typeparam>
        /// <param name="expression">字段表达式</param>
        /// <returns></returns>
        IFullTextSearchCondition<T, TR> OrderBy<TKey>(Expression<Func<T, TKey>> expression);

        /// <summary>
        /// 设置正向排序字段
        /// </summary>
        /// <param name="field">字段名称</param>
        /// <returns></returns>
        IFullTextSearchCondition<T, TR> OrderBy(string field);

        /// <summary>
        /// 设置反向排序字段
        /// </summary>
        /// <typeparam name="TKey">字段类型</typeparam>
        /// <param name="expression">字段表达式</param>
        /// <returns></returns>
        IFullTextSearchCondition<T, TR> OrderByDescending<TKey>(Expression<Func<T, TKey>> expression);

        /// <summary>
        /// 设置反向培训字段
        /// </summary>
        /// <param name="field">字段名称</param>
        /// <returns></returns>
        IFullTextSearchCondition<T, TR> OrderByDescending(string field);

        /// <summary>
        /// 设置转换函数
        /// </summary>
        /// <param name="select">转换函数表达式</param>
        /// <returns></returns>
        IFullTextSearchCondition<T, TR> Select(Expression<Func<T, TR>> select);
    }
}
