using Microsoft.AspNetCore.Mvc;

namespace TableTennisApp.Controllers
{
    public class QueueController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
