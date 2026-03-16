using CoreFitness.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Domain.Entities.Bookings.ValueObjects
{
    public readonly record struct BookingId
    {
        public Guid Value { get; }

        public BookingId(Guid value)
        {
            if (value == Guid.Empty)
                throw new IdIsRequiredException("UserId cannot be empty");

            Value = value;
        }

        public static BookingId New() => new(Guid.NewGuid());
        public override string ToString() => Value.ToString();
    }
}
