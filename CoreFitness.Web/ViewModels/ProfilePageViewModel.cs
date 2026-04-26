using CoreFitness.Application.DTOs.User;

namespace CoreFitness.Web.ViewModels;

public class ProfilePageViewModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? PhotoUrl { get; set; }
    public UserStatisticsDTO? Statistics { get; set; }
}
