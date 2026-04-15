using CoreFitness.Domain.Entities.Memberships;
using CoreFitness.Domain.Entities.Memberships.ValueObjects;

namespace CoreFitness.Domain.Interfaces.Memberships
{
    public interface IMembershipTypeRepository : IBaseRepository<MembershipType, MembershipTypeId>
    {
    }
}
