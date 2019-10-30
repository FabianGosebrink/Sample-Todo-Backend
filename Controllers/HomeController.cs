using Microsoft.AspNetCore.Mvc;

namespace server.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger/");
        }
    }
}