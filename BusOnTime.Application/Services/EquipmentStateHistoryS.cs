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
    public class EquipmentStateHistoryS : IEquipmentStateHistoryS
    {
        private readonly IEquipmentStateHistoryR equipmentStateHistoryR;
        public EquipmentStateHistoryS(IEquipmentStateHistoryR _equipmentStateHistoryR)
        {
            equipmentStateHistoryR = _equipmentStateHistoryR;
        }
        public async Task<EquipmentStateHistory> CreateAsync(EquipmentStateHistory entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));

                return await equipmentStateHistoryR.CreateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentStateHistoryS/CreateAsync", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.");

                await equipmentStateHistoryR.DeleteAsync(id);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentStateHistoryS/DeleteAsync", ex);
            }

        }

        public async Task<IEnumerable<EquipmentStateHistory>> FindAllAsync()
        {
            try
            {
                return await equipmentStateHistoryR.FindAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentStateHistoryS/FindAllAsync", ex);
            }
        }

        public async Task<EquipmentStateHistory> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.");

                return await equipmentStateHistoryR.GetByIdAsync(id);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentStateHistoryS/FindByIdAsync", ex);
            }
        }

        public async Task UpdateAsync(EquipmentStateHistory entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));

                await equipmentStateHistoryR.UpdateAsync(entity);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentStateHistoryS/UpdateAsync", ex);
            }
        }
    }
}
