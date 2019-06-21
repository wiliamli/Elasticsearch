using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Jwell.Modules.Cache;
using Jwell.Modules.Elasticsearch.Entity;
using Jwell.Modules.Elasticsearch.Helper;
using Jwell.Modules.Elasticsearch.SearchCondition;
using Jwell.Modules.Elasticsearch.SearchProvider.Client;
using Nest;

namespace Jwell.Modules.Elasticsearch.SearchProvider.Provider
{
    /// <summary>
    /// 全文检索查询
    /// </summary>
    public class FullTextSearchProvider : IFullTextSearchProvider
    {
        private readonly ElasticClient Client;
        private ICacheClient CacheClient { get; set; }
        private IndexOperate IndexOperate { get; set; }
        public FullTextSearchProvider(ICacheClient cacheClient)
        {
            this.CacheClient = cacheClient;
            this.Client = ElasticsearchClient.Instance;
            this.IndexOperate = new IndexOperate(this.Client, this.CacheClient);
        }

        /// <summary>
        /// 全文检索
        /// </summary>
        /// <typeparam name="T">查询模型类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public IEnumerable<T> FullTextSearch<T>(IFullTextSearchCondition<T> condition) where T : SearchEntityBase
        {
            var indexMappingName = IndexNameHelper.GetIndexMappingName(SetupConfig.SetupConfig.ServiceSign, condition.Index);
            var status = this.CacheClient.GetCache<IndexOperateStatus>(indexMappingName);
            this.IndexOperate.CkeckIndexStatus<T>(status, indexMappingName);

            if (condition.SearchFields == null || condition.SearchFields.Length == 0)
                throw new ElasticsearchException("全文检索失败：查询字段不能为空");
            
            var searchRequest = new SearchRequest<T>(indexMappingName);

            searchRequest.Size = condition.SizeNumber;
            searchRequest.From = condition.SkipNumber;

            var boolQuery = new BoolQuery();
            List<QueryContainer> queryContainers = new List<QueryContainer>();

            if (condition.SearchFields.Length > 1)
            {
                queryContainers.Add(new MultiMatchQuery { Fields = condition.SearchFields, Query = condition.SearchValue });
            }
            else
            {
                queryContainers.Add(new MatchQuery { Field = condition.SearchFields[0], Query = condition.SearchValue });
            }
            boolQuery.Must = queryContainers;
            if (condition.FilterPredicate != null)
            {
                boolQuery.Filter = new List<QueryContainer> { condition.FilterPredicate.GetQuery() };
            }

            searchRequest.Query = boolQuery;

            if (condition.Sort != null && condition.Sort.Count() > 0)
                searchRequest.Sort = condition.Sort.Select(x => (ISort)new SortField
                {
                    Field = x.Field,
                    Order = x.isAsc ? SortOrder.Ascending : SortOrder.Descending
                }).ToList();

            var highlight = new Highlight();
            var dic = new Dictionary<Field, IHighlightField>();
            foreach (var field in condition.SearchFields)
            {
                dic.Add(field, new HighlightField());
            }
            highlight.Fields = dic;

            searchRequest.Highlight = highlight;

            var response = Client.Search<T>(searchRequest);
            if (!response.IsValid)
                throw new ElasticsearchException("全文检索失败：" + response.OriginalException.Message);
            if (condition.IsHighLight)
                return response.Hits.Select(x => this.SetHightValue(x.Source, x.Highlights));
            return response.Hits.Select(x => x.Source);
        }

        private T SetHightValue<T>(T value, Dictionary<string, HighlightHit> dic) where T : class
        {
            Type ts = value.GetType();
            foreach (var hit in dic.Values)
            {
                foreach (var property in ts.GetProperties())
                {
                    var attr = property.GetCustomAttributes(typeof(PropertySearchNameAttribute), false).FirstOrDefault();
                    if (attr == null) break;
                    var field = ((PropertySearchNameAttribute)attr).Name;
                    if (hit.Field.Equals(field))
                        ts.GetProperty(property.Name).SetValue(value, hit.Highlights.FirstOrDefault());
                }
            }
            return value;
        }

