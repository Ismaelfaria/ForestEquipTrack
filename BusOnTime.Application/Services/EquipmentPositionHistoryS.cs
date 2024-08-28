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
        public async Task<EquipmentPositionHistoryVM> CreateAsync(EquipmentPositionHistoryIM entity)
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

                var view = await equipmentPositionHistoryR.CreateAsync(createMapObject);

                var viewModel = mapper.Map<EquipmentPositionHistoryVM>(view);

                return viewModel;
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

        public async Task<IEnumerable<EquipmentPositionHistoryVM>> FindAllAsync()
        {
            try
            {
                var view = await equipmentPositionHistoryR.FindAllAsync();

                var viewModel = mapper.Map<IEnumerable<EquipmentPositionHistoryVM>>(view);

                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentPositionHistoryS/FindAllAsync", ex);
            }
        }

        public async Task<EquipmentPositionHistoryVM> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.");

                var view = await equipmentPositionHistoryR.GetByIdAsync(id);

                var viewModel = mapper.Map<EquipmentPositionHistoryVM>(view);

                return viewModel;
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

        public async Task UpdateAsync(Guid id, EquipmentPositionHistoryIM entity)
        {
            try
            {

                if (entity == null) throw new ArgumentNullException(nameof(entity));

                var validResult = validator.Validate(entity);

                if (!validResult.IsValid)
                {
                    throw new ValidationException("Erro na validação ao criar 'EquipmentPositionHistory'");
                }

                var createMapObject = mapper.Map<EquipmentPositionHistory>(entity);

                createMapObject.EquipmentPositionId = id;

                await equipmentPositionHistoryR.UpdateAsync(createMapObject);
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
