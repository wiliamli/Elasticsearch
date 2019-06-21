using Jwell.Framework.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Params
{
    /// <summary>
    /// 索引分页查询参数
    /// </summary>
    public class SearchIndexParams : PageParam
    {
        /// <summary>
        /// 索引名称
        /// </summary>
        public string IndexName { get; set; }

        /// <summary>
        /// 服务编号
        /// </summary>
        public string ServiceNumber { get; set; }
    }
}
