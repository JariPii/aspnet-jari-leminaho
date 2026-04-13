using CoreFitness.Domain.Entities.Bookings;
using CoreFitness.Domain.Entities.Bookings.ValueObjects;
using CoreFitness.Domain.Entities.Memberships;
using CoreFitness.Domain.Entities.Memberships.ValueObjects;
using CoreFitness.Domain.Entities.TrainingSessions;
using CoreFitness.Domain.Entities.TrainingSessions.ValueObjects;
using CoreFitness.Domain.Entities.Users;
using CoreFitness.Domain.Entities.Users.ValueObjects;
using CoreFitness.Infrastructure.Converters;
using CoreFitness.Infrastructure.Primitives;
using Microsoft.EntityFrameworkCore;

namespace CoreFitness.Infrastructure
{
    public class CoreFitnessDbContext(DbContextOptions<CoreFitnessDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<TrainingSession> TrainingSessions { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<UserId>().HaveConversion<UserIdConverter>();
            configurationBuilder.Properties<MembershipId>().HaveConversion<MembershipIdConverter>();
            configurationBuilder.Properties<MembershipTypeId>().HaveConversion<MembershipTypeId>();
            configurationBuilder.Properties<BookingId>().HaveConversion<BookingIdConverter>();
            configurationBuilder.Properties<TrainingSessionId>().HaveConversion<TrainingSessionIdConverter>();

            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IntResult>().HasNoKey();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoreFitnessDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
