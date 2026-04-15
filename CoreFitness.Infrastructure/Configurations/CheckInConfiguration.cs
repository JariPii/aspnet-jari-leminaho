using CoreFitness.Domain.Entities.Memberships;
using CoreFitness.Domain.Entities.Memberships.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreFitness.Infrastructure.Configurations
{
    public class CheckInConfiguration :BaseEntityConfiguration<CheckIn, CheckInId>
    {
        public override void Configure(EntityTypeBuilder<CheckIn> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.CheckedInAt).IsRequired();

            builder.HasOne<Membership>()
                .WithMany(m => m.CheckIns)
                .HasForeignKey(c => c.MembershipId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
