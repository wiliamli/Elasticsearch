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
    /// term查询谓语
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TV"></typeparam>
    public class TermQueryPredicate<T, TV> : ITermQueryPredicate<T, TV> where T : SearchEntityBase
    {
        public TermQueryPredicate(Expression<Func<T, TV>> expression, TV value)
        {
            this._expression = expression;
            this._value = value;
        }

        private Expression<Func<T, TV>> _expression { get; set; }
        private TV _value { get; set; }

        public QueryContainer GetQuery()
        {
            string field = LoadFieldHelper.GetFiledStr<T, TV>(this._expression);
            return new QueryContainerDescriptor<T>().Term(field, this._value);
        }
    }
}
