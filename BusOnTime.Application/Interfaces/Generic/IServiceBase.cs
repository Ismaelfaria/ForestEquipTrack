using BusOnTime.Application.Mapping.DTOs.InputModel;
using BusOnTime.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Application.Interfaces.Generic
{
    public interface IServiceBase<TEntity, TViewMode> where TEntity : class
    {
        Task<TViewMode> CreateAsync(TEntity entity);
        Task UpdateAsync(Guid id, TEntity entity);
        Task<TViewMode> GetByIdAsync(Guid id);
        Task<IEnumerable<TViewMode>> FindAllAsync();
        Task DeleteAsync(Guid id);
    }
}
