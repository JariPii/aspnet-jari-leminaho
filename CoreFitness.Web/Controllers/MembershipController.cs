using CoreFitness.Application.DTOs.Membership;
using CoreFitness.Application.Interfaces;
using CoreFitness.Web.Extensions;
using CoreFitness.Web.ViewModels.Membership;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Web.Controllers
{
    [Route("memberships")]
    public class MembershipController(IMembershipService membershipService,IUserService userService, IFaqService faqService) : Controller
    {
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var membershipResult = await membershipService.GetMembershipTypesAsync();
            var faqItems = await faqService.GetFaqItemsAsync();

            var viewModel = new MembershipPageViewModel
            {
                MembershipTypes = membershipResult.IsSuccess ? membershipResult.Value! : [],
                FaqItems = faqItems
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost("Join")]
        public async Task<IActionResult> Join(Guid membershipTypeId, CancellationToken ct = default)
        {
            var authId = User.GetAuthenticationId();

            var dto = new CreateMembershipDTO
            {
                MembershipTypeId = membershipTypeId
            };

            var result = await membershipService.CreateAsync(authId, dto, ct);

            if(!result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Index", "Profile");
        }

        [Authorize]
        [HttpPost("Cancel")]
        public async Task<IActionResult> Cancel(CancellationToken ct = default)
        {
            var authId = User.GetAuthenticationId();

            var userResult = await userService.GetByAuthenticationId(authId, ct);

            if(!userResult.IsSuccess)
            {
                TempData["Error"] = "User not found";

                return RedirectToAction("Index", "Profile");
            }

            var result = await membershipService.DeactivateAsync(userResult.Value!.Id, ct);

            if(!result.IsSuccess)
            {
                TempData["Error"] = result.Error?.Message ?? "Could not cancel membership";
            }

            TempData["Success"] = "Membeship cancelled";
            return RedirectToAction("Index", "Profile");
        }
    }
}