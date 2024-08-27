using BusOnTime.Application.Interfaces.Generic;
using BusOnTime.Application.Mapping.DTOs.InputModel;
using BusOnTime.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Application.Interfaces
{
    public interface IEquipmentStateHistoryS : IServiceBase<EquipmentStateHistory>
    {
        Task<EquipmentStateHistory> CreateAsync(EquipmentStateHistoryIM entity);
        Task UpdateAsync(Guid id, EquipmentStateHistoryIM entity);
    }
}
