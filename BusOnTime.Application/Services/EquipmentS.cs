using BusOnTime.Application.Interfaces;
using BusOnTime.Data.Entities;
using BusOnTime.Data.Interfaces.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Application.Services
{
    internal class EquipmentS : IEquipmentS
    {
        private readonly IEquipmentR equipmentR;
        public EquipmentS(IEquipmentR _equipmentR)
        {
            equipmentR = _equipmentR;
        }
        public async Task<Equipment> CreateAsync(Equipment entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));

                entity.EquipmentId = Guid.NewGuid();

                return await equipmentR.CreateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentS/CreateAsync", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.", nameof(id));

                await equipmentR.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentS/DeleteAsync", ex);
            }
        }

        public async Task<IEnumerable<Equipment>> FindAllAsync()
        {
            try
            {
                return await equipmentR.FindAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentS/FindAllAsync", ex);
            }
        }

        public async Task<Equipment> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.", nameof(id));

                return await equipmentR.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentS/GetByIdAsync", ex);
            }
        }

        public async Task UpdateAsync(Equipment entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));

                await equipmentR.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentS/UpdateAsync", ex);
            }
        }
    }
}
