using CoreFitness.Domain.Entities.Memberships;
using CoreFitness.Domain.Entities.Memberships.ValueObjects;
using CoreFitness.Domain.Entities.Users.ValueObjects;
using CoreFitness.Domain.Interfaces.Memberships;

namespace CoreFitness.Infrastructure.Repositories
{
    public class MembershipRepository(CoreFitnessDbContext context) : BaseRepository<Membership, MembershipId>(context),
        IMembershipRepository
    {
        public Task<Membership?> GetByUserIdAsync(UserId userId, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
