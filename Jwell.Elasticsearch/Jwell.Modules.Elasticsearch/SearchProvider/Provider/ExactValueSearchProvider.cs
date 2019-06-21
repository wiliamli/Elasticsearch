using Jwell.Modules.Cache;
using Jwell.Modules.Elasticsearch.Entity;
using Jwell.Modules.Elasticsearch.Helper;
using Jwell.Modules.Elasticsearch.SearchCondition;
using Jwell.Modules.Elasticsearch.SearchProvider.Client;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Elasticsearch.SearchProvider.Provider
{
    /// <summary>
    /// 结构化查询
    /// </summary>
    public class ExactValueSearchProvider : IExactValueSearchProvider
    {
        private readonly ElasticClient Client;
        private ICacheClient CacheClient { get; set; }
        private IndexOperate IndexOperate { get; set; }
        public ExactValueSearchProvider(ICacheClient cacheClient)
        {
            this.CacheClient = cacheClient;
            this.Client = ElasticsearchClient.Instance;
            this.IndexOperate = new IndexOperate(this.Client,this.CacheClient);
        }

        /// <summary>
        /// 结构化查询
        /// </summary>
        /// <typeparam name="T">查询模型类型</typeparam>
        /// <param name="searchCondition">查询条件</param>
        /// <returns></returns>
        public IEnumerable<T> ExactValueSearch<T>(IExactValueSearchCondition<T> searchCondition) where T : SearchEntityBase
        {
            var indexMappingName = IndexNameHelper.GetIndexMappingName(SetupConfig.SetupConfig.ServiceSign, searchCondition.Index);
            var status = this.CacheClient.GetCache<IndexOperateStatus>(indexMappingName);

            this.IndexOperate.CkeckIndexStatus<T>(status, indexMappingName);

            var queryContainer = searchCondition.QueryPredicate == null ? new MatchAllQuery()
                 : searchCondition.QueryPredicate.GetQuery();

            var searchRequest =  new SearchRequest<T>(indexMappingName);

            searchRequest.Query = new ConstantScoreQuery() { Filter = queryContainer };

            if (searchCondition.Sort != null && searchCondition.Sort.Count() > 0)
                searchRequest.Sort = searchCondition.Sort.Select(x => (ISort)new SortField
                {
                    Field = x.Field,
                    Order = x.isAsc ? SortOrder.Ascending : SortOrder.Descending
                }).ToList();

            searchRequest.Size = searchCondition.SizeNumber;
            searchRequest.From = searchCondition.SkipNumber;

            var response = this.Client.Search<T>(searchRequest);
            if (!response.IsValid)
                throw new ElasticsearchException("index查询异常：" + response.OriginalException.Message);

            return response.Hits.Select(x => x.Source);
        }

        /// <summary>
        /// 结构化查询
        /// </summary>
        /// <typeparam name="T">查询模型类型</typeparam>
        /// <param name="expression">查询条件表达式</param>
        /// <returns></returns>
        public IEnumerable<T> ExactValueSearch<T>(Expression<Func<IExactValueSearchCondition<T>, IExactValueSearchCondition<T>>> expression) where T : SearchEntityBase
        {
            var searchCondition = expression.Compile().Invoke(new ExactValueSearchCondition<T>());
            return this.ExactValueSearch<T>(searchCondition);
        }

        /// <summary>
        /// 结构化查询
        /// </summary>
        /// <typeparam name="T">查询模型类型</typeparam>
        /// <typeparam name="TR">查询结果模型类型</typeparam>
        /// <param name="searchCondition">查询条件</param>
        /// <returns></returns>
        public IEnumerable<TR> ExactValueSearch<T, TR>(IExactValueSearchCondition<T, TR> searchCondition)
            where T : SearchEntityBase
            where TR : class
        {
            var indexMappingName = IndexNameHelper.GetIndexMappingName(SetupConfig.SetupConfig.ServiceSign, searchCondition.Index);
            var status = this.CacheClient.GetCache<IndexOperateStatus>(indexMappingName);
            this.IndexOperate.CkeckIndexStatus<T>(status, indexMappingName);

            if (searchCondition.Selector == null)
                throw new ElasticsearchException("index查询异常：未设置查询结果类型转换表达式");

            var queryContainer = searchCondition.QueryPredicate == null ? new MatchAllQuery()
                 : searchCondition.QueryPredicate.GetQuery();

            var properties = LoadFieldHelper.GetProperty(searchCondition.Selector);
            var sourefilter = new SourceFilter()
            {
                Includes = properties
            };
            
            var searchRequest = new SearchRequest<T>(indexMappingName);

            searchRequest.Query = new ConstantScoreQuery() { Filter = queryContainer };
            searchRequest.Source = new Union<bool, ISourceFilter>(sourefilter);

            if (searchCondition.Sort != null && searchCondition.Sort.Count() > 0)
                searchRequest.Sort = searchCondition.Sort.Select(x => (ISort)new SortField
                {
                    Field = x.Field,
                    Order = x.isAsc ? SortOrder.Ascending : SortOrder.Descending
                }).ToList();

            searchRequest.Size = searchCondition.SizeNumber;
            searchRequest.From = searchCondition.SkipNumber;

            var response = this.Client.Search<T>(searchRequest);
            if (!response.IsValid)
                throw new ElasticsearchException("index查询异常：" + response.OriginalException.Message);

            return response.Hits.Select(x => x.Source).Select(searchCondition.Selector.Compile());
        }

        /// <summary>
        /// 结构化查询
        /// </summary>
        /// <typeparam name="T">查询模型类型</typeparam>
        /// <typeparam name="TR">查询结果模型类型</typeparam>
        /// <param name="expression">查询条件表达式</param>
        /// <returns></returns>
        public IEnumerable<TR> ExactValueSearch<T, TR>(Expression<Func<IExactValueSearchCondition<T, TR>, IExactValueSearchCondition<T, TR>>> expression)
            where T : SearchEntityBase
            where TR : class
        {
            var searchCondition = expression.Compile().Invoke(new ExactValueSearchCondition<T, TR>());
            return this.ExactValueSearch<T, TR>(searchCondition);
        }
    }
}
