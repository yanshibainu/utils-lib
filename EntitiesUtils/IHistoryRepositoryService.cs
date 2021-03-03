namespace utils_lib.EntitiesUtils
{
    public interface IHistoryRepositoryService<out TEntity, THistoryEntity>
    {
        THistoryEntity ToHistory(object entity);

        TEntity HistoryBack(THistoryEntity entity);
    }
}