using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Namotion.Reflection;

namespace utils_lib.EntitiesUtils
{
    public abstract class AbstractRepositoryService<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class
    {
        protected readonly DbContext Context;

        protected AbstractRepositoryService(DbContext context)
        {
            Context = context;
        }

        public virtual TKey Create(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            Context.SaveChanges();

            return entity.TryGetPropertyValue<TKey>("Id");
        }

        public virtual IQueryable<TKey> Create(IList<TEntity> entityList)
        {
            Context.Set<TEntity>().AddRange(entityList);
            Context.SaveChanges();

            return entityList.Select(e => e.TryGetPropertyValue<TKey>("Id")).AsQueryable();
        }

        public virtual void Update(TKey id, object entity)
        {
            var oldEntity = FindById(id);
            Context.Entry(oldEntity).CurrentValues.SetValues(entity);

            var dictionary = TypeDescriptor.GetProperties(entity)
                .Cast<PropertyDescriptor>()
                .ToDictionary(property => property.Name,
                    property => property.GetValue(entity));

            if (Context.Entry(oldEntity).Navigations.Any())
            {
                foreach (var navigationEntry in Context.Entry(oldEntity).Navigations)
                {
                    if (dictionary.ContainsKey(navigationEntry.Metadata.Name))
                        navigationEntry.CurrentValue = dictionary[navigationEntry.Metadata.Name];
                }
            }

            Context.SaveChanges();
        }

        public virtual IQueryable<TKey> Update(IEnumerable<object> entitys)
        {
            foreach (var entity in entitys)
            {                
                if(entity.HasProperty("Id"))
                {
                    TKey id = (TKey)entity.GetType().GetProperty("Id").GetValue(entity, null);
                    var oldEntity = FindById(id);
                    Context.Entry(oldEntity).CurrentValues.SetValues(entity);
                }
            }

            Context.SaveChanges();
            return entitys.Select(e => e.TryGetPropertyValue<TKey>("Id")).AsQueryable();
        }

        public virtual void Delete(TKey id)
        {
            Context.Remove(Context.Set<TEntity>().Find(id));
            Context.SaveChanges();
        }
        public virtual void Delete(IEnumerable<TKey> idList)
        {
            foreach (var id in idList)
            {
                Context.Remove(Context.Set<TEntity>().Find(id));
            }
            Context.SaveChanges();
        }

        public virtual TEntity FindById(TKey id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            return Context.Set<TEntity>().Where(expression);
        }

        public virtual IQueryable<TEntity> All()
        {
            return Context.Set<TEntity>().AsNoTracking();
        }
    }
}