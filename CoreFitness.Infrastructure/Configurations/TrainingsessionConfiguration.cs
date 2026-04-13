using CoreFitness.Domain.Entities.TrainingSessions;
using CoreFitness.Domain.Entities.TrainingSessions.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreFitness.Infrastructure.Configurations
{
    public class TrainingsessionConfiguration : BaseEntityConfiguration<TrainingSession, TrainingSessionId>
    {
        public override void Configure(EntityTypeBuilder<TrainingSession> builder)
        {
            base.Configure(builder);

            builder.ComplexProperty(ts => ts.TrainingSessionName, name =>
            {
                name.Property(n => n.Value)
                .HasColumnName("Name")
                .HasMaxLength(TrainingSessionName.MaxLength)
                .IsRequired();
            });

            builder.ComplexProperty(ts => ts.TrainingSessionDescription, description =>
            {
                description.Property(d => d.Value)
                .HasColumnName("Description")
                .HasMaxLength(TrainingSessionDescription.MaxLength)
                .IsRequired();
            });

            builder.ComplexProperty(ts => ts.Capacity, capacity =>
            {
                capacity.Property(c => c.Value)
                .HasColumnName("Capacity")
                .IsRequired();
            });

            builder.ComplexProperty(ts => ts.Duration, duration =>
            {
                duration.Property(d => d.Value)
                .HasColumnName("Duration")
                .IsRequired();
            });

            builder.Property(ts => ts.StartDate)
                .IsRequired();

            builder.Ignore(ts => ts.EndDate);
            builder.Ignore(ts => ts.IsFull);

            builder.HasMany(b => b.Bookings)
                .WithOne(t => t.TrainingSession)
                .HasForeignKey(t => t.TrainingSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(ts => ts.Bookings)
                .HasField("_bookings");
        }
    }
}
