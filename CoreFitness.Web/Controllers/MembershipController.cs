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
    }
}