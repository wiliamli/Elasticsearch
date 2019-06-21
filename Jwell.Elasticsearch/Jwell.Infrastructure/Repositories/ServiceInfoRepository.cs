using Jwell.Domain.Entities;
using Jwell.Modules.EntityFramework.Repositories;
using Jwell.Modules.EntityFramework.Uow;
using Jwell.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Repository.Repositories
{
    /// <summary>
    /// 服务信息管理仓储
    /// </summary>
    public class ServiceInfoRepository: RepositoryBase<ServiceInfo, JwellDbContext, long>, IServiceInfoRepository
    {
        public ServiceInfoRepository(IDbContextResolver dbContextResolver) : base(dbContextResolver)
        {
        }
    }
}
