using Microsoft.AspNetCore.Mvc;
using SGM.Application.BL.BC.Service;

namespace SGM.PL.GUI.Controllers
{
    public class ReportesController : Controller
    {
        private readonly IReporteService _reporteService;

        public ReportesController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
                return RedirectToAction("Login", "Account");

            var resumen = _reporteService.ObtenerResumen();
            return View(resumen);
        }
    }
}