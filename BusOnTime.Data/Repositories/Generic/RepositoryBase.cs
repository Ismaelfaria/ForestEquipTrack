using BusOnTime.Data.DataContext;
using BusOnTime.Data.Interfaces.Generic;

namespace BusOnTime.Data.Repositories.Generic
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly Context _context;

        public RepositoryBase(Context context)
        {
            _context = context;
        }

        public virtual TEntity Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual IEnumerable<TEntity> FindAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public virtual void Delete(Guid id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
            }
        }
        public virtual void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
        }
        public virtual TEntity GetById(Guid id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }
    }
}