using CoreFitness.Domain.Entities.Common;
using CoreFitness.Domain.Entities.Memberships.ValueObjects;
using CoreFitness.Domain.Enums;
using CoreFitness.Domain.Exceptions;
using CoreFitness.Domain.Interfaces;

namespace CoreFitness.Domain.Entities.Memberships
{
    public class MembershipType : BaseEntity<MembershipTypeId>, IAggregateRoot
    {
        public MembershipTypeName Name { get; private set; }
        public MembershipTypeDescription Description { get; private set; }
        public MembershipTypePrice Price { get; private set; }
        public MembershipTypeDuration Duration { get; private set; }
        public int SessionLimit { get; private set; }
        public MembershipTypeEnums Type { get; private set; }

        public static MembershipType Create(MembershipTypeName name, MembershipTypeDescription description, MembershipTypePrice price, MembershipTypeDuration duration, int sessionLimit, MembershipTypeEnums type)
        {
            return new(MembershipTypeId.New(), name, description, price, duration, sessionLimit, type);
        }

        private MembershipType(MembershipTypeId id, MembershipTypeName name, MembershipTypeDescription description, MembershipTypePrice price, MembershipTypeDuration duration, int sessionLimit, MembershipTypeEnums type)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Duration = duration;
            SessionLimit = sessionLimit;
            Type = type;
        }
        protected MembershipType() { }

        public void UpdatePrice(MembershipTypePrice newPrice)
        {
            Price = newPrice;
            UpdateTimeStamp();
        }

        public void UpdateDuration(MembershipTypeDuration days)
        {
            Duration = days;
            UpdateTimeStamp();
        }

        public void UpdateSessionLimit(int newLimit)
        {
            if (newLimit <= 0)
                throw new InvalidSessionLimitException("Session limit must be greater than 0");

            SessionLimit = newLimit;
            UpdateTimeStamp();
        }
    }
}
