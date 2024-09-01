using AutoMapper;
using BusOnTime.Application.Interfaces;
using BusOnTime.Application.Mapping.DTOs.InputModel;
using BusOnTime.Application.Mapping.DTOs.ViewModel;
using BusOnTime.Data.Entities;
using BusOnTime.Data.Interfaces.Interface;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusOnTime.Application.Services
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
                    var errorMessage = string.Join(", ", validResult.Errors.Select(e => e.ErrorMessage));
                    throw new ValidationException($"Validation failed, {errorMessage}");
                }

                var createMapObject = mapper.Map<Equipment>(entity);

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

        public async Task UpdateAsync(Guid id, EquipmentIM entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));

                var validResult = validator.Validate(entity);

                if (!validResult.IsValid)
                {
                    var errorMessage = string.Join(", ", validResult.Errors.Select(e => e.ErrorMessage));
                    throw new ValidationException($"Validation failed, {errorMessage}");
                }

                var createMapObject = mapper.Map<Equipment>(entity);

                createMapObject.EquipmentId = id;

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
