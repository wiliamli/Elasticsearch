using Jwell.Modules.Elasticsearch.Entity;
using Jwell.Modules.Elasticsearch.Helper;
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
    /// 范围查询
    /// </summary>
    /// <typeparam name="T">查询实体</typeparam>
    /// <typeparam name="TV">查询属性</typeparam>
    public class RangeQueryPredicate<T, TV> : IRangeQueryPredicate<T, TV>
        where T : SearchEntityBase
        where TV : struct
    {
        public RangeQueryPredicate(Expression<Func<T, TV>> expression,
            TV? GreaterThan = null,
            TV? GreaterThanOrEqualTo = null,
            TV? LessThan = null,
            TV? LessThanOrEqualTo = null)
        {
            this._expression = expression;
            this._greaterThan = GreaterThan;
            this._greaterThanOrEqualTo = GreaterThanOrEqualTo;
            this._lessThan = LessThan;
            this._lessThanOrEqualTo = LessThanOrEqualTo;
        }

        private TV? _greaterThanOrEqualTo { get; set; }
        private TV? _lessThanOrEqualTo { get; set; }
        private TV? _greaterThan { get; set; }
        private TV? _lessThan { get; set; }
        private Expression<Func<T, TV>> _expression { get; set; }

        public QueryContainer GetQuery()
        {
            string field = LoadFieldHelper.GetFiledStr<T, TV>(this._expression);
            if (typeof(TV) == typeof(DateTime))
            {
                var rangQuery = new DateRangeQuery
                {
                    Field = field,
                    GreaterThanOrEqualTo = this._greaterThanOrEqualTo.HasValue ? DateTime.Parse(this._greaterThanOrEqualTo.Value.ToString()) : default(DateTime?),
                    GreaterThan = this._greaterThan.HasValue ? DateTime.Parse(this._greaterThan.Value.ToString()) : default(DateTime?),
                    LessThan = this._lessThan.HasValue ? DateTime.Parse(this._lessThan.Value.ToString()) : default(DateTime?),
                    LessThanOrEqualTo = this._lessThanOrEqualTo.HasValue ? DateTime.Parse(this._lessThanOrEqualTo.Value.ToString()) : default(DateTime?),
                    TimeZone = "Asia/Shanghai"
                };
                return rangQuery;
            }
            return new NumericRangeQuery
            {
                Field = field,
                GreaterThanOrEqualTo = this._greaterThanOrEqualTo.HasValue ? double.Parse(this._greaterThanOrEqualTo.Value.ToString()) : default(double?),
                GreaterThan = this._greaterThan.HasValue ? double.Parse(this._greaterThan.Value.ToString()) : default(double?),
                LessThan = this._lessThan.HasValue ? double.Parse(this._lessThan.Value.ToString()) : default(double?),
                LessThanOrEqualTo = this._lessThanOrEqualTo.HasValue ? double.Parse(this._lessThanOrEqualTo.Value.ToString()) : default(double?)
            };
        }
    }
}
