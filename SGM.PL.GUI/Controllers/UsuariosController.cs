using Microsoft.AspNetCore.Mvc;
using SGM.Application.BL.BC.Service;
using SGM.Domain.Entities;

namespace SGM.PL.GUI.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioService _service;

        public UsuariosController(IUsuarioService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            if (!EsAdministrador()) return RedirectToAction("Login", "Account");

            var lista = _service.Listar();
            return View(lista);
        }

        public IActionResult Create()
        {
            if (!EsAdministrador()) return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Usuario usuario)
        {
            if (!EsAdministrador()) return RedirectToAction("Login", "Account");

            // Validación básica
            if (string.IsNullOrWhiteSpace(usuario.Username) || string.IsNullOrWhiteSpace(usuario.Password))
            {
                ViewBag.Error = "Username y Password son obligatorios";
                return View(usuario);
            }

            _service.Registrar(usuario);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            if (!EsAdministrador()) return RedirectToAction("Login", "Account");

            var usuario = _service.ObtenerPorId(id);
            if (usuario == null) return NotFound();

            return View(usuario);
        }

        [HttpPost]
        public IActionResult Edit(Usuario usuario)
        {
            if (!EsAdministrador()) return RedirectToAction("Login", "Account");

            _service.Actualizar(usuario);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (!EsAdministrador()) return RedirectToAction("Login", "Account");

            // Evitar que se elimine a sí mismo
            var idLogueado = HttpContext.Session.GetInt32("IdUsuario");
            if (idLogueado == id)
            {
                TempData["Error"] = "No puedes eliminar tu propio usuario";
                return RedirectToAction("Index");
            }

            _service.Eliminar(id);
            return RedirectToAction("Index");
        }

        private bool EsAdministrador()
        {
            return HttpContext.Session.GetString("Rol") == "Administrador";
        }
    }
}