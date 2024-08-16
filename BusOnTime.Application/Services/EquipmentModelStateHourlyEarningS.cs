using BusOnTime.Application.Interfaces;
using BusOnTime.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Application.Services
{
    internal class EquipmentModelStateHourlyEarningS : IEquipmentModelStateHourlyEarningS
    {
        public Task<EquipmentModelStateHourlyEarnings> CreateAsync(EquipmentModelStateHourlyEarnings entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EquipmentModelStateHourlyEarnings>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EquipmentModelStateHourlyEarnings> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(EquipmentModelStateHourlyEarnings entity)
        {
            throw new NotImplementedException();
        }
    }
}
