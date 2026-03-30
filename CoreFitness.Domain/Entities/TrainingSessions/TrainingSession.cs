using CoreFitness.Domain.Entities.Bookings;
using CoreFitness.Domain.Entities.Common;
using CoreFitness.Domain.Entities.TrainingSessions.ValueObjects;
using CoreFitness.Domain.Exceptions;
using CoreFitness.Domain.Interfaces;

namespace CoreFitness.Domain.Entities.TrainingSessions
{
    public class TrainingSession : BaseEntity<TrainingSessionId>, IAggregateRoot
    {
        private readonly List<Booking> _bookings = new();
        public virtual IReadOnlyCollection<Booking> Bookings => _bookings.AsReadOnly();
        public TrainingSessionName TrainingSessionName { get; set; }
        public TrainingSessionDescription TrainingSessionDescription { get; set; }
        public TrainingSessionCapacity Capacity { get; private set; } = null!;
        public DateTimeOffset StartDate { get; private set; }
        public TrainingSessionDuration Duration { get; private set; }
        public DateTimeOffset EndDate => StartDate.Add(Duration.Value);

        private TrainingSession() { }

        private TrainingSession(TrainingSessionId id, TrainingSessionName name, TrainingSessionDescription description, DateTimeOffset startDate, TrainingSessionCapacity capacity, TrainingSessionDuration duration)
        {
            Id = id;
            TrainingSessionName = name;
            TrainingSessionDescription = description;
            StartDate = startDate;
            Capacity = capacity;
            Duration = duration;
        }

        public static TrainingSession Create(TrainingSessionId id, TrainingSessionName name, TrainingSessionDescription description, DateTimeOffset startDate, TrainingSessionCapacity capacity, TrainingSessionDuration duration)
        {
            if (startDate <= DateTimeOffset.UtcNow) throw new StartDateException("StartDate cannot be earlier than todays date");

            return new TrainingSession(
                id,
                name,
                description,
                startDate,
                capacity,
                duration);
        }

        public void UpdateName(TrainingSessionName newTrainingSessionName)
        {
            if (TrainingSessionName == newTrainingSessionName) return;

            TrainingSessionName = newTrainingSessionName;
            UpdateTimeStamp();
        }

        public void UpdateDescription(TrainingSessionDescription newDesctription)
        {
            if (TrainingSessionDescription == newDesctription) return;

            TrainingSessionDescription = newDesctription;
            UpdateTimeStamp();
        }

        public void UpdateStartDate(DateTimeOffset startDate)
        {
            if (startDate <= DateTimeOffset.UtcNow)
                throw new StartDateException("StartDate cannot be earlier than todays date");

            StartDate = startDate;
            UpdateTimeStamp();
        }

        public void UpdateDuration(TrainingSessionDuration newDuration)
        {
            if (Duration == newDuration) return;

            Duration = newDuration;
            UpdateTimeStamp();
        }

        public void UpdateCapacity(TrainingSessionCapacity newCapacity)
        {
            if (Capacity == newCapacity) return;

            Capacity = newCapacity;
            UpdateTimeStamp();
        }

        public void UpdateAll(TrainingSessionName name, TrainingSessionDescription description, DateTimeOffset start, TrainingSessionDuration duration, TrainingSessionCapacity capacity)
        {
            UpdateName(name);
            UpdateDescription(description);
            UpdateStartDate(start);
            UpdateDuration(duration);
            UpdateCapacity(capacity);
        }
    }
}
