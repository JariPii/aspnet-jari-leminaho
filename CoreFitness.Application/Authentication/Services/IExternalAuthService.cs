using CoreFitness.Application.Authentication.Models;

namespace CoreFitness.Application.Authentication.Services;

public interface IExternalAuthService
{
    Task<IReadOnlyList<string>> GetExternalProvidersAsync();
    Task<AuthenticationResult> HandleExternalLoginCallbackAsync(string? returnUrl = null, string? remoteError = null);
    Task<AuthenticationResult> VerifyExternaLoginAsync(string code, string? returnUrl = null);
    Task SignOutAsync();
}
