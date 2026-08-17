using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SGM.Application.BL.BC.Service;
using SGM.Domain.Entities;

namespace SGM.PL.GUI.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IProductoService _productoService;
        private readonly ICategoriaService _categoriaService;

        public ProductosController(IProductoService productoService, ICategoriaService categoriaService)
        {
            _productoService = productoService;
            _categoriaService = categoriaService;
        }

        public IActionResult Index()
        {
            if (!EsAdministrador()) return RedirectToAction("Login", "Account");

            var lista = _productoService.Listar();
            return View(lista);
        }

        public IActionResult Create()
        {
            if (!EsAdministrador()) return RedirectToAction("Login", "Account");

            ViewBag.Categorias = new SelectList(_categoriaService.Listar(), "IdCategoria", "Nombre");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Producto producto)
        {
            if (!EsAdministrador()) return RedirectToAction("Login", "Account");

            _productoService.Registrar(producto);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            if (!EsAdministrador()) return RedirectToAction("Login", "Account");

            var producto = _productoService.ObtenerPorId(id);
            if (producto == null) return NotFound();

            ViewBag.Categorias = new SelectList(_categoriaService.Listar(), "IdCategoria", "Nombre", producto.IdCategoria);
            return View(producto);
        }

        [HttpPost]
        public IActionResult Edit(Producto producto)
        {
            if (!EsAdministrador()) return RedirectToAction("Login", "Account");

            _productoService.Actualizar(producto);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (!EsAdministrador()) return RedirectToAction("Login", "Account");

            _productoService.Eliminar(id);
            return RedirectToAction("Index");
        }

        private bool EsAdministrador()
        {
            return HttpContext.Session.GetString("Rol") == "Administrador";
        }
    }
}