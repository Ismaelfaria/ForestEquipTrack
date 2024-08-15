using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Application.Interfaces.Generic
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
        TEntity GetById(Guid id);
        IEnumerable<TEntity> FindAll();
        TEntity Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(Guid id);
    }
}
