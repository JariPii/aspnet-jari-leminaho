using CoreFitness.Domain.Entities.TrainingSessions;
using CoreFitness.Domain.Entities.TrainingSessions.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreFitness.Infrastructure.Configurations
{
    public class TrainingsessionConfiguration : BaseEntityConfiguration<TrainingSession, TrainingSessionId>
    {
        public override void Configure(EntityTypeBuilder<TrainingSession> builder)
        {
            base.Configure(builder);

            builder.HasMany(b => b.Bookings)
                .WithOne(t => t.TrainingSession)
                .HasForeignKey(t => t.TrainingSessionId);
        }
    }
}
