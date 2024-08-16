using BusOnTime.Application.Interfaces;
using BusOnTime.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Application.Services
{
    internal class EquipmentPositionHistoryS : IEquipmentPositionHistoryS
    {
        public Task<EquipmentPositionHistory> CreateAsync(EquipmentPositionHistory entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EquipmentPositionHistory>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EquipmentPositionHistory> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(EquipmentPositionHistory entity)
        {
            throw new NotImplementedException();
        }
    }
}
