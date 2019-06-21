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
    /// 小组信息仓储
    /// </summary>
    public class TeamRepository: RepositoryBase<Team, JwellDbContext, long>, ITeamRepository
    {
        public TeamRepository(IDbContextResolver dbContextResolver) : base(dbContextResolver)
        {
        }
    }
}
