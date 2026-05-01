using CoreFitness.Application.DTOs.TrainingSession;
using CoreFitness.Application.Interfaces;
using CoreFitness.Web.ViewModels.Admin.Sessions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController(ITrainingSessionService trainingSessionService) : Controller
{
    private readonly ITrainingSessionService trainingSessionService = trainingSessionService;

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Sessions()
    {
        var result = await trainingSessionService.GetUpcomingAsync();

        var vm = new AdminSessionsViewModel
        {
            Sessions = result.IsSuccess ? result.Value : []
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSession(CreateSessionViewModel vm)
    {
        if(!ModelState.IsValid)
            return RedirectToAction(nameof(Sessions));

        var startDate = DateTime.SpecifyKind(vm.StartDate, DateTimeKind.Local);
        var startDateOffset = new DateTimeOffset(startDate);

        var dto = new CreateTrainingSessionDTO
        {
            Name = vm.Name,
            Description = vm.Description,
            Capacity = vm.Capacity,
            StartDate = startDateOffset,
            DurationInMinutes = vm.DurationInMinutes
        };

        var result = await trainingSessionService.CreateAsync(dto);

        if(!result.IsSuccess)
            TempData["Error"] = result.Error!.Message;

        return RedirectToAction(nameof(Sessions));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteSession(Guid id)
    {
        await trainingSessionService.DeleteAsync(id);

        return RedirectToAction(nameof(Sessions));
    }

    [HttpGet]
    public async Task<IActionResult> EditSession(Guid id)
    {
        var result = await trainingSessionService.GetByIdAsync(id);

        if(!result.IsSuccess)
            return NotFound();

        var session = result.Value;

        var vm = new UpdateSessionViewModel
        {
            Id = session.Id,
            Name = session.Name,
            Description = session.Description,
            StartDate = session.StartDate.DateTime,
            Capacity = session.Capacity,
            DurationInMinutes = session.DurationInMinutes
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateSession(UpdateSessionViewModel vm)
    {
        if(!ModelState.IsValid)
            return View("EditSession", vm);

        var startDate = DateTime.SpecifyKind(vm.StartDate, DateTimeKind.Local);
        var startDateOffset = new DateTimeOffset(startDate);

        var dto = new UpdateTrainingSessionDTO
        {
            Id = vm.Id,
            Name = vm.Name,
            Description = vm.Description,
            StartDate = startDateOffset,
            Capacity = vm.Capacity,
            DurationInMinutes = vm.DurationInMinutes
        };

        var result = await trainingSessionService.UpdateAsync(dto);

        if(!result.IsSuccess)
        {
            TempData["Error"] = result.Error!.Message;
            return View("EditSession", vm);
        };

        return RedirectToAction(nameof(Sessions));
    }
}
