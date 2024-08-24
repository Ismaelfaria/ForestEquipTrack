using AutoMapper;
using BusOnTime.Application.Interfaces;
using BusOnTime.Application.Mapping.DTOs.InputModel;
using BusOnTime.Data.Entities;
using BusOnTime.Data.Interfaces.Interface;
using FluentValidation;
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
        public async Task<Equipment> CreateAsync(EquipmentIM entity)
        {
            try
            {
                var validResult = validator.Validate(entity);

                if (!validResult.IsValid)
                {
                    throw new ValidationException("Erro na validação ao criar 'Equipment'");
                }

                var createMapObject = mapper.Map<Equipment>(entity);

                if (createMapObject == null) throw new ArgumentNullException(nameof(createMapObject));

                return await equipmentR.CreateAsync(createMapObject);
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

        public async Task<IEnumerable<Equipment>> FindAllAsync()
        {
            try
            {
                return await equipmentR.FindAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentS/FindAllAsync", ex);
            }
        }

        public async Task<Equipment> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.");

                return await equipmentR.GetByIdAsync(id);
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
                var validResult = validator.Validate(entity);

                if (!validResult.IsValid)
                {
                    throw new ValidationException("Erro na validação ao criar 'Equipment'");
                }

                var createMapObject = mapper.Map<Equipment>(entity);

                createMapObject.EquipmentId = id;

                if (entity == null) throw new ArgumentNullException(nameof(createMapObject));

                await equipmentR.UpdateAsync(createMapObject);
            }
            catch (ArgumentNullException)
            {
                throw;
            }catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentS/UpdateAsync", ex);
            }
        }
    }
}
