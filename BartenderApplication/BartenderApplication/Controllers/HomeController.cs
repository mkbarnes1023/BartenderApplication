using Microsoft.AspNetCore.Mvc;

namespace BartenderApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); // Shows homepage with links to menu and order queue
        }
    }
}
