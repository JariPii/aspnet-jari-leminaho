using CoreFitness.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Domain.Entities.Memberships.ValueObjects
{
    public readonly record struct MembershipTypeId
    {
        public Guid Value { get; }

        public MembershipTypeId(Guid value)
        {
            if (value == Guid.Empty)
                throw new IdIsRequiredException("MembershipId cannot be empty");

            Value = value;
        }

        public static MembershipTypeId New() => new(Guid.NewGuid());
        public override string ToString() => Value.ToString();
    }
}
