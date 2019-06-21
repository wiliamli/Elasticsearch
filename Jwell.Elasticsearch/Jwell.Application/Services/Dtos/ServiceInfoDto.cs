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
    /// 服务信心Dto
    /// </summary>
    public class ServiceInfoDto
    {
        /// <summary>
        /// 服务编号
        /// </summary>
        public string ServiceNumber { get; set; }

        /// <summary>
        /// 服务标识
        /// </summary>
        public string ServiceCode { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string ModifiedTime { get; set; }

        [JsonIgnore]
        public DateTime LastModifiedTime { get; set; }
    }

    public static class ServiceInfoDtoExt
    {
        public static PageResult<ServiceInfoDto> ToPageDtos(this IQueryable<ServiceInfoDto> query, PageParam pageParam)
        {
            var pageResult = new PageResult<ServiceInfoDto>(query.OrderBy(pageParam.Sort, pageParam.SortDirection), pageParam.PageIndex, pageParam.PageSize);
            pageResult.Pager.ForEach(e => e.ModifiedTime = e.LastModifiedTime.ToString("yyyy-MM-dd"));
            return pageResult;
        }
    }
}
