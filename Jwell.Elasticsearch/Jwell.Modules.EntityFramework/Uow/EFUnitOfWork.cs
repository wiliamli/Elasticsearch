using Autofac;
using System;
using System.Data.Entity;
using Jwell.Framework.Domain.Entities;
using Jwell.Framework.Domain.Repositories;
using Jwell.Framework.Domain.Uow;
using Jwell.Framework.Ioc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
#if DEBUG
using System.Diagnostics;
#endif

namespace Jwell.Modules.EntityFramework.Uow
{
    [Transient]
    public class EFUnitOfWork : IUnitOfWork
    {
        private string _id;

        public string Id
        {
            get
            {
                return _id;
            }
        }

        private ILifetimeScope _lifetime;

        public event EventHandler Disposed;

        private Dictionary<string, DbContext> _activeDbContexts = new Dictionary<string, DbContext>();

        private Dictionary<string, DbContextTransaction> _activeTransactions = new Dictionary<string, DbContextTransaction>();

        public UnitOfWorkOptions Options { get; private set; }

        public EFUnitOfWork(ILifetimeScope lifetime, UnitOfWorkOptions options)
        {
            if (lifetime == null)
            {
                throw new ArgumentNullException(nameof(lifetime));
            }

            _id = Guid.NewGuid().ToString();
            _lifetime = lifetime;
            Options = options;

#if DEBUG
            Debug.WriteLine("EFUnitOfWork:" + _id + " created");
#endif
        }

        public void Commit()
        {
            try
            {
                foreach (var context in GetAllActiveContexts())
                {
                    context.SaveChanges();
                }

                if (Options.UseTransaction)
                {
                    foreach (var tran in GetAllActiveTransactions())
                    {
                        tran.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                foreach (var context in GetAllActiveContexts())
                {
                    await context.SaveChangesAsync();
                }

                if (Options.UseTransaction)
                {
                    foreach (var tran in GetAllActiveTransactions())
                    {
                        tran.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
        }

        public void Rollback()
        {
            foreach (var tran in GetAllActiveTransactions())
            {
                tran.Rollback();
            }
        }

        public void Dispose()
        {
            foreach (var context in GetAllActiveContexts())
            {
                try
                {
                    context.Dispose();
                }
                catch
                {

                }
            }
            foreach (var tran in GetAllActiveTransactions())
            {
                try
                {
                    tran.Dispose();
                }
                catch
                {

                }
            }

            this.Disposed?.Invoke(this, new EventArgs());

#if DEBUG
            Debug.WriteLine("EFUnitOfWork:" + _id + " disposed");
#endif
        }

        public TDbContext GetOrCreateContext<TDbContext>() where TDbContext : DbContext
        {
            Type contextType = typeof(TDbContext);

            var dbContextKey = contextType.FullName;

            DbContext dbContext;
            if (!_activeDbContexts.TryGetValue(dbContextKey, out dbContext))
            {
                dbContext = _lifetime.Resolve<TDbContext>();
                _activeDbContexts[dbContextKey] = dbContext;

                if (Options.UseTransaction)
                {
                    DbContextTransaction transaction = dbContext.Database.BeginTransaction();
                    _activeTransactions[dbContextKey] = transaction;
                }
            }

            return (TDbContext)dbContext;
        }

        public IReadOnlyList<DbContext> GetAllActiveContexts()
        {
            return _activeDbContexts.Values.ToList();
        }

        public IReadOnlyList<DbContextTransaction> GetAllActiveTransactions()
        {
            return _activeTransactions.Values.ToList();
        }
    }
}
