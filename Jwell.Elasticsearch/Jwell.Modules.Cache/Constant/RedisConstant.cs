using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Cache
{
    /// <summary>
    /// Redis配置常量
    /// 等统一配置出来，读统一配置的信息
    /// </summary>
    internal static class RedisConstant
    {
        /// <summary>
        /// 客户端名称
        /// </summary>
        internal static string CLIENTNAME => "REDISCACHE";

        /// <summary>
        /// redis密码
        /// </summary>
        internal static string PASSWORD => "qazwsxedc";

        /// <summary>
        /// redis的IP地址
        /// </summary>
        //internal static string IP => "10.130.0.254";

        internal static string IP => "10.130.0.71";

        /// <summary>
        /// 默认端口
        /// </summary>
        internal static int PORT => 6379;

        /// <summary>
        /// timespan基数，设置到秒
        /// </summary>
        internal static long TICKCARDINAL = 10000000;
    }
}
