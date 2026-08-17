using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SGM.Application.BL.BE;
using SGM.Application.BL.BC.Service;

namespace SGM.PL.GUI.Controllers
{
    public class VentasController : Controller
    {
        private readonly IVentaService _ventaService;
        private readonly IProductoService _productoService;
        private readonly IClienteService _clienteService;

        public VentasController(
            IVentaService ventaService,
            IProductoService productoService,
            IClienteService clienteService)
        {
            _ventaService = ventaService;
            _productoService = productoService;
            _clienteService = clienteService;
        }

        public IActionResult Index()
        {
            if (!EstaLogueado()) return RedirectToAction("Login", "Account");

            var lista = _ventaService.Listar();
            return View(lista);
        }

        public IActionResult Create()
        {
            if (!EstaLogueado()) return RedirectToAction("Login", "Account");

            ViewBag.Clientes = new SelectList(_clienteService.Listar(), "Id", "Nombres");
            ViewBag.Productos = _productoService.Listar().Where(p => p.Estado && p.Stock > 0).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create([FromBody] CrearVentaRequest request)
        {
            if (!EstaLogueado())
                return Json(new { success = false, message = "Sesión expirada" });

            try
            {
                // Asignar el usuario logueado
                request.IdUsuario = HttpContext.Session.GetInt32("IdUsuario") ?? 0;

                if (request.IdUsuario == 0)
                    return Json(new { success = false, message = "Usuario no válido" });

                var idVenta = _ventaService.Registrar(request);

                return Json(new { success = true, message = "Venta registrada correctamente", idVenta });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public IActionResult Details(long id)
        {
            if (!EstaLogueado()) return RedirectToAction("Login", "Account");

            var venta = _ventaService.ObtenerPorId(id);
            if (venta == null) return NotFound();

            return View(venta);
        }

        public IActionResult Anular(long id)
        {
            if (!EsAdministrador()) return RedirectToAction("Login", "Account");

            _ventaService.Anular(id);
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