using BusOnTime.Application.Interfaces;
using BusOnTime.Data.Entities;
using BusOnTime.Data.Interfaces.Interface;
using BusOnTime.Data.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Application.Services
{
    public class EquipmentModelStateHourlyEarningS : IEquipmentModelStateHourlyEarningS
    {
        private readonly IEquipmentModelStateHourlyEarningsR equipmentModelStateHourlyEarningsR;
        public EquipmentModelStateHourlyEarningS(IEquipmentModelStateHourlyEarningsR _equipmentModelStateHourlyEarningsR)
        {
            equipmentModelStateHourlyEarningsR = _equipmentModelStateHourlyEarningsR;
        }
        public async Task<EquipmentModelStateHourlyEarnings> CreateAsync(EquipmentModelStateHourlyEarnings entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));

                return await equipmentModelStateHourlyEarningsR.CreateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentModelStateHourlyEarningsS/CreateAsync", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.", nameof(id));

                await equipmentModelStateHourlyEarningsR.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentModelStateHourlyEarningsS/DeleteAsync", ex);
            }

        }

        public async Task<IEnumerable<EquipmentModelStateHourlyEarnings>> FindAllAsync()
        {
            try
            {
                return await equipmentModelStateHourlyEarningsR.FindAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentModelStateHourlyEarningsS/FindAllAsync", ex);
            }
        }

        public async Task<EquipmentModelStateHourlyEarnings> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.", nameof(id));

                return await equipmentModelStateHourlyEarningsR.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentModelStateHourlyEarningsS/FindByIdAsync", ex);
            }
        }

        public async Task UpdateAsync(EquipmentModelStateHourlyEarnings entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));

                await equipmentModelStateHourlyEarningsR.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentModelStateHourlyEarningsS/UpdateAsync", ex);
            }
        }
    }
}
