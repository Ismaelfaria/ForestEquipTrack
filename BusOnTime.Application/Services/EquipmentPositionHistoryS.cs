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
    public class EquipmentPositionHistoryS : IEquipmentPositionHistoryS
    {
        private readonly IEquipmentPositionHistoryR equipmentPositionHistoryR;
        private readonly IMapper mapper;
        private readonly IValidator<EquipmentPositionHistoryIM> validator;
        public EquipmentPositionHistoryS(
            IEquipmentPositionHistoryR _equipmentPositionHistoryR,
            IMapper _mapper,
            IValidator<EquipmentPositionHistoryIM> _validator
            )
        {
            equipmentPositionHistoryR = _equipmentPositionHistoryR;
            mapper = _mapper;
            validator = _validator;
        }
        public async Task<EquipmentPositionHistory> CreateAsync(EquipmentPositionHistoryIM entity)
        {
            try
            {
                var validResult = validator.Validate(entity);

                if (!validResult.IsValid)
                {
                    throw new ValidationException("Erro na validação ao criar 'EquipmentModel'");
                }

                var createMapObject = mapper.Map<EquipmentPositionHistory>(entity);

                if (createMapObject == null) throw new ArgumentNullException(nameof(createMapObject));

                return await equipmentPositionHistoryR.CreateAsync(createMapObject);
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
