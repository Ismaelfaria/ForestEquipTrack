using BusOnTime.Infrastructure.DataContext;
using BusOnTime.Domain.Entities.Generic;
using BusOnTime.Infrastructure.Interfaces.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BusOnTime.Infrastructure.Repositories.Generic
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        protected readonly Context _context;
        public RepositoryBase(Context context)
        {
            _context = context;
        }

        public async virtual Task<TEntity> CreateAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "A entidade não pode ser nula.");
                }

                await _context.Set<TEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar entidade: {ex.Message}");
                throw;
            }
        }

        public async virtual Task<IEnumerable<TEntity>> FindAllAsync()
        {
            try
            {
                return await _context.Set<TEntity>()
                 .Where(entity => !entity.IsDeleted)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar todas as entidades: {ex.Message}");
                throw;
            }
        }

        public async virtual Task UpdateAsync(TEntity entity)
        {

            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "A entidade não pode ser nula.");
                }

                _context.Set<TEntity>().Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar entidade: {ex.Message}");
                throw;
            }
        }

        public async virtual Task<TEntity> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentException("O ID não pode ser vazio.", nameof(id));
                }

                var entity = await _context.Set<TEntity>().FindAsync(id);

                if (entity == null)
                {
                    throw new KeyNotFoundException($"Entidade com ID {id} não encontrada.");
                }

                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar entidade por ID: {ex.Message}");
                throw;
            }
        }

        public async virtual Task DeleteAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentException("O ID não pode ser vazio.", nameof(id));
                }

                var entity = await GetByIdAsync(id);

                if (entity == null)
                {
                    throw new KeyNotFoundException("Entidade não encontrada.");
                }

                entity.IsDeleted = true;
                _context.Update(entity);
                await _context.SaveChangesAsync();
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

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().AnyAsync(predicate);
        }
    }
}
