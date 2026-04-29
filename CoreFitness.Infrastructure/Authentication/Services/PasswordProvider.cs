using CoreFitness.Application.Authentication.Abstractions;
using CoreFitness.Application.Authentication.Models;
using CoreFitness.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace CoreFitness.Infrastructure.Authentication.Services;

public class PasswordProvider(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<PasswordProvider> logger) : IPasswordProvider
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly ILogger<PasswordProvider> _logger = logger;

    public async Task<PasswordSignInResult> PasswordSignInAsync(string email, string password, bool rememberMe, CancellationToken ct = default)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: true);

        return result switch
        {
            { Succeeded: true } => PasswordSignInResult.Succeeded,
            { RequiresTwoFactor: true } => PasswordSignInResult.RequiresTwoFactor,
            { IsLockedOut: true } => PasswordSignInResult.LockedOut,
            _ => PasswordSignInResult.Failed
        };
    }
    public async Task<CreateUserResult> CreateUserWithPasswordAsync(string email, string? password = null, CancellationToken ct = default)
    {
        var existing = await _userManager.FindByEmailAsync(email);

        if(existing is not null)
        {
            _logger.LogWarning("User already exists for {Email}", email);
            return CreateUserResult.Failed();
        }

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = password is null
        };

        IdentityResult result = password is null
            ? await _userManager.CreateAsync(user)
            : await _userManager.CreateAsync(user, password);

        if(!result.Succeeded)
        {
            _logger.LogWarning("Failed to create user {Email}: {Errors}", email, string.Join(", ", result.Errors.Select(e => e.Description)));
            return CreateUserResult.Failed();
        }

        return CreateUserResult.Success(user.Id.ToString());
    }

    
    public async Task<PasswordSignInResult> SignInWithEmailAsync(string email, CancellationToken ct = default)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if(user is null)
        {
            _logger.LogWarning("SignInByEmail failed. User not found: {Email}", email);
            return PasswordSignInResult.Failed;
        }

        await _signInManager.SignInAsync(user, isPersistent: false);

        return PasswordSignInResult.Succeeded;
    }

    
    public async Task SignInAsync(string userId, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if(user is null)
        {
            _logger.LogError("Signin failed. User not found: {UserId}", userId);
            return;
        }

        await _signInManager.SignInAsync(user, isPersistent: false);
    }

    public async Task SignOutAsync(CancellationToken ct = default) =>
        await _signInManager.SignOutAsync();

    public async Task DeleteUserAsync(string userId, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if(user is null)
        {
            _logger.LogWarning("Failed to delete user. User not found: {UserId}", userId);
            return;
        }

        var result = await _userManager.DeleteAsync(user);

        if(!result.Succeeded)
        {
            _logger.LogError("Failed to delete user {UserId}: {Errors}", userId, string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}
