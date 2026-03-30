using CoreFitness.Domain.Entities.Common;
using CoreFitness.Domain.Entities.Memberships.ValueObjects;

namespace CoreFitness.Domain.Entities.Memberships
{
    public class MembershipType : BaseEntity<MembershipTypeId>
    {
        public MembershipTypeName Name { get; private set; }
        public MembershipTypeDescription Description { get; private set; }
        public MembershipTypePrice Price { get; private set; }
        public MembershipTypeDuration Duration { get; private set; }
        public bool IsActive { get; private set; }

        public static MembershipType Create(MembershipTypeName name, MembershipTypeDescription description, MembershipTypePrice price, MembershipTypeDuration duration)
        {
            return new(MembershipTypeId.New(), name, description, price, duration);
        }

        private MembershipType(MembershipTypeId id, MembershipTypeName name, MembershipTypeDescription description, MembershipTypePrice price, MembershipTypeDuration duration)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Duration = duration;
            IsActive = true;
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

        public void DeactivateMembership() => IsActive = false;
        public void ActivateMembership() => IsActive = true;

    }
}
