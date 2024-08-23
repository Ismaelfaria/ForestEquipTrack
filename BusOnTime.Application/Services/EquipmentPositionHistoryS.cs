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
    public class EquipmentPositionHistoryS : IEquipmentPositionHistoryS
    {
        private readonly IEquipmentPositionHistoryR equipmentPositionHistoryR;
        public EquipmentPositionHistoryS(IEquipmentPositionHistoryR _equipmentPositionHistoryR)
        {
            equipmentPositionHistoryR = _equipmentPositionHistoryR;
        }
        public async Task<EquipmentPositionHistory> CreateAsync(EquipmentPositionHistory entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));

                return await equipmentPositionHistoryR.CreateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentPositionHistoryS/CreateAsync", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.");

                await equipmentPositionHistoryR.DeleteAsync(id);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentPositionHistoryS/DeleteAsync", ex);
            }

        }

        public async Task<IEnumerable<EquipmentPositionHistory>> FindAllAsync()
        {
            try
            {
                return await equipmentPositionHistoryR.FindAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentPositionHistoryS/FindAllAsync", ex);
            }
        }

        public async Task<EquipmentPositionHistory> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.");

                return await equipmentPositionHistoryR.GetByIdAsync(id);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentPositionHistoryS/FindByIdAsync", ex);
            }
        }

        public async Task UpdateAsync(EquipmentPositionHistory entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));

                await equipmentPositionHistoryR.UpdateAsync(entity);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentPositionHistoryS/UpdateAsync", ex);
            }
        }
    }
}
