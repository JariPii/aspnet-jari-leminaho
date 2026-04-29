using CoreFitness.Application.Authentication.Models;

namespace CoreFitness.Application.Authentication.Abstractions;

public interface IPasswordProvider
{
    Task<PasswordSignInResult> PasswordSignInAsync(string email, string password, bool rememberMe, CancellationToken ct = default);
    Task<PasswordSignInResult> SignInWithEmailAsync(string email, CancellationToken ct = default);
    Task<CreateUserResult> CreateUserWithPasswordAsync(string email, string? password = null, CancellationToken ct = default);
    Task DeleteUserAsync(string userId, CancellationToken ct = default);
    Task SignInAsync(string userId, CancellationToken ct = default);
    Task SignOutAsync(CancellationToken ct = default);
}
