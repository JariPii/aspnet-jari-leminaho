using CoreFitness.Application.DTOs.User;
using CoreFitness.Application.Interfaces;
using CoreFitness.Application.Mappings;
using CoreFitness.Domain.Common;
using CoreFitness.Domain.Entities.Users.ValueObjects;
using CoreFitness.Domain.Exceptions;
using CoreFitness.Domain.Interfaces.UnitOfWork;
using CoreFitness.Domain.Interfaces.Users;

namespace CoreFitness.Application.Services
{
    public class UserService(IUserRepository repository, IUnitOfWork unitOfWork) : IUserService
    {
        public async Task<Result> DeleteAsync(Guid userId, CancellationToken ct = default)
        {
            return await Result.TryAsync(async () =>
            {
                var id = new UserId(userId);

                var deleted = await repository.DeleteAsync(id, ct);

                if (!deleted)
                    throw new UserNotFoundException("User not found");

                await unitOfWork.SaveChangesAsync(ct);
            });
        }

        public async Task<Result<UserDTO>> GetByIdAsync(Guid userId, CancellationToken ct = default)
        {
            return await Result<UserDTO>.TryAsync(async () =>
            {
                var id = new UserId(userId);

                var user = await repository.GetByIdAsync(id, ct) ??
                    throw new UserNotFoundException("User not found");

                return user.ToDTO();
            });
        }

        public async Task<Result> UpdateAsync(UpdateUserDTO dto, CancellationToken ct = default)
        {
            return await Result.TryAsync(async () =>
            {
                var id = new UserId(dto.Id);

                var user = await repository.GetByIdAsync(id, ct) ??
                    throw new UserNotFoundException("User not found");

                if (dto.Email is not null && dto.Email != user.Email.Value)
                {
                    var newEmail = UserEmail.Create(dto.Email);

                    if (await repository.ExistsByEmailAsync(newEmail, ct))
                        throw new EmailAlreadyExistsException(newEmail);

                    user.UpdateEmail(newEmail);
                }

                user.UpdateFirstName(dto.FirstName ?? user.UserName.FirstName);
                user.UpdateLastName(dto.LastName ?? user.UserName.LastName);

                if (dto.PhoneNumber is not null && dto.PhoneNumber != user.UserPhoneNumber?.Value)
                {
                    user.UpdatePhoneNumber(UserPhoneNumber.Create(dto.PhoneNumber));
                }

                await repository.UpdateAsync(user, dto.RowVersion, ct);
                await unitOfWork.SaveChangesAsync(ct);
            });
        }
    }
}
