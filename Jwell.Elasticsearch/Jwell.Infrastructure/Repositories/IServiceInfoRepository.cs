using Jwell.Domain.Entities;
using Jwell.Framework.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Repository.Repositories
{
    /// <summary>
    /// 服务管理信息仓储-接口
    /// </summary>
    public interface IServiceInfoRepository: IRepository<ServiceInfo, long>
    {
    }
}
