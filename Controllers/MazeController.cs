using Microsoft.AspNetCore.Mvc;

namespace AnotherTechblog.Controllers
{
    public class MazeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}