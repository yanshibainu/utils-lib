namespace utils_lib.EntitiesUtils
{
    public interface IHistoryRepositoryService<TEntity, THistoryEntity>
    {
        THistoryEntity ToHistory(THistoryEntity entity);

        TEntity HistoryBack(THistoryEntity entity);
    }
}