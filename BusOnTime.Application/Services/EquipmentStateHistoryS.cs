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
        public async Task<EquipmentStateHistoryVM> CreateAsync(EquipmentStateHistoryIM entity)
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

                var view = await equipmentStateHistoryR.CreateAsync(createMapObject);

                var viewModel = mapper.Map<EquipmentStateHistoryVM>(view);

                return viewModel;
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

        public async Task<IEnumerable<EquipmentStateHistoryVM>> FindAllAsync()
        {
            try
            {
                var view = await equipmentStateHistoryR.FindAllAsync();

                var viewModel = mapper.Map<IEnumerable<EquipmentStateHistoryVM>>(view);

                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentStateHistoryS/FindAllAsync", ex);
            }
        }

        public async Task<EquipmentStateHistoryVM> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.");

                var view = await equipmentStateHistoryR.GetByIdAsync(id);

                var viewModel = mapper.Map<EquipmentStateHistoryVM>(view);

                return viewModel;
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

        public async Task UpdateAsync(Guid id, EquipmentStateHistoryIM entity)
        {
            try
            {
                var validResult = validator.Validate(entity);

                if (!validResult.IsValid)
                {
                    throw new ValidationException("Erro na validação ao criar 'EquipmentStateHistory'");
                }

                var createMapObject = mapper.Map<EquipmentStateHistory>(entity);

                createMapObject.EquipmentStateId = id;

                if (createMapObject == null) throw new ArgumentNullException(nameof(createMapObject));

                await equipmentStateHistoryR.UpdateAsync(createMapObject);
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
