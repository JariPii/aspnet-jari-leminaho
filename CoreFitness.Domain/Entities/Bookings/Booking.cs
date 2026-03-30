using CoreFitness.Domain.Entities.Bookings.ValueObjects;
using CoreFitness.Domain.Entities.Common;
using CoreFitness.Domain.Entities.TrainingSessions;
using CoreFitness.Domain.Entities.TrainingSessions.ValueObjects;
using CoreFitness.Domain.Entities.Users.ValueObjects;

namespace CoreFitness.Domain.Entities.Bookings
{
    public class Booking : BaseEntity<BookingId>
    {
        public UserId UserId { get; private set; }
        public TrainingSessionId TrainingSessionId { get; private set; }
        public TrainingSession TrainingSession { get; private set; } = null!;
        private Booking() { }

        public Booking(BookingId id, UserId userId, TrainingSessionId trainingSessionId)
        {
            Id = id;
            UserId = userId;
            TrainingSessionId = trainingSessionId;
        }
    }
}
