using AutoMapper;
using ForestEquipTrack.Application.Interfaces;
using ForestEquipTrack.Application.Mapping.DTOs.InputModel;
using ForestEquipTrack.Application.Mapping.DTOs.ViewModel;
using ForestEquipTrack.Domain.Entities;
using ForestEquipTrack.Infrastructure.Interfaces.Interface;
using FluentValidation;

namespace ForestEquipTrack.Application.Services
{
    public class EquipmentModelS : IEquipmentModelS
    {
        private readonly IEquipmentModelR equipmentModelR;
        private readonly IMapper mapper;
        private readonly IValidator<EquipmentModelIM> validator;
        public EquipmentModelS(IEquipmentModelR _equipmentModelR, IMapper _mapper, IValidator<EquipmentModelIM> _validator)
        {
            equipmentModelR = _equipmentModelR;
            mapper = _mapper;
            validator = _validator;
        }
        public async Task<EquipmentModelVM> CreateAsync(EquipmentModelIM entity)
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

                var createMapObject = mapper.Map<EquipmentModel>(entity);

                var exists = await equipmentModelR.AnyAsync(e => e.Name == createMapObject.Name && !e.IsDeleted);
                
                if (exists)
                {
                    throw new InvalidOperationException("Um modelo com o mesmo nome já existe.");
                }

                var view = await equipmentModelR.CreateAsync(createMapObject);

                var viewModel = mapper.Map<EquipmentModelVM>(view);

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
                throw new Exception("BusOnTime/Application/Services/EquipmentModelS/CreateAsync", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.");

                await equipmentModelR.DeleteAsync(id);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentModelS/DeleteAsync", ex);
            }

        }

        public async Task<IEnumerable<EquipmentModelVM>> FindAllAsync()
        {
            try
            {
                var view = await equipmentModelR.FindAllAsync();

                var viewModel = mapper.Map <IEnumerable<EquipmentModelVM>>(view);

                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentModelS/FindAllAsync", ex);
            }
        }

        public async Task<EquipmentModelVM> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.");

                var view = await equipmentModelR.GetByIdAsync(id);

                var viewModel = mapper.Map<EquipmentModelVM>(view);

                return viewModel;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentModelS/FindByIdAsync", ex);
            }
        }

        public async Task UpdateAsync(Guid? id, EquipmentModelIM entity)
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

                var createMapObject = mapper.Map<EquipmentModel>(entity);
                createMapObject.EquipmentModelId = id.Value;

                await equipmentModelR.UpdateAsync(createMapObject);
            }
            catch (ArgumentNullException)
            {
                throw;
            }catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentModelS/UpdateAsync", ex);
            }
        }
    }
}
