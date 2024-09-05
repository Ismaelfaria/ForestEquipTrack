using AutoMapper;
using BusOnTime.Application.Interfaces;
using BusOnTime.Application.Mapping.DTOs.InputModel;
using BusOnTime.Application.Mapping.DTOs.ViewModel;
using BusOnTime.Domain.Entities;
using BusOnTime.Infrastructure.Interfaces.Interface;
using FluentValidation;

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
                if (entity == null) throw new ArgumentNullException("Entity Invalid.");

                var validResult = validator.Validate(entity);

                if (!validResult.IsValid)
                {
                    var errorMessage = string.Join(", ", validResult.Errors.Select(e => e.ErrorMessage));
                    throw new ValidationException($"Validation failed, {errorMessage}");
                }

                var createMapObject = mapper.Map<EquipmentStateHistory>(entity);

                var view = await equipmentStateHistoryR.CreateAsync(createMapObject);

                var viewModel = mapper.Map<EquipmentStateHistoryVM>(view);

                return viewModel;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (ValidationException)
            {
                throw;
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

        public async Task UpdateAsync(Guid? id, EquipmentStateHistoryIM entity)
        {
            try
            {
                if (id == null) throw new ArgumentNullException(nameof(id));
                if (entity == null) throw new ArgumentNullException(nameof(entity));

                var validResult = validator.Validate(entity);

                if (!validResult.IsValid)
                {
                    var errorMessage = string.Join(", ", validResult.Errors.Select(e => e.ErrorMessage));
                    throw new ValidationException($"Validation failed, {errorMessage}");
                }

                var createMapObject = mapper.Map<EquipmentStateHistory>(entity);
                createMapObject.EquipmentStateId = id.Value;

                await equipmentStateHistoryR.UpdateAsync(createMapObject);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (ValidationException)
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
