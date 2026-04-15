using CoreFitness.Domain.Entities.Memberships;
using CoreFitness.Domain.Entities.Memberships.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreFitness.Infrastructure.Configurations
{
    public class MembershipTypeConfiguration : BaseEntityConfiguration<MembershipType, MembershipTypeId>
    {
        public override void Configure(EntityTypeBuilder<MembershipType> builder)
        {
            base.Configure(builder);

            builder.Property(t => t.Name)
                .HasConversion(v => v.Value, v => MembershipTypeName.Create(v));

            builder.Property(t => t.Description)
                .HasConversion(v => v.Value, v => MembershipTypeDescription.Create(v));

            builder.Property(t => t.Price)
                .HasConversion(v => v.Value, v => MembershipTypePrice.Create(v))
                .HasColumnType("decimal(10,2)");

            builder.Property(t => t.Duration)
                .HasConversion(v => v.Value, v => MembershipTypeDuration.Create(v));
        }
    }
}
