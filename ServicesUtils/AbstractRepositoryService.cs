using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Namotion.Reflection;
using utils_lib.EntitiesUtils;

namespace utils_lib.ServicesUtils
{
    public abstract class AbstractRepositoryService<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class
    {
        protected readonly DbContext Context;

        protected AbstractRepositoryService(DbContext context)
        {
            Context = context;
        }

        public TKey Create(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            Context.SaveChanges();

            return entity.TryGetPropertyValue<TKey>("Id");
        }

        public IQueryable<TKey> Create(IList<TEntity> entityList)
        {
            Context.Set<TEntity>().AddRange(entityList);
            Context.SaveChanges();

            return entityList.Select(e => e.TryGetPropertyValue<TKey>("Id")).AsQueryable();
        }

        public void Update(TKey id, object entity)
        {
            var oldEntity = FindById(id);
            Context.Entry(oldEntity).CurrentValues.SetValues(entity);
            Context.SaveChanges();
        }

        public void Delete(TKey id)
        {
            Context.Remove(Context.Set<TEntity>().Find(id));
            Context.SaveChanges();
        }

        public TEntity FindById(TKey id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            return Context.Set<TEntity>().Where(expression);
        }

        public IQueryable<TEntity> All()
        {
            return Context.Set<TEntity>().AsNoTracking();
        }
    }
}