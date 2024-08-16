using BusOnTime.Application.Interfaces;
using BusOnTime.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Application.Services
{
    internal class EquipmentStateS : IEquipmentStateS
    {
        public Task<EquipmentState> CreateAsync(EquipmentState entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EquipmentState>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EquipmentState> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(EquipmentState entity)
        {
            throw new NotImplementedException();
        }
    }
}
