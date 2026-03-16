using CoreFitness.Domain.Entities.Bookings;
using CoreFitness.Domain.Entities.Common;
using CoreFitness.Domain.Entities.TrainingSessions.ValueObjects;
using CoreFitness.Domain.Interfaces;

namespace CoreFitness.Domain.Entities.TrainingSessions
{
    public class TrainingSession : BaseEntity<TrainingSessionId>, IAggregateRoot
    {
        private readonly List<Booking> _bookings = new();
        public virtual IReadOnlyCollection<Booking> Bookings => _bookings.AsReadOnly();
        public TrainingSessionId TrainingSessionId { get; private set; }
        public string TrainingSessionName { get; set; } = string.Empty;
        public string TrainingSessionDescription { get; set; } = string.Empty;
        public int Capacity { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public TrainingSession() { }
    }
}
