using CoreFitness.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Domain.Entities.Memberships.ValueObjects
{
    public readonly record struct MembershipTypePrice
    {
        public decimal Value { get; }

        private MembershipTypePrice(decimal value)
        {
            Value = value;
        }

        public static MembershipTypePrice Create(decimal value)
        {
            if (value < 0)
                throw new InvalidPriceException("Price cannot be a negative value");

            return new MembershipTypePrice(value);
        }

        public static implicit operator decimal(MembershipTypePrice price) => price.Value;
    }
}
