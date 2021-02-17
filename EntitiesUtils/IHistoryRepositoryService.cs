namespace utils_lib.EntitiesUtils
{
    public interface IHistoryRepositoryService<TEntity, THistoryEntity>
    {
        THistoryEntity ToHistory(TEntity entity);

        TEntity HistoryBack(THistoryEntity entity);
    }
}