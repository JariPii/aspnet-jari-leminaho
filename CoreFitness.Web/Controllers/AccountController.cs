using DomainUser = CoreFitness.Domain.Entities.Users.User;
using CoreFitness.Domain.Interfaces.UnitOfWork;
using CoreFitness.Domain.Interfaces.Users;
using CoreFitness.Infrastructure.Identity;
using CoreFitness.Web.ViewModels.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CoreFitness.Domain.Entities.Users.ValueObjects;
using CoreFitness.Domain.Enums;

namespace CoreFitness.Web.Controllers;

public class AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ILogger<AccountController> logger, IUserRepository userRepository, IUnitOfWork unitOfWork) : Controller
{
    [HttpGet]
    public async Task<IActionResult> SignUp()
    {
        var schemes = await signInManager.GetExternalAuthenticationSchemesAsync();
        return View(new SignUpViewModel
        {
            ExternalProviders = [..schemes.Select(x => x.Name)]
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> SignUp(SignUpViewModel vm)
    {
        if(!ModelState.IsValid)
            return View(vm);

        var existingUser = await userManager.FindByEmailAsync(vm.Email);
        if(existingUser is not null)
        {
            ModelState.AddModelError(nameof(vm.Email), "An account with this email already exists");
            return View(vm);
        }

        TempData["VerifyEmail"] = vm.Email;

        return RedirectToAction(nameof(VerifyEmail), new { email = vm.Email });
    }

    [HttpGet]
    public IActionResult VerifyEmail(string email) => View(new VerifyEmailViewModel { Email = email });

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult VerifyEmail(VerifyEmailViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        if (!string.Equals(vm.Code, "123456", StringComparison.Ordinal))
        {
            ModelState.AddModelError(nameof(vm.Code), "Wrong code");
            return View(vm);
        }

        return RedirectToAction(nameof(SetPassword), new { email = vm.Email});
    }

    [HttpGet]
    public IActionResult SetPassword(string email) => View(new SetUpPasswordViewModel { Email = email });

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> SetPassword(SetUpPasswordViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var user = new ApplicationUser
        {
            UserName = vm.Email,
            Email = vm.Email,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, vm.Password);

        if(!result.Succeeded)
        {
            foreach(var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(vm);
        }

        var coreUser = DomainUser.Create(
            AuthenticationId.Create(user.Id.ToString()),
            UserEmail.Create(user.Email!),
            UserName.Create("",""),
            null,
            null,
            UserRole.Member
        );

        await userRepository.AddAsync(coreUser);
        await unitOfWork.SaveChangesAsync();

        await signInManager.SignInAsync(user, isPersistent: false);
        return RedirectToAction(nameof(SignIn), new { returnUrl = "/"});
    }

    [HttpGet]
    public async Task<IActionResult> SignIn(string? returnUrl = null)
    {
        var schema = await signInManager.GetExternalAuthenticationSchemesAsync();

        return View(new SignInViewModel
        {
            ReturnUrl = returnUrl,
            ExternalProviders = [..schema.Select(x => x.Name)]
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> SignIn(SignInViewModel vm)
    {
        if(!ModelState.IsValid)
            return View(vm);

        var result = await signInManager.PasswordSignInAsync(
            vm.Email,
            vm.Password,
            isPersistent: false,
            lockoutOnFailure: true
        );

        if(result.Succeeded)
            return RedirectToLocal(vm.ReturnUrl);

        if(result.IsLockedOut)
        {
            logger.LogWarning("User {Email} is locked out", vm.Email);
            ModelState.AddModelError(string.Empty, "Account locked. Try again later");
            return View(vm);
        }

        ModelState.AddModelError(string.Empty, "Invalid email or password");

        return View(vm);
    }

    private IActionResult RedirectToLocal(string? returnUrl)
    {
        if(Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction("Index", "Home");
    }
}
