using System;
using System.Data.Entity;
using AsbaBank.Core;
using AsbaBank.DataModel;

namespace AsbaBank.Infrastructure
{
    public class GenericUnitOfWork : IUnitOfWork
    {
        private DbContext context;
        private bool isDisposed;

        public GenericUnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Rollback()
        {
            context.Dispose();
            context = new AsbaContext("ASBA");
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
