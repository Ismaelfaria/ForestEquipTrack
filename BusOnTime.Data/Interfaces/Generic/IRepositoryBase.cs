
namespace BusOnTime.Data.Interfaces.Generic
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        TEntity GetById(Guid id);
        IEnumerable<TEntity> FindAll();
        TEntity Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(Guid id);
    }
}
