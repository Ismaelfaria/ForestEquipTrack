using BusOnTime.Application.Interfaces;
using BusOnTime.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Application.Services
{
    internal class EquipmentStateHistoryS : IEquipmentStateHistoryS
    {
        public Task<EquipmentStateHistory> CreateAsync(EquipmentStateHistory entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EquipmentStateHistory>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EquipmentStateHistory> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(EquipmentStateHistory entity)
        {
            throw new NotImplementedException();
        }
    }
}
