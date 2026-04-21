using CoreFitness.Domain.Entities.Common;
using CoreFitness.Domain.Exceptions;

namespace CoreFitness.Domain.Entities.Memberships.ValueObjects
{
    public readonly record struct MembershipTypeBenefitDescription
    {
        public string Value { get; }
        public const int MaxLength = 500;

        private MembershipTypeBenefitDescription(string value)
        {
            Value = value;
        }

        public static MembershipTypeBenefitDescription Create(string value)
        {
            var cleanDescription = value.NormalizeText();

            if (cleanDescription.Length > MaxLength)
                throw new InvalidBenefitDescriptionException($"Description cannot exceed {MaxLength} characters");

            return new MembershipTypeBenefitDescription(cleanDescription);
        }

        public override string ToString() => Value;
    }
}
