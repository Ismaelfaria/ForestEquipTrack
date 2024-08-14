
namespace BusOnTime.Data.Interfaces.Generic
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(Guid id);
        IEnumerable<TEntity> FindAll();
        TEntity Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(Guid id);
    }
}
