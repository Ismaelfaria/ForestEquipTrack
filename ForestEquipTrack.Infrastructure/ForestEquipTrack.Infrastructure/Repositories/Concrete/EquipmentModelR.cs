using ForestEquipTrack.Infrastructure.DataContext;
using ForestEquipTrack.Domain.Entities;
using ForestEquipTrack.Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using ForestEquipTrack.Infrastructure.Interfaces.Interface;

namespace ForestEquipTrack.Infrastructure.Repositories.Concrete
{
    public class EquipmentModelR : RepositoryBase<EquipmentModel>, IEquipmentModelR
    {
        public EquipmentModelR(Context context) : base(context)
        {}

        public override async Task<EquipmentModel> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentException("O ID não pode ser vazio.", nameof(id));
                }

                var entity = await _context.EquipmentModel.SingleOrDefaultAsync(a => a.EquipmentModelId == id);

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


