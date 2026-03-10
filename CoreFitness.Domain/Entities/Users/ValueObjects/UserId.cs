using CoreFitness.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Domain.Entities.Users.ValueObjects
{
    public readonly record struct UserId
    {
        public Guid Value { get; }

        public UserId(Guid value)
        {
            if (value == Guid.Empty)
                throw new IdIsRequiredException("UserId cannot be empty");

            Value = value;
        }

        public static UserId New() => new(Guid.NewGuid());
        public override string ToString() => Value.ToString();
    }
}
