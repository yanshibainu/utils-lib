﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace utils_lib.EntitiesUtils
{
    public interface IRepository<TEntity, TKey>
        where TEntity : class
    {
        TKey Create(TEntity entity);

        IQueryable<TKey> Create(IList<TEntity> entityList);

        void Update(TKey id, object entity);

        void Delete(TKey id);

        TEntity FindById(TKey id);

        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression);

        IQueryable<TEntity> All();
    }
}