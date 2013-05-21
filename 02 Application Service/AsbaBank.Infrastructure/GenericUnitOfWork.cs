using System;
using AsbaBank.Core;

namespace AsbaBank.Infrastructure
{
    public class GenericUnitOfWork : IUnitOfWork
    {
        private readonly IDbContext context;

        public GenericUnitOfWork(IDbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new GenericRepository<TEntity>(context);
        }
    }
}
