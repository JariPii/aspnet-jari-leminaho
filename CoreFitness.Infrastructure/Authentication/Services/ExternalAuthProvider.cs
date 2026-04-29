using System.IO.Compression;
using System.Security.Claims;
using CoreFitness.Application.Authentication.Abstractions;
using CoreFitness.Application.Authentication.Models;
using CoreFitness.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace CoreFitness.Infrastructure.Authentication.Services;

public class ExternalAuthProvider(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ILogger<ExternalAuthProvider> logger) : IExternalAuthProvider
{
    public AuthenticationProperties ConfigureExternalLogin(string provider, string redirectUrl) =>
        signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

    public async Task<IReadOnlyList<string>> GetExternalProvidersAsync(CancellationToken ct = default)
    {
        var schemes = await signInManager.GetExternalAuthenticationSchemesAsync();
        return [..schemes.Select(x => x.Name)];
    }

    public async Task<ExternalUserInfo?> GetExternalUserAsync(CancellationToken ct = default)
    {
        var info = await signInManager.GetExternalLoginInfoAsync();

        if(info is null)
        {
            logger.LogWarning("External login info was null");
            return null;
        }

        var email = info.Principal.FindFirstValue(ClaimTypes.Email);

        if(string.IsNullOrWhiteSpace(email))
        {
            logger.LogWarning("No email claim fomr {Provider}", info.LoginProvider);
            return null;
        }

        var firstName = info.Principal.FindFirstValue(ClaimTypes.GivenName) ?? "";
        var lastName = info.Principal.FindFirstValue(ClaimTypes.Surname) ?? "";
        var picture = info.Principal.FindFirstValue("picture");

        return new ExternalUserInfo
        {
            Email = email,
            Provider = info.LoginProvider,
            ProviderKey = info.ProviderKey,
            FirstName = firstName,
            LastName = lastName,
            PhotoUrl = picture
        };
    }

    public async Task<ExternalSignInResult> SignInWithExternalAsync(string provider, string providerKey, CancellationToken ct = default)
    {
        var result = await signInManager.ExternalLoginSignInAsync(
            provider,
            providerKey,
            isPersistent: false,
            bypassTwoFactor: true
        );

        return result.Succeeded
            ? ExternalSignInResult.Success()
            : ExternalSignInResult.Failed();
    }

    public async Task<LinkResult> LinkExternalLoginAsync(string provider, string providerKey, string userId, CancellationToken ct = default)
    {
        var user = await userManager.FindByIdAsync(userId);

        if(user is null)
        {
            logger.LogError("LinkExternalLogin failed. User not found: {UserId}", userId);
            return LinkResult.Failed("User nod found");
        }

        var info = new UserLoginInfo(provider, providerKey, provider);

        var result = await userManager.AddLoginAsync(user, info);

        if(!result.Succeeded)
        {
            logger.LogError("Failed to link {Provider} to {UserId}: {Errors}", provider, userId,
            string.Join(", ", result.Errors.Select(e => e.Description)));

            return LinkResult.Failed();
        }

        return LinkResult.Success();
    }

    public async Task<LinkResult> RemoveExternalLoginAsync(string provider, string providerKey, string userId, CancellationToken ct = default)
    {
        var user = await userManager.FindByIdAsync(userId);

        if(user is null)
        {
            logger.LogWarning("RemoveExternalLogin failed. User not found: {UserId}", userId);
            return LinkResult.Failed("User not found");
        }

        var result = await userManager.RemoveLoginAsync(user, provider, providerKey);

        if(!result.Succeeded)
        {
            logger.LogError("Failed to remove {Provider} from {UserId}: {Errors}",
            provider, userId, string.Join(", ", result.Errors.Select(e => e.Description)));

            return LinkResult.Failed("Failed");
        }

        return LinkResult.Success();
    }
}
