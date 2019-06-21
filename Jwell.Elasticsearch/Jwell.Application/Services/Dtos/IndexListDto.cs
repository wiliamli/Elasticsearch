using Jwell.Domain.Entities;
using Jwell.Framework.Paging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Dtos
{
    /// <summary>
    /// 索引Dto
    /// </summary>
    public class IndexListDto
    {
        /// <summary>
        /// 索引编号
        /// </summary>
        public string IndexNumber { get; set; }

        /// <summary>
        /// 服务编号
        /// </summary>
        public string ServiceSign { get; set; }

        /// <summary>
        /// 索引名称
        /// </summary>
        public string IndexName { get; set; }

        /// <summary>
        /// 索引状态
        /// </summary>
        public string Status { get; set; }

        [JsonIgnore]
        public IndexStatus EnumStatus { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string ModifiedTime { get; set; }

        [JsonIgnore]
        public DateTime LastModifiedTime { get; set; }
    }

    public static class IndexListDtoExt
    {
        public static PageResult<IndexListDto> ToPageDtos(this IQueryable<IndexListDto> query, PageParam pageParam)
        {
            var pageResult = new PageResult<IndexListDto>(query.OrderBy(pageParam.Sort, pageParam.SortDirection), pageParam.PageIndex, pageParam.PageSize);
            pageResult.Pager.ForEach(e =>
            {
                e.ModifiedTime = e.LastModifiedTime.ToString("yyyy-MM-dd");
                e.Status = e.EnumStatus.GetDescription();
            });
            return pageResult;
        }
    }
}
