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
    /// 索引信息管理仓储-接口
    /// </summary>
    public interface IIndexManagerRepository : IRepository<IndexManager, long>
    {
        bool AddWithTransection(IndexManager entity, Action action);
        bool ModifyWithTransection(IndexManager entity, Action action);
    }
}
