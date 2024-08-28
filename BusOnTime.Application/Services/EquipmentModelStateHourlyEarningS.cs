using AutoMapper;
using BusOnTime.Application.Interfaces;
using BusOnTime.Application.Mapping.DTOs.InputModel;
using BusOnTime.Application.Mapping.DTOs.ViewModel;
using BusOnTime.Data.Entities;
using BusOnTime.Data.Interfaces.Interface;
using BusOnTime.Data.Repositories.Concrete;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        public async Task<EquipmentModelStateHourlyEarningsVM> CreateAsync(EquipmentModelStateHourlyEarningsIM entity)
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

                var view = await equipmentModelStateHourlyEarningsR.CreateAsync(createMapObject);

                var viewModel = mapper.Map<EquipmentModelStateHourlyEarningsVM>(view);

                return viewModel;
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

        public async Task<IEnumerable<EquipmentModelStateHourlyEarningsVM>> FindAllAsync()
        {
            try
            {
                var view = await equipmentModelStateHourlyEarningsR.FindAllAsync();

                var viewModel = mapper.Map<IEnumerable<EquipmentModelStateHourlyEarningsVM>>(view);

                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentModelStateHourlyEarningsS/FindAllAsync", ex);
            }
        }

        public async Task<EquipmentModelStateHourlyEarningsVM> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.");

                var view = await equipmentModelStateHourlyEarningsR.GetByIdAsync(id);

                var viewModel = mapper.Map<EquipmentModelStateHourlyEarningsVM>(view);

                return viewModel;
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

        public async Task UpdateAsync(Guid id, EquipmentModelStateHourlyEarningsIM entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));

                var validResult = validator.Validate(entity);

                if (!validResult.IsValid) throw new ValidationException("Erro na validação ao criar 'EquipmentModelStateHourlyEarnings'");

                var createMapObject = mapper.Map<EquipmentModelStateHourlyEarnings>(entity);

                createMapObject.EquipmentModelStateHourlyEarningsId = id;

                await equipmentModelStateHourlyEarningsR.UpdateAsync(createMapObject);
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
