using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Params
{
    /// <summary>
    /// 登陆参数
    /// </summary>
    public class LoginParam
    {
        /// <summary>
        /// 团队标识
        /// </summary>
        public string TeamCode { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
