using CoreFitness.Application.DTOs.Membership;
using CoreFitness.Application.DTOs.User;
using CoreFitness.Application.Interfaces;
using CoreFitness.Domain.Common;
using CoreFitness.Domain.Interfaces.Memberships;
using CoreFitness.Domain.Interfaces.Users;

namespace CoreFitness.Application.Services;

public class AdminService(IUserRepository userRepository,
IMembershipRepository membershipRepository,
IMembershipTypeRepository membershipTypeRepository) : IAdminService
{
    public async Task<Result<IEnumerable<AdminUserDTO>>> GetAllUsersAsync(CancellationToken ct = default)
    {
        //var users = await userRepository.GetAllAsync(ct);

        //var
        throw new NotImplementedException();
    }

}
