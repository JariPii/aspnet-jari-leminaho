using CoreFitness.Domain.Entities.Common;
using CoreFitness.Domain.Exceptions;

namespace CoreFitness.Domain.Entities.TrainingSessions.ValueObjects
{
    public readonly record struct TrainingSessionDescription
    {
        public const int MaxLength = 1000;
        public string Value { get; }

        private TrainingSessionDescription(string value)
        {
            Value = value;
        }

        public static TrainingSessionDescription Create(string description)
        {
            var cleanDescription = description.NormalizeText();

            if (cleanDescription.Length > MaxLength)
                throw new InvalidDescriptionLengthException($"Description cannot be more than {MaxLength} characcters");

            return new TrainingSessionDescription(cleanDescription);
        }

        public override string ToString() => Value;
    }
}
