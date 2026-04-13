using CoreFitness.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreFitness.Infrastructure.Configurations
{
    public abstract class BaseEntityConfiguration<TEntity, TId> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity<TId>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.CreatedAt).HasColumnType("datetime2").IsRequired();
            builder.Property(e => e.UpdatedAt).HasColumnType("datetime2").IsRequired(false);

            builder.Property(e => e.RowVersion).IsRowVersion();
        }
    }
}
