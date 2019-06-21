using Jwell.Domain.Entities;
using Jwell.Modules.EntityFramework.Repositories;
using Jwell.Modules.EntityFramework.Uow;
using Jwell.Repository.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Repository.Repositories
{
    /// <summary>
    /// 索引信息管理仓储
    /// </summary>
    public class IndexManagerRepository : RepositoryBase<IndexManager, JwellDbContext, long>, IIndexManagerRepository
    {
        public IndexManagerRepository(IDbContextResolver dbContextResolver) : base(dbContextResolver)
        {
        }

        public bool AddWithTransection(IndexManager entity, Action action)
        {
            var isSuccess = false;
            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    this.Set.Add(entity);
                    isSuccess = this.DbContext.SaveChanges() > 0;
                    if (!isSuccess)
                        throw new Exception("indexManager添加失败");
                    action.Invoke();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return false;
                }
            }
            return isSuccess;
        }

        public bool ModifyWithTransection(IndexManager entity, Action action)
        {
            var isSuccess = false;
            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    DbContext.Entry(entity).State = EntityState.Modified;
                    isSuccess = this.DbContext.SaveChanges() > 0;
                    if (!isSuccess)
                        throw new Exception("indexManager更新失败");
                    action.Invoke();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return false;
                }
            }
            return isSuccess;
        }
    }
}
