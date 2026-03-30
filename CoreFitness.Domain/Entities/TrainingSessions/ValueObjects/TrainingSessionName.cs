using CoreFitness.Domain.Entities.Common;
using CoreFitness.Domain.Exceptions;

namespace CoreFitness.Domain.Entities.TrainingSessions.ValueObjects
{
    public readonly partial record struct TrainingSessionName
    {
        public string Value { get; }

        private TrainingSessionName(string value)
        {
            Value = value;
        }

        public static TrainingSessionName Create(string value)
        {
            var cleanTrainingSessionName = value.NormalizeName();

            if (cleanTrainingSessionName.Length == 0)
                throw new InvalidNameException("TrainingSessionName is required");

            return new TrainingSessionName(cleanTrainingSessionName);
        }

        public override string ToString() => Value;
    }
}
