using Jwell.Framework.Application.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services
{
    /// <summary>
    /// 缓存数据同步-服务-接口
    /// </summary>
    public interface ISyncCacheDataService : IApplicationService
    {
        /// <summary>
        /// 同步数据到缓存
        /// </summary>
        /// <param name="retryTime">重试次数</param>
        /// <returns></returns>
        void SyncCacheData(int retryTime = 2);
    }
}