        /// <summary>
        /// 全文检索
        /// </summary>
        /// <typeparam name="T">查询模型类型</typeparam>
        /// <param name="searchExpression">查询条件表达式</param>
        /// <returns></returns>
        public IEnumerable<T> FullTextSearch<T>(Expression<Func<IFullTextSearchCondition<T>, IFullTextSearchCondition<T>>> searchExpression) where T : SearchEntityBase
        {
            var condition = searchExpression.Compile().Invoke(new FullTextSearchCondition<T>());
            return this.FullTextSearch<T>(condition);
        }

        /// <summary>
        /// 全文检索
        /// </summary>
        /// <typeparam name="T">查询模型类型</typeparam>
        /// <typeparam name="TR">查询结果模型类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public IEnumerable<TR> FullTextSearch<T, TR>(IFullTextSearchCondition<T, TR> condition)
            where T : SearchEntityBase
            where TR : class
        {
            var indexMappingName = IndexNameHelper.GetIndexMappingName(SetupConfig.SetupConfig.ServiceSign, condition.Index);
            var status = this.CacheClient.GetCache<IndexOperateStatus>(indexMappingName);
            this.IndexOperate.CkeckIndexStatus<T>(status, indexMappingName);

            if (condition.SearchFields == null || condition.SearchFields.Length == 0)
                throw new ElasticsearchException("全文检索失败：查询字段不能为空");

            if (condition.Selector == null)
                throw new ElasticsearchException("全文检索失败：未设置查询结果类型转换表达式");
            
            var searchRequest = new SearchRequest<T>(indexMappingName);

            searchRequest.Size = condition.SizeNumber;
            searchRequest.From = condition.SkipNumber;

            var boolQuery = new BoolQuery();
            List<QueryContainer> queryContainers = new List<QueryContainer>();

            if (condition.SearchFields.Length > 1)
            {
                queryContainers.Add(new MultiMatchQuery { Fields = condition.SearchFields, Query = condition.SearchValue });
            }
            else
            {
                queryContainers.Add(new MatchQuery { Field = condition.SearchFields[0], Query = condition.SearchValue });
            }
            boolQuery.Must = queryContainers;
            if (condition.FilterPredicate != null)
            {
                boolQuery.Filter = new List<QueryContainer> { condition.FilterPredicate.GetQuery() };
            }

            searchRequest.Query = boolQuery;

            if (condition.Sort != null && condition.Sort.Count() > 0)
                searchRequest.Sort = condition.Sort.Select(x => (ISort)new SortField
                {
                    Field = x.Field,
                    Order = x.isAsc ? SortOrder.Ascending : SortOrder.Descending
                }).ToList();

            var properties = LoadFieldHelper.GetProperty(condition.Selector);
            var sourefilter = new SourceFilter()
            {
                Includes = properties
            };
            searchRequest.Source = new Union<bool, ISourceFilter>(sourefilter);

            var highlight = new Highlight();
            var dic = new Dictionary<Field, IHighlightField>();
            foreach (var field in condition.SearchFields)
            {
                dic.Add(field, new HighlightField());
            }
            highlight.Fields = dic;

            searchRequest.Highlight = highlight;

            var response = this.Client.Search<T>(searchRequest);
            if (!response.IsValid)
                throw new ElasticsearchException("全文检索失败：" + response.OriginalException.Message);

            if (condition.IsHighLight)
                return response.Hits.Select(x => this.SetHightValue(x.Source, x.Highlights)).Select(condition.Selector.Compile());

            return response.Hits.Select(x => x.Source).Select(condition.Selector.Compile());
        }

        /// <summary>
        /// 全文检索
        /// </summary>
        /// <typeparam name="T">查询模型类型</typeparam>
        /// <typeparam name="TR">查询结果模型类型</typeparam>
        /// <param name="searchExpression">查询条件表达式</param>
        /// <returns></returns>
        public IEnumerable<TR> FullTextSearch<T, TR>(Expression<Func<IFullTextSearchCondition<T, TR>, IFullTextSearchCondition<T, TR>>> searchExpression)
            where T : SearchEntityBase
            where TR : class
        {
            var condition = searchExpression.Compile().Invoke(new FullTextSearchCondition<T, TR>());
            return this.FullTextSearch(condition);
        }
    }
}
