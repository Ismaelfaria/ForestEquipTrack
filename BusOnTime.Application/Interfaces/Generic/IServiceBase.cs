namespace BusOnTime.Application.Interfaces.Generic
{
    public interface IServiceBase<TEntity, TViewMode> where TEntity : class
    {
        Task<TViewMode> CreateAsync(TEntity entity);
        Task UpdateAsync(Guid? id, TEntity entity);
        Task<TViewMode> GetByIdAsync(Guid id);
        Task<IEnumerable<TViewMode>> FindAllAsync();
        Task DeleteAsync(Guid id);
    }
}
