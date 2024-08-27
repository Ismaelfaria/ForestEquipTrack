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
        public async Task<EquipmentModel> CreateAsync(EquipmentModelIM entity)
        {
            try
            {
                var validResult = validator.Validate(entity);

                if (!validResult.IsValid)
                {
                    throw new ValidationException("Erro na validação ao criar 'EquipmentModel'");
                }

                var createMapObject = mapper.Map<EquipmentModel>(entity);

                if (createMapObject == null) throw new ArgumentNullException(nameof(createMapObject));

                return await equipmentModelR.CreateAsync(createMapObject);
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

        public async Task<IEnumerable<EquipmentModel>> FindAllAsync()
        {
            try
            {
                return await equipmentModelR.FindAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentModelS/FindAllAsync", ex);
            }
        }

        public async Task<EquipmentModel> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty) throw new ArgumentException("Invalid ID.");

                return await equipmentModelR.GetByIdAsync(id);
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

        public async Task UpdateAsync(Guid id, EquipmentModelIM entity)
        {
            try
            {
                var validResult = validator.Validate(entity);

                if (!validResult.IsValid)
                {
                    throw new ValidationException("Erro na validação ao criar 'EquipmentModel'");
                }

                var createMapObject = mapper.Map<EquipmentModel>(entity);

                createMapObject.EquipmentId = id;

                if (createMapObject == null) throw new ArgumentNullException(nameof(createMapObject));

                await equipmentModelR.UpdateAsync(createMapObject);
            }
            catch (ArgumentNullException)
            {
                throw;
            }catch (Exception ex)
            {
                throw new Exception("BusOnTime/Application/Services/EquipmentModelS/UpdateAsync", ex);
            }
        }
    }
}
