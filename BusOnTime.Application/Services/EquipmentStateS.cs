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
    public class EquipmentStateS : IEquipmentStateS
    {
        private readonly IEquipmentStateR equipmentStateR;
        public EquipmentStateS(IEquipmentStateR _equipmentStateR)
        {
            equipmentStateR = _equipmentStateR;
        }
        public async Task<EquipmentState> CreateAsync(EquipmentState entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));

                return await equipmentStateR.CreateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentStateS/CreateAsync", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.", nameof(id));

                await equipmentStateR.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentStateS/DeleteAsync", ex);
            }

        }

        public async Task<IEnumerable<EquipmentState>> FindAllAsync()
        {
            try
            {
                return await equipmentStateR.FindAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentStateS/FindAllAsync", ex);
            }
        }

        public async Task<EquipmentState> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.", nameof(id));

                return await equipmentStateR.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentStateS/FindByIdAsync", ex);
            }
        }

        public async Task UpdateAsync(EquipmentState entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));

                await equipmentStateR.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentStateS/UpdateAsync", ex);
            }
        }
    }
}
