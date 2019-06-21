using Jwell.Domain.Entities;
using Jwell.Framework.Domain.Repositories;

namespace Jwell.Repository.Repositories
{
    /// <summary>
    /// 团队信息仓储-接口
    /// </summary>
    public interface ITeamRepository: IRepository<Team, long>
    {
    }
}
