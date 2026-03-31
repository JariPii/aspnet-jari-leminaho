using CoreFitness.Domain.Exceptions;

namespace CoreFitness.Domain.Entities.TrainingSessions.ValueObjects
{
    public readonly record struct TrainingSessionDuration
    {
        public TimeSpan Value { get; }

        private TrainingSessionDuration(TimeSpan value)
        {
            Value = value;
        }

        public static TrainingSessionDuration Create(TimeSpan value)
        {
            if (value <= TimeSpan.Zero)
                throw new InvalidDurationException("Duration must be set");

            return new TrainingSessionDuration(value);
        }
    }
}
