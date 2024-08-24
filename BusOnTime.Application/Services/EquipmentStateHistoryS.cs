using AutoMapper;
using BusOnTime.Application.Interfaces;
using BusOnTime.Application.Mapping.DTOs.InputModel;
using BusOnTime.Data.Entities;
using BusOnTime.Data.Interfaces.Interface;
using BusOnTime.Data.Repositories.Concrete;
using FluentValidation;
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
        private readonly IMapper mapper;
        private readonly IValidator<EquipmentStateHistoryIM> validator;
        public EquipmentStateHistoryS(
            IEquipmentStateHistoryR _equipmentStateHistoryR,
            IMapper _mapper,
            IValidator<EquipmentStateHistoryIM> _validator
            )
        {
            equipmentStateHistoryR = _equipmentStateHistoryR;
            mapper = _mapper;
            validator = _validator;
        }
        public async Task<EquipmentStateHistory> CreateAsync(EquipmentStateHistoryIM entity)
        {
            try
            {
                var validResult = validator.Validate(entity);

                if (!validResult.IsValid)
                {
                    throw new ValidationException("Erro na validação ao criar 'EquipmentModel'");
                }

                var createMapObject = mapper.Map<EquipmentStateHistory>(entity);

                if (createMapObject == null) throw new ArgumentNullException(nameof(createMapObject));

                return await equipmentStateHistoryR.CreateAsync(createMapObject);
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
