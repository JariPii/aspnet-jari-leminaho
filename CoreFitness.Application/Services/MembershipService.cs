using CoreFitness.Application.DTOs.Membership;
using CoreFitness.Application.Interfaces;
using CoreFitness.Domain.Common;
using CoreFitness.Domain.Entities.Users.ValueObjects;
using CoreFitness.Domain.Exceptions;
using CoreFitness.Domain.Interfaces.Memberships;
using CoreFitness.Domain.Interfaces.UnitOfWork;

namespace CoreFitness.Application.Services
{
    public class MembershipService(IMembershipRepository repository, IUnitOfWork unitOfWork) : IMembershipService
    {
        public Task<Result> ActivateAsync(Guid userId, CancellationToken ct = default) =>
            Result.TryAsync(async () =>
            {
                var id = new UserId(userId);

                var membership = await repository.GetByUserIdAsync(id, ct) ??
                    throw new MembershipNotFoundException($"No membership found for user {userId}");

                membership.ActivateMembership();

                await unitOfWork.SaveChangesAsync(ct);
            });

        public Task<Result> CreateAsync(Guid userId, CreateMembershipDTO dto, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> DeactivateAsync(Guid userId, CancellationToken ct = default)
        {
            return await Result.TryAsync(async () =>
            {
                var id = new UserId(userId);

                var membership = await repository.GetByUserIdAsync(id, ct) ??
                    throw new MembershipNotFoundException($"No membership was found for user {userId}");

                membership.DeactivateMembership();

                await unitOfWork.SaveChangesAsync(ct);
            });
        }

        public Task<Result<MembershipDTO>> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<MembershipTypeDTO>>> GetMembershipTypesAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
