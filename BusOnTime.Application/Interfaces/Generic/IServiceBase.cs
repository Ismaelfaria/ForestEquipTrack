using BusOnTime.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Application.Interfaces.Generic
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(Guid id);
        Task UpdateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> FindAllAsync();
        Task DeleteAsync(Guid id);
    }
}
