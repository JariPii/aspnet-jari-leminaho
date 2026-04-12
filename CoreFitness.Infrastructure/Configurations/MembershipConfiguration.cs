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

            builder.Property(m => m.CurrentWeight)
                .HasColumnType("decimal(5,2)");

            builder.Property(m => m.TargetWeight)
                .HasColumnType("decimal(5,2)");

            builder.Property(m => m.Height)
                .HasColumnType("decimal(5,2)");

            builder.Ignore(m => m.BMI);
            builder.Ignore(m => m.IsActive);
        }
    }
}
