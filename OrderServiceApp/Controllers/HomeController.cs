using Microsoft.AspNetCore.Mvc;

namespace OrderServiceApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
