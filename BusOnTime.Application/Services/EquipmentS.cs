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
    public class EquipmentS : IEquipmentS
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
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.");

                await equipmentR.DeleteAsync(id);
            }
            catch (ArgumentException)
            {
                throw;
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
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.");

                return await equipmentR.GetByIdAsync(id);
            }
            catch (ArgumentException)
            {
                throw;
            }catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentS/FindByIdAsync", ex);
            }
        }

        public async Task UpdateAsync(Equipment entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));

                await equipmentR.UpdateAsync(entity);
            }
            catch (ArgumentNullException)
            {
                throw;
            }catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentS/UpdateAsync", ex);
            }
        }
    }
}
