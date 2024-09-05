using BusOnTime.Infrastructure.DataContext;
using BusOnTime.Domain.Entities;
using BusOnTime.Infrastructure.Interfaces.Interface;
using BusOnTime.Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace BusOnTime.Infrastructure.Repositories.Concrete
{
    public class EquipmentStateHistoryR : RepositoryBase<EquipmentStateHistory>, IEquipmentStateHistoryR
    {
        public EquipmentStateHistoryR(Context context) : base(context)
        {}
        
        public override async Task<EquipmentStateHistory> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentException("O ID não pode ser vazio.", nameof(id));
                }

                var entity = await _context.EquipmentStateHistory.SingleOrDefaultAsync(a => a.EquipmentStateId == id);

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
