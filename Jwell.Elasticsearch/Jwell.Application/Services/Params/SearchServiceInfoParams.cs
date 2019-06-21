using Jwell.Framework.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Params
{
    /// <summary>
    /// 服务信息查询参数
    /// </summary>
    public class SearchServiceInfoParams : PageParam
    {
        /// <summary>
        /// 服务标识
        /// </summary>
        public string ServiceCode { get; set; }
    }
}
