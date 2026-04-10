using CoreFitness.Domain.Entities.Memberships;
using CoreFitness.Domain.Entities.Memberships.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreFitness.Infrastructure.Configurations
{
    public class MembershipConfiguration : BaseEntityConfiguration<Membership, MembershipId>
    {
        public override void Configure(EntityTypeBuilder<Membership> builder)
        {
            base.Configure(builder);

            builder.Property(m => m.StartDate)
                .HasConversion(d => d.ToDateTime(TimeOnly.MinValue),
                d => DateOnly.FromDateTime(d))
                .HasColumnType("date")
                .IsRequired();

            builder.Property(m => m.EndDate)
                .HasConversion(d => d.ToDateTime(TimeOnly.MinValue),
                d => DateOnly.FromDateTime(d))
                .HasColumnType("date")
                .IsRequired();

            builder.Ignore(m => m.IsActive);
        }
    }
}
