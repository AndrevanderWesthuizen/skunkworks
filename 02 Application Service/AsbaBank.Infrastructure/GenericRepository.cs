using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AsbaBank.Core;

namespace AsbaBank.Infrastructure
{
    sealed class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly PropertyInfo identityPropertyInfo;

        private IDbContext context;
        public GenericRepository(IDbContext context)
        {
            this.context = context;
            identityPropertyInfo = GetIdentityPropertyInformation();
        }

        private IDbSet<TEntity> dbSet
        {
            get
            {
                return context.Set<TEntity>();
            }
        }

        private PropertyInfo GetIdentityPropertyInformation() 
        {
            return typeof(TEntity)
                .GetProperties()
                .Single(propertyInfo => Attribute.IsDefined(propertyInfo, typeof(KeyAttribute)));
        }

        public TEntity Get(object id)
        {
            return dbSet
               .AsQueryable()
               .SingleOrDefault(WithMatchingId(id));
        }

        public void Update(object id, TEntity item)
        {
            throw new NotImplementedException();
        }

        public void Add(TEntity item)
        {
            dbSet.Add(item);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(TEntity item)
        {
            return dbSet.Contains(item);
        }

        public void CopyTo(TEntity[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return dbSet.Count(); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(TEntity item)
        {
            dbSet.Remove(item);
            return true;
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return dbSet.AsQueryable().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private Func<TEntity, bool> WithMatchingId(object id)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "x");
            Expression property = Expression.Property(parameter, identityPropertyInfo.Name);
            Expression target = Expression.Constant(id);
            Expression equalsMethod = Expression.Equal(property, target);
            Func<TEntity, bool> predicate = Expression.Lambda<Func<TEntity, bool>>(equalsMethod, parameter).Compile();

            return predicate;
        }
    }
}
