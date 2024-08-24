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
    public class EquipmentModelStateHourlyEarningS : IEquipmentModelStateHourlyEarningS
    {
        private readonly IEquipmentModelStateHourlyEarningsR equipmentModelStateHourlyEarningsR;
        private readonly IMapper mapper;
        private readonly IValidator<EquipmentModelStateHourlyEarningsIM> validator;
        public EquipmentModelStateHourlyEarningS(
            IEquipmentModelStateHourlyEarningsR _equipmentModelStateHourlyEarningsR,
            IMapper _mapper,
            IValidator<EquipmentModelStateHourlyEarningsIM> _validator)
        {
            equipmentModelStateHourlyEarningsR = _equipmentModelStateHourlyEarningsR;
            mapper = _mapper;
            validator = _validator;
        }
        public async Task<EquipmentModelStateHourlyEarnings> CreateAsync(EquipmentModelStateHourlyEarningsIM entity)
        {
            try
            {
                var validResult = validator.Validate(entity);

                if (!validResult.IsValid)
                {
                    throw new ValidationException("Erro na validação ao criar o 'EquipmentModelStateHourlyEarnings'");
                }

                var createMapObject = mapper.Map<EquipmentModelStateHourlyEarnings>(entity);

                if (createMapObject == null) throw new ArgumentNullException(nameof(createMapObject));

                return await equipmentModelStateHourlyEarningsR.CreateAsync(createMapObject);
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
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.");

                await equipmentModelStateHourlyEarningsR.DeleteAsync(id);
            }
            catch (ArgumentException)
            {
                throw;
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
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.");

                return await equipmentModelStateHourlyEarningsR.GetByIdAsync(id);
            }
            catch (ArgumentException)
            {
                throw;
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
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentModelStateHourlyEarningsS/UpdateAsync", ex);
            }
        }
    }
}
