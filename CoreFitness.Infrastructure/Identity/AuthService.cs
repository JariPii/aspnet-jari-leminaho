using CoreFitness.Application.Identity;
using CoreFitness.Domain.Common;
using CoreFitness.Domain.Entities.Users;
using CoreFitness.Domain.Entities.Users.ValueObjects;
using CoreFitness.Domain.Enums;
using CoreFitness.Domain.Interfaces.UnitOfWork;
using CoreFitness.Domain.Interfaces.Users;
using Microsoft.AspNetCore.Identity;

namespace CoreFitness.Infrastructure.Identity
{
    public class AuthService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork) : IAuthService
    {
        public async Task<Result> LoginAsync(string email, string password, bool rememberMe = false, CancellationToken ct = default)
        {
            var appUser = await userManager.FindByEmailAsync(email);
            if (appUser is null)
                return Result.Failure("Invalid email or password");

            var result = await signInManager.PasswordSignInAsync(appUser, password, rememberMe, lockoutOnFailure: false);
            if (!result.Succeeded)
                return Result.Failure("Invalid email or password");

            return Result.Success();
        }

        public async Task LogOutAsync() =>
            await signInManager.SignOutAsync();

        public async Task<Result> RegisterAsync(string email, string password, string firstName, string lastName, CancellationToken ct = default)
        {
            if (await userManager.FindByEmailAsync(email) is not null)
                return Result.Failure("Email already in use");

            var appUser = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            var result = await userManager.CreateAsync(appUser, password);
            if (!result.Succeeded)
                return Result.Failure(result.Errors.First().Description);

            await userManager.AddToRoleAsync(appUser, "Member");

            var domainUser = User.Create(
                new AuthenticationId(appUser.Id.ToString()),
                UserEmail.Create(email),
                UserName.Create(firstName, lastName),
                null,
                null,
                UserRole.Member);

            await userRepository.AddAsync(domainUser, ct);
            await unitOfWork.SaveChangesAsync(ct);

            return Result.Success();
        }
    }
}
