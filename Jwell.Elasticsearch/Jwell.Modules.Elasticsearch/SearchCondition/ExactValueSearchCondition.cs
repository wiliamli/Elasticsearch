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
    /// 结构化数据查询条件
    /// </summary>
    /// <typeparam name="T">查询模型类型</typeparam>
    public class ExactValueSearchCondition<T> : IExactValueSearchCondition<T>
        where T : SearchEntityBase
    {
        #region 属性
        /// <summary>
        /// 查询谓语
        /// </summary>
        public IQueryPredicate<T> QueryPredicate { get; private set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        public int? SizeNumber { get; private set; }

        /// <summary>
        /// 查询跳过的数量
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
        #endregion

        /// <summary>
        /// 对某个字段正向排序
        /// </summary>
        /// <typeparam name="TKey">正向排序字段类型</typeparam>
        /// <param name="expression">lamda表达式</param>
        /// <returns></returns>
        public IExactValueSearchCondition<T> OrderBy<TKey>(Expression<Func<T, TKey>> expression)
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
        /// <param name="field">正向排序字段</param>
        /// <returns></returns>
        public IExactValueSearchCondition<T> OrderBy(string field)
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
        /// <typeparam name="TKey">反向排序字段类型</typeparam>
        /// <param name="expression">lamda表达式</param>
        /// <returns></returns>
        public IExactValueSearchCondition<T> OrderByDescending<TKey>(Expression<Func<T, TKey>> expression)
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
        /// <param name="field">反向排序字段</param>
        /// <returns></returns>
        public IExactValueSearchCondition<T> OrderByDescending(string field)
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
        /// 设置查询的索引
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IExactValueSearchCondition<T> SearchIndex(string index)
        {
            this.Index = index;
            return this;
        }

        /// <summary>
        /// 设置分页大小
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public IExactValueSearchCondition<T> Size(int size)
        {
            this.SizeNumber = size;
            return this;
        }

        /// <summary>
        /// 设置查询跳过的数量
        /// </summary>
        /// <param name="skip"></param>
        /// <returns></returns>
        public IExactValueSearchCondition<T> Skip(int skip)
        {
            this.SkipNumber = skip;
            return this;
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="expression">查询谓语表达式</param>
        /// <returns></returns>
        public IExactValueSearchCondition<T> Where(Expression<Func<IBoolQueryPredicate<T>, IBoolQueryPredicate<T>>> expression)
        {
            this.QueryPredicate = expression.Compile().Invoke(new BoolQueryPredicate<T>());
            return this;
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="predicate">查询谓语</param>
        /// <returns></returns>
        public IExactValueSearchCondition<T> Where(IQueryPredicate<T> predicate)
        {
            this.QueryPredicate = predicate;
            return this;
        }
    }

    /// <summary>
    /// 结构化查询条件
    /// </summary>
    /// <typeparam name="T">查询模型类型</typeparam>
    /// <typeparam name="TR">查询结果模型类型</typeparam>
    public class ExactValueSearchCondition<T, TR> : IExactValueSearchCondition<T, TR>
       where T : SearchEntityBase
        where TR : class
    {
        #region 属性
        /// <summary>
        /// 查询谓语
        /// </summary>
        public IQueryPredicate<T> QueryPredicate { get; private set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        public int? SizeNumber { get; private set; }

        /// <summary>
        /// 查询跳过的数量
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
        /// 装换函数表达式
        /// </summary>
        public Expression<Func<T, TR>> Selector { get; private set; } 
        #endregion

        /// <summary>
        /// 对某个字段正向排序
        /// </summary>
        /// <typeparam name="TKey">正向排序字段类型</typeparam>
        /// <param name="expression">lamda表达式</param>
        /// <returns></returns>
        public IExactValueSearchCondition<T, TR> OrderBy<TKey>(Expression<Func<T, TKey>> expression)
        {
            if (this.Sort == default(List<FieldSort>))
                this.Sort = new List<FieldSort>();

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
        /// <param name="field">正向排序字段</param>
        /// <returns></returns>
        public IExactValueSearchCondition<T, TR> OrderBy(string field)
        {
            if (this.Sort == default(List<FieldSort>))
                this.Sort = new List<FieldSort>();

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
        /// <typeparam name="TKey">反向排序字段类型</typeparam>
        /// <param name="expression">lamda表达式</param>
        /// <returns></returns>
        public IExactValueSearchCondition<T, TR> OrderByDescending<TKey>(Expression<Func<T, TKey>> expression)
        {
            if (this.Sort == default(List<FieldSort>))
                this.Sort = new List<FieldSort>();

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
        /// <param name="field">反向排序字段</param>
        /// <returns></returns>
        public IExactValueSearchCondition<T, TR> OrderByDescending(string field)
        {
            if (this.Sort == default(List<FieldSort>))
                this.Sort = new List<FieldSort>();

            this.Sort.Add(new FieldSort
            {
                Field = field,
                isAsc = false
            });
            return this;
        }

        /// <summary>
        /// 设置装换函数
        /// </summary>
        /// <param name="expression">转换函数表达式</param>
        /// <returns></returns>
        public IExactValueSearchCondition<T, TR> Select(Expression<Func<T, TR>> expression)
        {
            this.Selector = expression;
            return this;
        }

        /// <summary>
        /// 设置查询的索引
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IExactValueSearchCondition<T,TR> SetIndex(string index)
        {
            this.Index = index;
            return this;
        }

        /// <summary>
        /// 设置分页大小
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public IExactValueSearchCondition<T, TR> Size(int size)
        {
            this.SizeNumber = size;
            return this;
        }

        /// <summary>
        /// 设置查询跳过的数量
        /// </summary>
        /// <param name="skip"></param>
        /// <returns></returns>
        public IExactValueSearchCondition<T, TR> Skip(int skip)
        {
            this.SkipNumber = skip;
            return this;
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="expression">查询谓语表达式</param>
        /// <returns></returns>
        public IExactValueSearchCondition<T, TR> Where(Expression<Func<IBoolQueryPredicate<T>, IBoolQueryPredicate<T>>> expression)
        {
            this.QueryPredicate = expression.Compile().Invoke(new BoolQueryPredicate<T>());
            return this;
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="predicate">查询谓语</param>
        /// <returns></returns>
        public IExactValueSearchCondition<T, TR> Where(IQueryPredicate<T> predicate)
        {
            this.QueryPredicate = predicate;
            return this;
        }
    }
}
