﻿using ForestEquipTrack.Infrastructure.DataContext;
using ForestEquipTrack.Domain.Entities;
using ForestEquipTrack.Infrastructure.Interfaces.Interface;
using ForestEquipTrack.Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace ForestEquipTrack.Infrastructure.Repositories.Concrete
{
    public class EquipmentModelStateHourlyEarningsR : RepositoryBase<EquipmentModelStateHourlyEarnings>, IEquipmentModelStateHourlyEarningsR
    {
        public EquipmentModelStateHourlyEarningsR(Context context) : base(context)
        {}
        public override async Task<EquipmentModelStateHourlyEarnings> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentException("O ID não pode ser vazio.", nameof(id));
                }

                var entity = await _context.EquipmentModelStateHourlyEarnings.SingleOrDefaultAsync(a => a.EquipmentModelStateHourlyEarningsId == id);

                if (entity == null)
                {
                    throw new KeyNotFoundException("Entidade não encontrada.");
                }

                return entity;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro de argumento: {ex.Message}");
                throw;
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Entidade não encontrada: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                throw;
            }
        }
    }
}
