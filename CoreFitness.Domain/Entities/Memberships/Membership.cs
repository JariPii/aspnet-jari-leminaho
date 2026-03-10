using CoreFitness.Domain.Entities.Common;
using CoreFitness.Domain.Entities.Memberships.ValueObjects;
using CoreFitness.Domain.Entities.Users.ValueObjects;
using CoreFitness.Domain.Enums;
using CoreFitness.Domain.Exceptions;
using CoreFitness.Domain.Interfaces;

namespace CoreFitness.Domain.Entities.Memberships
{
    public class Membership : BaseEntity<MembershipId>, IAggregateRoot
    {
        public UserId UserId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public MembershipType Type { get; private set; }

        private Membership() { }

        public Membership(
            MembershipId id,
            UserId userId,
            MembershipType type,
            DateTime startDate,
            DateTime endDate)
        {
            Id = id;
            UserId = userId;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
        }

        public bool IsActive => DateTime.UtcNow <= EndDate;

        public void Extend(DateTime newEndDate)
        {
            if (newEndDate <= EndDate)
                throw new InvalidExtendMembershipException("New date has to later than end date");

            EndDate = newEndDate;
        }
    }
}
