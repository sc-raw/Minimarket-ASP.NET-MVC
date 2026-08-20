using Microsoft.AspNetCore.Mvc;

namespace SGM.PL.GUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.Rol = HttpContext.Session.GetString("Rol");
            ViewBag.Nombres = HttpContext.Session.GetString("Nombres");
            return View();
        }
    }
}