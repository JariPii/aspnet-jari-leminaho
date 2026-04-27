using System.Security.Claims;
using CoreFitness.Application.DTOs.User;
using CoreFitness.Application.Interfaces;
using CoreFitness.Infrastructure.Identity;
using CoreFitness.Web.ViewModels.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Web.Controllers;

[Authorize]
public class ProfileController(IUserService userService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<ProfileController> logger) : Controller
{
    public async Task<IActionResult> Index()
    {
        var authId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var userResult = await userService.GetByAuthenticationId(authId);
        if(!userResult.IsSuccess)
        {
            await signInManager.SignOutAsync();

            TempData["Error"] = "Your account was not found. Please sign in again or create a new account";
            
            return RedirectToAction("SignIn", "Account");
        }

        var statsResult = await userService.GetStatisticsAsync(userResult.Value!.Id);

        var vm = new ProfilePageViewModel
        {
            Id = userResult.Value!.Id,
            FirstName = userResult.Value.FirstName,
            LastName = userResult.Value.LastName,
            Email = userResult.Value.Email,
            PhoneNumber = userResult.Value.PhoneNumber,
            PhotoUrl = userResult.Value.PhotoUrl,
            Statistics = statsResult.IsSuccess ? statsResult.Value : null,
            Weight = statsResult.IsSuccess ? statsResult.Value?.CurrentWeight : null,
            Height = statsResult.IsSuccess ? statsResult.Value?.Height : null,
            TargetWeight = statsResult.IsSuccess ? statsResult.Value?.TargetWeight : null
        };

        return View(vm);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(ProfilePageViewModel vm)
    {
        if(!ModelState.IsValid)
            return View("Index", vm);

        var dto = new UpdateUserDTO
        {
            Id = vm.Id,
            FirstName = vm.FirstName,
            LastName = vm.LastName,
            Email = vm.Email,
            PhoneNumber = vm.PhoneNumber
        };

        var result = await userService.UpdateAsync(dto);

        if(result is null || !result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result?.Error?.Message ?? "Update faile");

            return View("Index", vm);
        }

        if(vm.Weight.HasValue && vm.Height.HasValue)
        {
            var statsResult = await userService.UpdateWeightAsync(vm.Id, vm.Weight.Value, vm.Height.Value);

            if(statsResult is null || !statsResult.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, statsResult?.Error?.Message ?? "Stats update failed");

                return View("Index", vm);
            }
        }

        if(vm.TargetWeight.HasValue)
        {
            var targetResult = await userService.UpdateWeightGoalAsync(vm.Id, vm.TargetWeight.Value);

            if(targetResult is null || !targetResult.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, targetResult?.Error?.Message ?? "Target weight update failed");

                return View("Index", vm);
            }
        }  

        TempData["Success"] = "Profile updated successfully";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete()
    {
        var authId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var userResult = await userService.GetByAuthenticationId(authId);
        if(!userResult.IsSuccess)
        {
            await signInManager.SignOutAsync();

            TempData["Error"] = "Your account was not found.";
            
            return RedirectToAction("SignIn", "Account");
        }

        var result = await userService.DeleteAsync(userResult.Value!.Id);
        if(result is null || !result.IsSuccess)
        {
            TempData["Error"] = result?.Error?.Message ?? "Failed to delete account";
            
            return RedirectToAction(nameof(Index));
        }

        var identityUser = await userManager.FindByIdAsync(authId.ToString());
        if(identityUser is not null)
            await userManager.DeleteAsync(identityUser);

        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
