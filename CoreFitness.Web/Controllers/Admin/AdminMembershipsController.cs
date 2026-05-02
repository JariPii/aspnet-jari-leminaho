using CoreFitness.Application.Interfaces;
using CoreFitness.Web.ViewModels.Admin.Memberships;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Web.Controllers.Admin;

[Authorize(Roles = "Admin")]
[Route("Admin/Memberships")]
public class AdminMembershipsController(IMembershipService membershipService, IMembershipTypeService membershipTypeService) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var result = await membershipTypeService.GetAllAsync();

        var vm = new AdminMembershipsViewModel
        {
            MembershipTypes = result.IsSuccess ? result.Value : []
        };

        return View(vm);
    }
}
