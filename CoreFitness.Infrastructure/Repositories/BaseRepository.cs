using Microsoft.EntityFrameworkCore;
using CoreFitness.Domain.Entities.Common;
using CoreFitness.Domain.Interfaces;
using CoreFitness.Domain.Exceptions;

namespace CoreFitness.Infrastructure.Repositories
{
    public abstract class BaseRepository<T, TId>(CoreFitnessDbContext context)
        where T : BaseEntity<TId>, IAggregateRoot
    {
        protected readonly CoreFitnessDbContext _context = context;

        public virtual async Task AddAsync(T entity, CancellationToken ct = default) =>
            await _context.Set<T>().AddAsync(entity, ct);

        public virtual Task UpdateAsync(T entity, byte[]? rowVersion, CancellationToken ct = default)
        {
            if (rowVersion is null || rowVersion.Length == 0)
                throw new MissingRowVersionException();

            var entry = _context.Entry(entity);

            if (entry.State == EntityState.Detached)
                _context.Attach(entity);

            var rowVersionProperty = entry.Property("RowVersion");

            rowVersionProperty.OriginalValue = rowVersion;
            rowVersionProperty.IsModified = false;

            return Task.CompletedTask;
        }

        public virtual async Task<bool> DeleteAsync(TId id, CancellationToken ct = default)
        {
            var entity = await _context.Set<T>().FindAsync([id], ct);

            if (entity is null) return false;

            _context.Set<T>().Remove(entity);
            return true;
        }

        public virtual async Task<T?> GetByIdAsync(TId id, CancellationToken ct = default) =>
            await _context.Set<T>().FindAsync([id], ct);

        public virtual async Task<bool> ExistsByIdAsync(TId id, CancellationToken ct = default) =>
            await _context.Set<T>().AnyAsync(e => EqualityComparer<TId>.Default.Equals(e.Id, id), ct);

        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync(ct);
        }
    }
}
