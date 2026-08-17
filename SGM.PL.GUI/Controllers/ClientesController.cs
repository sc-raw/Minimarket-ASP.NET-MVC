using Microsoft.AspNetCore.Mvc;
using SGM.Application.BL.BC.Service;
using SGM.Domain.Entities;

namespace SGM.PL.GUI.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IClienteService _service;

        public ClientesController(IClienteService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            if (!EstaLogueado()) return RedirectToAction("Login", "Account");

            var lista = _service.Listar();
            return View(lista);
        }

        public IActionResult Create()
        {
            if (!EstaLogueado()) return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Cliente cliente)
        {
            if (!EstaLogueado()) return RedirectToAction("Login", "Account");

            _service.Registrar(cliente);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            if (!EstaLogueado()) return RedirectToAction("Login", "Account");

            var cliente = _service.ObtenerPorId(id);
            if (cliente == null) return NotFound();

            return View(cliente);
        }

        [HttpPost]
        public IActionResult Edit(Cliente cliente)
        {
            if (!EstaLogueado()) return RedirectToAction("Login", "Account");

            _service.Actualizar(cliente);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (!EsAdministrador()) return RedirectToAction("Login", "Account");

            _service.Eliminar(id);
            return RedirectToAction("Index");
        }

        private bool EstaLogueado()
        {
            return HttpContext.Session.GetString("Username") != null;
        }

        private bool EsAdministrador()
        {
            return HttpContext.Session.GetString("Rol") == "Administrador";
        }
    }
}