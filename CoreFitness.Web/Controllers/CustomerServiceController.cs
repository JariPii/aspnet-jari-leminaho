using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Web.Controllers;

public class CustomerServiceController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
