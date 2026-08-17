using Microsoft.AspNetCore.Mvc;
using SGM.Application.BL.BC.Service;
using SGM.Domain.Entities;

namespace SGM.PL.GUI.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ICategoriaService _service;

        public CategoriasController(ICategoriaService service)
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
        public IActionResult Create(Categoria categoria)
        {
            if (!EsAdministrador()) return RedirectToAction("Login", "Account");

            _service.Registrar(categoria);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            if (!EsAdministrador()) return RedirectToAction("Login", "Account");

            var categoria = _service.ObtenerPorId(id);
            if (categoria == null) return NotFound();

            return View(categoria);
        }

        [HttpPost]
        public IActionResult Edit(Categoria categoria)
        {
            if (!EsAdministrador()) return RedirectToAction("Login", "Account");

            _service.Actualizar(categoria);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (!EsAdministrador()) return RedirectToAction("Login", "Account");

            _service.Eliminar(id);
            return RedirectToAction("Index");
        }

        private bool EsAdministrador()
        {
            return HttpContext.Session.GetString("Rol") == "Administrador";
        }
    }
}