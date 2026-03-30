using CoreFitness.Domain.Entities.Memberships;
using CoreFitness.Domain.Entities.Memberships.ValueObjects;

namespace CoreFitness.Domain.Interfaces.Memberships
{
    public interface IMembershipRepository
    {
        Task<Membership?> ExistsByIdAsync(MembershipId id, CancellationToken ct = default);
    }
}
