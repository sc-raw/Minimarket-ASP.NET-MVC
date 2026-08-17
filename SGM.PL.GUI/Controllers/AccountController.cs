using Microsoft.AspNetCore.Mvc;
using SGM.Application.BL.BE;
using SGM.Application.BL.BC.Service;

namespace SGM.PL.GUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public AccountController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginRequest request)
        {
            var response = _usuarioService.Login(request);

            if (!response.Success)
            {
                ViewBag.Error = response.Message;
                return View();
            }

            // Guardar datos en sesión
            HttpContext.Session.SetInt32("IdUsuario", response.IdUsuario);
            HttpContext.Session.SetString("Username", response.Username);
            HttpContext.Session.SetString("Rol", response.Rol);
            HttpContext.Session.SetString("Nombres", response.Nombres ?? "");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}