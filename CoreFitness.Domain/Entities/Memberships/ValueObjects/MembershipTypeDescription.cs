using CoreFitness.Domain.Entities.Common;
using CoreFitness.Domain.Exceptions;

namespace CoreFitness.Domain.Entities.Memberships.ValueObjects
{
    public readonly record struct MembershipTypeDescription
    {
        public string Value { get; }
        public const int MaxLength = 1000;

        private MembershipTypeDescription(string value)
        {
            Value = value;
        }

        public static MembershipTypeDescription Create(string valaue)
        {
            var cleanDescription = valaue.NormalizeText();

            if (cleanDescription.Length > MaxLength)
                throw new InvalidMembershipTypeDescriptionException($"Description cannot excedd {MaxLength} characters");

            return new MembershipTypeDescription(cleanDescription);
        }

        public override string ToString() => Value;
    }
}
