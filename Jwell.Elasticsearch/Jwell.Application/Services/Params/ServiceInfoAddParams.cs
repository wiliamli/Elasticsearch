using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Params
{
    /// <summary>
    /// 服务信息添加参数
    /// </summary>
    public class ServiceInfoAddParams
    {
        /// <summary>
        /// 服务标识
        /// </summary>
        public string ServiceCode { get; set; }

        /// <summary>
        /// 服务编号
        /// </summary>
        public string ServiceNumber { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
