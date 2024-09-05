using System.Linq.Expressions;

namespace BusOnTime.Infrastructure.Interfaces.Generic
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> FindAllAsync();
        Task<TEntity> CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
