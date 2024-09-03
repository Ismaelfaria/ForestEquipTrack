using AutoMapper;
using BusOnTime.Application.Interfaces;
using BusOnTime.Application.Mapping.DTOs.InputModel;
using BusOnTime.Application.Mapping.DTOs.ViewModel;
using BusOnTime.Data.Entities;
using BusOnTime.Data.Interfaces.Interface;
using FluentValidation;

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
                if (entity == null) throw new ArgumentNullException("Entity Invalid.");

                var validResult = validator.Validate(entity);

                if (!validResult.IsValid)
                {
                    var errorMessage = string.Join(", ", validResult.Errors.Select(e => e.ErrorMessage));
                    throw new ValidationException($"Validation failed, {errorMessage}");
                }

                var createMapObject = mapper.Map<EquipmentModelStateHourlyEarnings>(entity);

                var view = await equipmentModelStateHourlyEarningsR.CreateAsync(createMapObject);

                var viewModel = mapper.Map<EquipmentModelStateHourlyEarningsVM>(view);

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

        public async Task UpdateAsync(Guid? id, EquipmentModelStateHourlyEarningsIM entity)
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

                var createMapObject = mapper.Map<EquipmentModelStateHourlyEarnings>(entity);

                createMapObject.EquipmentModelStateHourlyEarningsId = id.Value;

                await equipmentModelStateHourlyEarningsR.UpdateAsync(createMapObject);
            }
            catch (ArgumentNullException)
            {
                throw;
            }catch (ValidationException)
            {
                throw;
            }catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentModelStateHourlyEarningsS/UpdateAsync", ex);
            }
        }
    }
}
