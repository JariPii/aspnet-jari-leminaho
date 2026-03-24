using CoreFitness.Domain.Entities.Common;
using CoreFitness.Domain.Entities.Memberships.ValueObjects;
using CoreFitness.Domain.Entities.Users.ValueObjects;
using CoreFitness.Domain.Exceptions;
using CoreFitness.Domain.Interfaces;

namespace CoreFitness.Domain.Entities.Memberships
{
    public class Membership : BaseEntity<MembershipId>, IAggregateRoot
    {
        public UserId UserId { get; private set; }
        public DateOnly StartDate { get; private set; }
        public DateOnly EndDate { get; private set; }
        public MembershipTypeId TypeId { get; private set; }

        public Membership(
            MembershipId id,
            UserId userId,
            MembershipTypeId type,
            DateOnly startDate,
            DateOnly endDate)
        {
            Id = id;
            UserId = userId;
            TypeId = type;
            StartDate = startDate;
            EndDate = endDate;
        }
        private Membership() { }

        public bool IsActive => DateOnly.FromDateTime(DateTime.UtcNow) <= EndDate;

        public void Extend(DateOnly newEndDate)
        {
            if (newEndDate <= EndDate)
                throw new InvalidExtendMembershipException("New date has to later than end date");

            EndDate = newEndDate;
        }
    }
}
