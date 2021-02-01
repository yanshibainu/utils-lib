using Microsoft.EntityFrameworkCore;

namespace utils_lib.ServicesUtils
{
    public abstract class AbstractHistoryRepositoryService<TEntity, TKey, THistoryEntity> : AbstractRepositoryService<THistoryEntity, TKey>, IHistoryRepositoryService<TEntity, THistoryEntity>
        where THistoryEntity : class, new() where TEntity : class, new()
    {
        protected readonly DbContext Context;

        protected AbstractHistoryRepositoryService(DbContext context) : base(context)
        {
            Context = context;
        }

        public THistoryEntity ToHistory(TEntity entity)
        {
            var newEntity = Context.Entry(new THistoryEntity());
            newEntity.CurrentValues.SetValues(entity);
            Context.Add(newEntity.Entity);
            Context.SaveChanges();

            return newEntity.Entity;
        }

        public TEntity HistoryBack(THistoryEntity entity)
        {
            var newEntity = Context.Entry(new TEntity());
            newEntity.CurrentValues.SetValues(entity);
            Context.Add(newEntity.Entity);
            Context.SaveChanges();

            return newEntity.Entity;
        }
    }
}