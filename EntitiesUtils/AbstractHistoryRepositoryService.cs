using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace utils_lib.EntitiesUtils
{
    public abstract class AbstractHistoryRepositoryService<TEntity, TKey, THistoryEntity> : AbstractRepositoryService<THistoryEntity, TKey>, IHistoryRepositoryService<TEntity, THistoryEntity>
        where THistoryEntity : class, new() where TEntity : class, new()
    {
        protected readonly IMapper _mapper;

        protected AbstractHistoryRepositoryService(DbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }       

        public THistoryEntity ToHistory(TEntity entity)
        {
            var newEntity = _mapper.Map<THistoryEntity>(entity);
            Context.Add(newEntity);
            Context.SaveChanges();

            return newEntity;
        }

        public TEntity HistoryBack(THistoryEntity entity)
        {
            var newEntity = _mapper.Map<TEntity>(entity);
            Context.Add(newEntity);
            Context.SaveChanges();

            return newEntity;
        }
    }
}