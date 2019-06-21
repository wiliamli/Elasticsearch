using Jwell.Modules.Elasticsearch.Entity;
using Jwell.Modules.Elasticsearch.Helper;
using Nest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Elasticsearch.QueryPredicate
{
    /// <summary>
    /// terms查询谓语
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TV"></typeparam>
    public class TermsQueryPredicate<T, TV> : ITermsQueryPredicate<T, TV> where T : SearchEntityBase
    {
        public TermsQueryPredicate(Expression<Func<T, TV>> expression, params TV[] values)
        {
            this._expression = expression;
            this._values = values;
        }

        private Expression<Func<T, TV>> _expression { get; set; }
        private TV[] _values { get; set; }

        public QueryContainer GetQuery()
        {
            string field = LoadFieldHelper.GetFiledStr<T, TV>(this._expression);
            return new TermsQuery
            {
                Field = field,
                Terms = this._values.Select(x => (object)x)
            };
        }
    }
}
