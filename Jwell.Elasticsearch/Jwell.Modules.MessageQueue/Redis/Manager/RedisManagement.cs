using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.MessageQueue.Redis.Manager
{
    internal class RedisManagement
    {
        internal static ConnectionMultiplexer RedisClient { get; private set; }

        #region 静态单例 
        static RedisManagement()
        {
            RedisClient = ConnectionMultiplexer.Connect($"{RedisConstant.IP}:{RedisConstant.PORT},password={RedisConstant.PASSWORD}");
        }
        #endregion
    }
}
