using CoreFitness.Domain.Entities.Common;
using CoreFitness.Domain.Exceptions;

namespace CoreFitness.Domain.Entities.Memberships.ValueObjects
{
    public readonly record struct MembershipTypeName
    {
        public string Value { get; }
        public const int MaxLength = 50;
        private MembershipTypeName(string value)
        {
            Value = value;
        }

        public static MembershipTypeName Create(string value)
        {
            var cleanMTName = value.NormalizeName();

            if (cleanMTName.Length == 0)
                throw new InvalidMembershipTypeNameException(cleanMTName);

            if (cleanMTName.Length > MaxLength)
                throw new InvalidMembershipTypeNameException($"Name cannot excedd {MaxLength} characters");

            return new MembershipTypeName(cleanMTName);
        }

        public override string ToString() => Value;
    }
}
