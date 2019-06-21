using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Jwell.Modules.Elasticsearch.Entity;
using Jwell.Modules.Elasticsearch.Helper;
using Jwell.Modules.Elasticsearch.QueryPredicate;

namespace Jwell.Modules.Elasticsearch.SearchCondition
{
    /// <summary>
    /// 全文检索查询条件
    /// </summary>
    /// <typeparam name="T">查询模型类型</typeparam>
    public class FullTextSearchCondition<T> : IFullTextSearchCondition<T> where T : SearchEntityBase
    {
        /// <summary>
        /// 过滤条件谓语
        /// </summary>
        public IQueryPredicate<T> FilterPredicate { get; private set; }

        /// <summary>
        /// 查询字段
        /// </summary>
        public string[] SearchFields { get; private set; }

        /// <summary>
        /// 查询值
        /// </summary>
        public string SearchValue { get; private set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        public int? SizeNumber { get; private set; }

        /// <summary>
        /// 跳过的数量
        /// </summary>
        public int? SkipNumber { get; private set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public List<FieldSort> Sort { get; private set; }

        /// <summary>
        /// 查询的索引
        /// </summary>
        public string Index { get; private set; }

        /// <summary>
        /// 查询结果是否高亮
        /// </summary>
        public bool IsHighLight { get; private set; }

        /// <summary>
        /// 对某个字段正向排序
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="expression">培训字段表达式</param>
        /// <returns></returns>
        public IFullTextSearchCondition<T> OrderBy<TKey>(Expression<Func<T, TKey>> expression)
        {
            if (this.Sort == default(List<FieldSort>)) this.Sort = new List<FieldSort>();
            string field = LoadFieldHelper.GetFiledStr<T, TKey>(expression);
            this.Sort.Add(new FieldSort
            {
                Field = field,
                isAsc = true
            });
            return this;
        }

        /// <summary>
        /// 对某个字段正向排序
        /// </summary>
        /// <param name="field">字段名称</param>
        /// <returns></returns>
        public IFullTextSearchCondition<T> OrderBy(string field)
        {
            if (this.Sort == default(List<FieldSort>)) this.Sort = new List<FieldSort>();
            this.Sort.Add(new FieldSort
            {
                Field = field,
                isAsc = true
            });
            return this;
        }

        /// <summary>
        /// 对某个字段反向排序
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="expression">排序字段表达式</param>
        /// <returns></returns>
        public IFullTextSearchCondition<T> OrderByDescending<TKey>(Expression<Func<T, TKey>> expression)
        {
            if (this.Sort == default(List<FieldSort>)) this.Sort = new List<FieldSort>();
            string field = LoadFieldHelper.GetFiledStr<T, TKey>(expression);
            this.Sort.Add(new FieldSort
            {
                Field = field,
                isAsc = false
            });
            return this;
        }

        /// <summary>
        /// 对某个字段反向排序
        /// </summary>
        /// <param name="field">排序字段名称</param>
        /// <returns></returns>
        public IFullTextSearchCondition<T> OrderByDescending(string field)
        {
            if (this.Sort == default(List<FieldSort>)) this.Sort = new List<FieldSort>();
            this.Sort.Add(new FieldSort
            {
                Field = field,
                isAsc = false
            });
            return this;
        }

        /// <summary>
        /// 设置过滤条件
        /// </summary>
        /// <param name="expression">过滤条件谓语表达式</param>
        /// <returns></returns>
        public IFullTextSearchCondition<T> Filter(Expression<Func<IBoolQueryPredicate<T>, IBoolQueryPredicate<T>>> expression)
        {
            this.FilterPredicate = expression.Compile().Invoke(new BoolQueryPredicate<T>());
            return this;
        }

        /// <summary>
        /// 设置过滤条件
        /// </summary>
        /// <param name="predicate">过滤条件谓语</param>
        /// <returns></returns>
        public IFullTextSearchCondition<T> Filter(IQueryPredicate<T> predicate)
        {
            this.FilterPredicate = predicate;
            return this;
        }

        /// <summary>
        /// 设置是否高亮
        /// </summary>
        /// <param name="isHighLight">是否高亮，默认为false</param>
        /// <returns></returns>
        public IFullTextSearchCondition<T> IsUseHighLight(bool isHighLight)
        {
            this.IsHighLight = isHighLight;
            return this;
        }

        /// <summary>
        /// 设置查询索引
        /// </summary>
        /// <param name="index">索引名称</param>
        /// <returns></returns>
        public IFullTextSearchCondition<T> SetIndex(string index)
        {
            this.Index = index;
            return this;
        }

        /// <summary>
        /// 设置分页大小
        /// </summary>
        /// <param name="size">分页大小</param>
        /// <returns></returns>
        public IFullTextSearchCondition<T> Size(int size)
        {
            this.SizeNumber = size;
            return this;
        }

        /// <summary>
        /// 设置跳过的数量
        /// </summary>
        /// <param name="skip">跳过的数量</param>
        /// <returns></returns>
        public IFullTextSearchCondition<T> Skip(int skip)
        {
            this.SkipNumber = skip;
            return this;
        }

        /// <summary>
        /// 设置查询条件
        /// </summary>
        /// <param name="value">查询的值</param>
        /// <param name="searchKeyExpressions">查询字段表达式</param>
        /// <returns></returns>
        public IFullTextSearchCondition<T> Where(string value, params Expression<Func<T, string>>[] searchKeyExpressions)
        {
            this.SearchFields = searchKeyExpressions.Select(x => LoadFieldHelper.GetFiledStr<T, string>(x)).ToArray();
            this.SearchValue = value;
            return this;
        }

        /// <summary>
        /// 设置查询条件
        /// </summary>
        /// <param name="value">查询的值</param>
        /// <param name="searchField">查询字段</param>
        /// <returns></returns>
        public IFullTextSearchCondition<T> Where(string value, params string[] searchFields)
        {
            this.SearchFields = searchFields;
            this.SearchValue = value;
            return this;
        }
    }

    /// <summary>
    /// 全文检索查询条件
    /// </summary>
    /// <typeparam name="T">查询模型类型</typeparam>
    /// <typeparam name="TR">返回结果模型类型</typeparam>
    public class FullTextSearchCondition<T, TR> : IFullTextSearchCondition<T, TR>
        where T : SearchEntityBase
        where TR : class
    {
        /// <summary>
        /// 过滤条件谓语
        /// </summary>
        public IQueryPredicate<T> FilterPredicate { get; private set; }

        /// <summary>
        /// 查询字段
        /// </summary>
        public string[] SearchFields { get; private set; }

        /// <summary>
        /// 查询值
        /// </summary>
        public string SearchValue { get; private set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        public int? SizeNumber { get; private set; }

        /// <summary>
        /// 跳过的数量
        /// </summary>
        public int? SkipNumber { get; private set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public List<FieldSort> Sort { get; private set; }

        /// <summary>
        /// 查询的索引名称
        /// </summary>
        public string Index { get; private set; }

        /// <summary>
        /// 转换表达式
        /// </summary>
        public Expression<Func<T, TR>> Selector { get; private set; }

        /// <summary>
        /// 是否高亮
        /// </summary>
        public bool IsHighLight { get; private set; }

        /// <summary>
        /// 设置正向排序字段
        /// </summary>
        /// <typeparam name="TKey">字段类型</typeparam>
        /// <param name="expression">字段表达式</param>
        /// <returns></returns>
        public IFullTextSearchCondition<T, TR> OrderBy<TKey>(Expression<Func<T, TKey>> expression)
        {
            if (this.Sort == default(List<FieldSort>)) this.Sort = new List<FieldSort>();
            string field = LoadFieldHelper.GetFiledStr<T, TKey>(expression);
            this.Sort.Add(new FieldSort
            {
                Field = field,
                isAsc = true
            });
            return this;
        }

        /// <summary>
        /// 设置正向排序字段
        /// </summary>
        /// <param name="field">字段名称</param>
        /// <returns></returns>
        public IFullTextSearchCondition<T, TR> OrderBy(string field)
        {
            if (this.Sort == default(List<FieldSort>)) this.Sort = new List<FieldSort>();
            this.Sort.Add(new FieldSort
            {
                Field = field,
                isAsc = true
            });
            return this;
        }

        /// <summary>
        /// 设置反向排序字段
        /// </summary>
        /// <typeparam name="TKey">字段类型</typeparam>
        /// <param name="expression">字段表达式</param>
        /// <returns></returns>
        public IFullTextSearchCondition<T, TR> OrderByDescending<TKey>(Expression<Func<T, TKey>> expression)
        {
            if (this.Sort == default(List<FieldSort>)) this.Sort = new List<FieldSort>();
            string field = LoadFieldHelper.GetFiledStr<T, TKey>(expression);
            this.Sort.Add(new FieldSort
            {
                Field = field,
                isAsc = false
            });
            return this;
        }

        /// <summary>
        /// 设置反向培训字段
        /// </summary>
        /// <param name="field">字段名称</param>
        /// <returns></returns>
        public IFullTextSearchCondition<T, TR> OrderByDescending(string field)
        {
            if (this.Sort == default(List<FieldSort>)) this.Sort = new List<FieldSort>();
            this.Sort.Add(new FieldSort
            {
                Field = field,
                isAsc = false
            });
            return this;
        }

        /// <summary>
        /// 设置过滤条件谓语表达式
        /// </summary>
        /// <param name="expression">过滤条件谓语表达式</param>
        /// <returns></returns>
        public IFullTextSearchCondition<T, TR> Filter(Expression<Func<IBoolQueryPredicate<T>, IBoolQueryPredicate<T>>> expression)
        {
            this.FilterPredicate = expression.Compile().Invoke(new BoolQueryPredicate<T>());
            return this;
        }

        /// <summary>
        /// 设置过滤条件谓语
        /// </summary>
        /// <param name="predicate">过滤条件谓语</param>
        /// <returns></returns>
        public IFullTextSearchCondition<T, TR> Filter(IQueryPredicate<T> predicate)
        {
            this.FilterPredicate = predicate;
            return this;
        }

        /// <summary>
        /// 设置是否高亮
        /// </summary>
        /// <param name="isHighLight"></param>
        /// <returns></returns>
        public IFullTextSearchCondition<T, TR> IsUseHighLight(bool isHighLight)
        {
            this.IsHighLight = isHighLight;
            return this;
        }

        /// <summary>
        /// 设置转换函数
        /// </summary>
        /// <param name="select">转换函数表达式</param>
        /// <returns></returns>
        public IFullTextSearchCondition<T, TR> Select(Expression<Func<T, TR>> select)
        {
            this.Selector = select;
            return this;
        }

        /// <summary>
        /// 设置查询索引
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IFullTextSearchCondition<T, TR> SetIndex(string index)
        {
            this.Index = Index;
            return this;
        }

        /// <summary>
        /// 设置分页大小
        /// </summary>
        /// <param name="size">分页大小</param>
        /// <returns></returns>
        public IFullTextSearchCondition<T, TR> Size(int size)
        {
            this.SizeNumber = size;
            return this;
        }

        /// <summary>
        /// 设置跳过的数量
        /// </summary>
        /// <param name="skip">跳过的数量</param>
        /// <returns></returns>
        public IFullTextSearchCondition<T, TR> Skip(int skip)
        {
            this.SkipNumber = skip;
            return this;
        }

        /// <summary>
        /// 设置查询条件
        /// </summary>
        /// <param name="value">查询值</param>
        /// <param name="searchKeyExpressions">查询字段表达式</param>
        /// <returns></returns>
        public IFullTextSearchCondition<T, TR> Where(string value, params Expression<Func<T, string>>[] searchKeyExpressions)
        {
            this.SearchFields = searchKeyExpressions.Select(x => LoadFieldHelper.GetFiledStr<T, string>(x)).ToArray();
            this.SearchValue = value;
            return this;
        }


        /// <summary>
        /// 设置查询条件
        /// </summary>
        /// <param name="value">查询值</param>
        /// <param name="searchFields">查询字段</param>
        /// <returns></returns>
        public IFullTextSearchCondition<T, TR> Where(string value, params string[] searchFields)
        {
            this.SearchFields = searchFields;
            this.SearchValue = value;
            return this;
        }
    }
}
