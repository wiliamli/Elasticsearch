using Jwell.Modules.Elasticsearch.Entity;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Elasticsearch.QueryPredicate
{
    /// <summary>
    /// bool条件查询谓词
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BoolQueryPredicate<T> : IBoolQueryPredicate<T> where T : SearchEntityBase
    {
        public BoolQueryPredicate()
        {
            this._must = new List<IQueryPredicate<T>>();
            this._mustNot = new List<IQueryPredicate<T>>();
            this._shuld = new List<IQueryPredicate<T>>();
        }
        private List<IQueryPredicate<T>> _must { get; set; }
        private List<IQueryPredicate<T>> _mustNot { get; set; }
        private List<IQueryPredicate<T>> _shuld { get; set; }

        /// <summary>
        /// 添加必须必须匹配的bool条件谓词
        /// </summary>
        /// <param name="expression">条件谓词表达式</param>
        /// <returns></returns>
        public IBoolQueryPredicate<T> AddBoolPredicateForMust(Expression<Func<BoolQueryPredicate<T>, IBoolQueryPredicate<T>>> expression)
        {
            var addPredicate = expression.Compile().Invoke(new BoolQueryPredicate<T>());
            this._must.Add(addPredicate);
            return this;
        }

        /// <summary>
        /// 添加不能匹配的bool条件谓词
        /// </summary>
        /// <param name="expression">条件谓词表达式</param>
        /// <returns></returns>
        public IBoolQueryPredicate<T> AddBoolPredicateForMustNot(Expression<Func<BoolQueryPredicate<T>, IBoolQueryPredicate<T>>> expression)
        {
            var addPredicate = expression.Compile().Invoke(new BoolQueryPredicate<T>());
            this._mustNot.Add(addPredicate);
            return this;
        }

        /// <summary>
        /// 添加应该匹配的bool条件谓词
        /// </summary>
        /// <param name="expression">条件谓词表达式</param>
        /// <returns></returns>
        public IBoolQueryPredicate<T> AddBoolPredicateForShuld(Expression<Func<BoolQueryPredicate<T>, IBoolQueryPredicate<T>>> expression)
        {
            var addPredicate = expression.Compile().Invoke(new BoolQueryPredicate<T>());
            this._shuld.Add(addPredicate);
            return this;
        }

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
        public IBoolQueryPredicate<T> AddRangePredicateForMust<TV>(
            System.Linq.Expressions.Expression<Func<T, TV>> expression,
            TV? GreaterThan = null,
            TV? GreaterThanOrEqualTo = null,
            TV? LessThan = null,
            TV? LessThanOrEqualTo = null) where TV : struct
        {
            var addPredicate = new RangeQueryPredicate<T, TV>(expression, GreaterThan, GreaterThanOrEqualTo, LessThan, LessThanOrEqualTo);
            this._must.Add(addPredicate);
            return this;
        }

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
        public IBoolQueryPredicate<T> AddRangePredicateForMustNot<TV>(
            System.Linq.Expressions.Expression<Func<T, TV>> expression,
            TV? GreaterThan = null,
            TV? GreaterThanOrEqualTo = null,
            TV? LessThan = null,
            TV? LessThanOrEqualTo = null) where TV : struct
        {
            var addPredicate = new RangeQueryPredicate<T, TV>(expression, GreaterThan, GreaterThanOrEqualTo, LessThan, LessThanOrEqualTo);
            this._mustNot.Add(addPredicate);
            return this;
        }

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
        public IBoolQueryPredicate<T> AddRangePredicateForShuld<TV>(
            System.Linq.Expressions.Expression<Func<T, TV>> expression,
            TV? GreaterThan = null,
            TV? GreaterThanOrEqualTo = null,
            TV? LessThan = null,
            TV? LessThanOrEqualTo = null) where TV : struct
        {
            var addPredicate = new RangeQueryPredicate<T, TV>(expression, GreaterThan, GreaterThanOrEqualTo, LessThan, LessThanOrEqualTo);
            this._shuld.Add(addPredicate);
            return this;
        }

        /// <summary>
        /// 添加必须匹配term条件谓语
        /// </summary>
        /// <typeparam name="TV">谓词字段类型</typeparam>
        /// <param name="expression">谓语表达式</param>
        /// <param name="value">匹配值</param>
        /// <returns></returns>
        public IBoolQueryPredicate<T> AddTermPredicateForMust<TV>(System.Linq.Expressions.Expression<Func<T, TV>> expression, TV value)
        {
            var addPredicate = new TermQueryPredicate<T, TV>(expression, value);
            this._must.Add(addPredicate);
            return this;
        }

        /// <summary>
        /// 添加不能匹配term条件谓语
        /// </summary>
        /// <typeparam name="TV">谓词字段类型</typeparam>
        /// <param name="expression">谓语表达式</param>
        /// <param name="value">匹配值</param>
        /// <returns></returns>
        public IBoolQueryPredicate<T> AddTermPredicateForMustNot<TV>(System.Linq.Expressions.Expression<Func<T, TV>> expression, TV value)
        {
            var addPredicate = new TermQueryPredicate<T, TV>(expression, value);
            this._mustNot.Add(addPredicate);
            return this;
        }

        /// <summary>
        /// 添加应匹配term条件谓语
        /// </summary>
        /// <typeparam name="TV">谓词字段类型</typeparam>
        /// <param name="expression">谓语表达式</param>
        /// <param name="value">匹配值</param>
        /// <returns></returns>
        public IBoolQueryPredicate<T> AddTermPredicateForShuld<TV>(System.Linq.Expressions.Expression<Func<T, TV>> expression, TV value)
        {
            var addPredicate = new TermQueryPredicate<T, TV>(expression, value);
            this._shuld.Add(addPredicate);
            return this;
        }

        /// <summary>
        /// 添加必须匹配terms条件谓语
        /// </summary>
        /// <typeparam name="TV">谓词字段类型</typeparam>
        /// <param name="expression">谓语表达式</param>
        /// <param name="values">配置值数组</param>
        /// <returns></returns>
        public IBoolQueryPredicate<T> AddTermsPredicateForMust<TV>(System.Linq.Expressions.Expression<Func<T, TV>> expression, params TV[] values)
        {
            var addPredicate = new TermsQueryPredicate<T, TV>(expression, values);
            this._must.Add(addPredicate);
            return this;
        }

        /// <summary>
        /// 添加不能匹配terms条件谓语
        /// </summary>
        /// <typeparam name="TV">谓词字段类型</typeparam>
        /// <param name="expression">谓语表达式</param>
        /// <param name="values">配置值数组</param>
        /// <returns></returns>
        public IBoolQueryPredicate<T> AddTermsPredicateForMustNot<TV>(System.Linq.Expressions.Expression<Func<T, TV>> expression, params TV[] values)
        {
            var addPredicate = new TermsQueryPredicate<T, TV>(expression, values);
            this._mustNot.Add(addPredicate);
            return this;
        }

        /// <summary>
        /// 添加应匹配terms条件谓语
        /// </summary>
        /// <typeparam name="TV">谓词字段类型</typeparam>
        /// <param name="expression">谓语表达式</param>
        /// <param name="values">配置值数组</param>
        /// <returns></returns>
        public IBoolQueryPredicate<T> AddTermsPredicateForShuld<TV>(System.Linq.Expressions.Expression<Func<T, TV>> expression, params TV[] values)
        {
            var addPredicate = new TermsQueryPredicate<T, TV>(expression, values);
            this._shuld.Add(addPredicate);
            return this;
        }

        /// <summary>
        /// 获取查询容器
        /// </summary>
        /// <returns></returns>
        public QueryContainer GetQuery()
        {
            return new BoolQuery
            {
                Must = this._must.Select(x => x.GetQuery()),
                MustNot = this._mustNot.Select(x => x.GetQuery()),
                Should = this._shuld.Select(x => x.GetQuery())
            };
        }
    }
}
