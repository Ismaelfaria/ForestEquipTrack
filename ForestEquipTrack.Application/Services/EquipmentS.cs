﻿using AutoMapper;
using ForestEquipTrack.Application.Interfaces;
using ForestEquipTrack.Application.Mapping.DTOs.InputModel;
using ForestEquipTrack.Application.Mapping.DTOs.ViewModel;
using ForestEquipTrack.Domain.Entities;
using ForestEquipTrack.Infrastructure.Interfaces.Interface;
using FluentValidation;

namespace ForestEquipTrack.Application.Services
{
    public class EquipmentS : IEquipmentS
    {
        private readonly IEquipmentR equipmentR;
        private readonly IMapper mapper;
        private readonly IValidator<EquipmentIM> validator;
        public EquipmentS(
            IEquipmentR _equipmentR,
            IMapper _mapper,
            IValidator<EquipmentIM> _validator)
        {
            equipmentR = _equipmentR;
            mapper = _mapper;
            validator = _validator;
        }
        public async Task<EquipmentVM> CreateAsync(EquipmentIM entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException("Entity Invalid.");

                var validResult = validator.Validate(entity);

                if (!validResult.IsValid)
                {
                    throw new ValidationException(validResult.Errors);
                }

                var createMapObject = mapper.Map<Equipment>(entity);

                var exists = await equipmentR.AnyAsync(e => e.Name == createMapObject.Name && !e.IsDeleted);

                if (exists)
                {
                    throw new InvalidOperationException("Um equipamento com o mesmo nome já existe.");
                }

                var view = await equipmentR.CreateAsync(createMapObject);

                var viewModel = mapper.Map<EquipmentVM>(view);

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
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentS/CreateAsync", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.");

                await equipmentR.DeleteAsync(id);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentS/DeleteAsync", ex);
            }

        }

        public async Task<IEnumerable<EquipmentVM>> FindAllAsync()
        {
            try
            {
                var view = await equipmentR.FindAllAsync();

                var viewModel = mapper.Map<IEnumerable<EquipmentVM>>(view);

                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentS/FindAllAsync", ex);
            }
        }

        public async Task<EquipmentVM> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.");

                var view = await equipmentR.GetByIdAsync(id);

                var viewModel = mapper.Map<EquipmentVM>(view);

                return viewModel;
            }
            catch (ArgumentException)
            {
                throw;
            }catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentS/FindByIdAsync", ex);
            }
        }

        public async Task UpdateAsync(Guid? id, EquipmentIM entity)
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

                var createMapObject = mapper.Map<Equipment>(entity);

                createMapObject.EquipmentId = id.Value;

                await equipmentR.UpdateAsync(createMapObject);
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
                throw new Exception("BusOnTime/Application/Services/EquipmentS/UpdateAsync", ex);
            }
        }
    }
}
