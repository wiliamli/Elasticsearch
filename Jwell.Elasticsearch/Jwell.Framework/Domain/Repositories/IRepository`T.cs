using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jwell.Framework.Domain.Entities;

namespace Jwell.Framework.Domain.Repositories
{
    public interface IRepository<T,TPrimaryKey> : IRepository where T : Entity<TPrimaryKey>
    {
        IQueryable<T> Queryable();

        int Add(T entity);

        int Update(T entity);

        int Delete(T entity);
    }
}
