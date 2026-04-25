using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Web.Controllers
{
    [Route("memberships")]
    public class MembershipController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}