using System;
using System.Data.Entity;
using AsbaBank.Core;
using AsbaBank.DataModel;

namespace AsbaBank.Infrastructure.EntityFramework
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork, IDisposable
    {
        private DbContext context;
        private bool isDisposed;

        public EntityFrameworkUnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Rollback()
        {
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new EntityFrameworkRepository<TEntity>(context.Set<TEntity>());
        }

        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }

            isDisposed = true;
            context.Dispose();
        }
    }
}