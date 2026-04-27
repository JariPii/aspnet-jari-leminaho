using System.Security.Claims;
using CoreFitness.Application.Interfaces;
using CoreFitness.Web.ViewModels.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Web.Controllers;

[Authorize]
public class ProfileController(IUserService userService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var authId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var userResult = await userService.GetByAuthenticationId(authId);
        if(!userResult.IsSuccess)
            return NotFound();

        var statsResult = await userService.GetStatisticsAsync(userResult.Value!.Id);

        var vm = new ProfilePageViewModel
        {
            Id = userResult.Value!.Id,
            FirstName = userResult.Value.FirstName,
            LastName = userResult.Value.LastName,
            Email = userResult.Value.Email,
            PhoneNumber = userResult.Value.PhoneNumber,
            PhotoUrl = userResult.Value.PhotoUrl,
            Statistics = statsResult.IsSuccess ? statsResult.Value : null
        };

        return View(vm);
    }
}
