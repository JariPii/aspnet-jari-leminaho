using CoreFitness.Domain.Common;

namespace CoreFitness.Application.Identity
{
    public interface IAuthService
    {
        Task<Result> RegisterAsync(string email, string password, string firstName, string lastName, CancellationToken ct = default);
        Task<Result> LoginAsync(string email, string password, bool rememberMe = false, CancellationToken ct = default);
        Task LogOutAsync();
    }
}
