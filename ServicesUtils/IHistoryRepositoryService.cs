namespace utils_lib.ServicesUtils
{
    public interface IHistoryRepositoryService<TEntity, THistoryEntity>
    {
        THistoryEntity ToHistory(TEntity entity);

        TEntity HistoryBack(THistoryEntity entity);
    }
}