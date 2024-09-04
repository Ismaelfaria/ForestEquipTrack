using AutoMapper;
using BusOnTime.Application.Interfaces;
using BusOnTime.Application.Mapping.DTOs.InputModel;
using BusOnTime.Application.Mapping.DTOs.ViewModel;
using BusOnTime.Data.Entities;
using BusOnTime.Data.Interfaces.Interface;
using BusOnTime.Data.Repositories.Concrete;
using FluentValidation;

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
        public async Task<EquipmentStateVM> CreateAsync(EquipmentStateIM entity)
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

                var createMapObject = mapper.Map<EquipmentState>(entity);

                var exists = await equipmentStateR.AnyAsync(e => e.Name == createMapObject.Name && !e.IsDeleted);

                if (exists)
                {
                    throw new InvalidOperationException("Um estato com o mesmo nome já existe.");
                }

                var view = await equipmentStateR.CreateAsync(createMapObject);

                var viewModel = mapper.Map<EquipmentStateVM>(view);

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

        public async Task<IEnumerable<EquipmentStateVM>> FindAllAsync()
        {
            try
            {
                var view = await equipmentStateR.FindAllAsync();

                var viewModel = mapper.Map<IEnumerable<EquipmentStateVM>>(view);

                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentStateS/FindAllAsync", ex);
            }
        }

        public async Task<EquipmentStateVM> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.");

                var view = await equipmentStateR.GetByIdAsync(id);

                var viewModel = mapper.Map<EquipmentStateVM>(view);

                return viewModel;
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

        public async Task UpdateAsync(Guid? id, EquipmentStateIM entity)
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

                var createMapObject = mapper.Map<EquipmentState>(entity);
                createMapObject.StateId = id.Value;

                await equipmentStateR.UpdateAsync(createMapObject);
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
                throw new Exception("BusOnTime/Application/Services/EquipmentStateS/UpdateAsync", ex);
            }
        }
    }
}
