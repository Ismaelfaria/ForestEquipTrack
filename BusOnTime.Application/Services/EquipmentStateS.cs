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
    public class EquipmentStateS : IEquipmentStateS
    {
        private readonly IEquipmentStateR equipmentStateR;
        private readonly IMapper mapper;
        private readonly IValidator<EquipmentStateIM> validator;
        public EquipmentStateS(
            IEquipmentStateR _equipmentStateR,
            IMapper _mapper,
            IValidator<EquipmentStateIM> _validator)
        {
            equipmentStateR = _equipmentStateR;
            mapper = _mapper;
            validator = _validator;
        }
        public async Task<EquipmentState> CreateAsync(EquipmentStateIM entity)
        {
            try
            {
                var validResult = validator.Validate(entity);

                if (!validResult.IsValid)
                {
                    throw new ValidationException("Erro na validação ao criar 'EquipmentModel'");
                }

                var createMapObject = mapper.Map<EquipmentState>(entity);

                if (createMapObject == null) throw new ArgumentNullException(nameof(createMapObject));

                return await equipmentStateR.CreateAsync(createMapObject);
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
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.");

                await equipmentStateR.DeleteAsync(id);
            }
            catch (ArgumentException)
            {
                throw;
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
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.");

                return await equipmentStateR.GetByIdAsync(id);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentStateS/FindByIdAsync", ex);
            }
        }

        public async Task UpdateAsync(Guid id, EquipmentStateIM entity)
        {
            try
            {
                var validResult = validator.Validate(entity);

                if (!validResult.IsValid)
                {
                    throw new ValidationException("Erro na validação ao criar 'EquipmentState'");
                }

                var createMapObject = mapper.Map<EquipmentState>(entity);

                createMapObject.StateId = id;

                if (createMapObject == null) throw new ArgumentNullException(nameof(createMapObject));

                await equipmentStateR.UpdateAsync(createMapObject);
            }
            catch (ArgumentNullException)
            {
                throw;
            }catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentStateS/UpdateAsync", ex);
            }
        }
    }
}
