using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace utils_lib.EntitiesUtils
{
    public interface IRepository<TEntity, TKey>
        where TEntity : class
    {
        TKey Create(TEntity entity);

        IList<TKey> Create(IList<TEntity> entityList);

        TEntity Update(TKey id, object entity);

        IList<TEntity> Update(IEnumerable<object> entityList);

        void Delete(TKey id);

        void Delete(IEnumerable<TKey> idList);

        TEntity FindById(TKey id);

        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression);

        IQueryable<TEntity> All();
    }
}