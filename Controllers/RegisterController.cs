using Microsoft.AspNetCore.Mvc;

namespace ResumeManager.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
