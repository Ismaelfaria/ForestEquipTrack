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
    public class EquipmentModelS : IEquipmentModelS
    {
        private readonly IEquipmentModelR equipmentModelR;
        public EquipmentModelS(IEquipmentModelR _equipmentModelR)
        {
            equipmentModelR = _equipmentModelR;
        }
        public async Task<EquipmentModel> CreateAsync(EquipmentModel entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));

                return await equipmentModelR.CreateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentModelS/CreateAsync", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.", nameof(id));

                await equipmentModelR.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentModelS/DeleteAsync", ex);
            }

        }

        public async Task<IEnumerable<EquipmentModel>> FindAllAsync()
        {
            try
            {
                return await equipmentModelR.FindAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentModelS/FindAllAsync", ex);
            }
        }

        public async Task<EquipmentModel> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.", nameof(id));

                return await equipmentModelR.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentModelS/FindByIdAsync", ex);
            }
        }

        public async Task UpdateAsync(EquipmentModel entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));

                await equipmentModelR.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentModelS/UpdateAsync", ex);
            }
        }
    }
}
