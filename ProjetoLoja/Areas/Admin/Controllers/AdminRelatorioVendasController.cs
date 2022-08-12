using Microsoft.AspNetCore.Mvc;
using ProjetoLoja.Areas.Admin.Services;

namespace ProjetoLoja.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminRelatorioVendasController : Controller
    {
        private readonly RelatorioVendasService _relatorioVendasService;

        public AdminRelatorioVendasController(RelatorioVendasService relatorioVendasService)
        {
            _relatorioVendasService = relatorioVendasService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RelatorioVendasSimples(DateTime? inicialDate,
           DateTime? finalDate)
        {
            if (!inicialDate.HasValue)
            {
                inicialDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!finalDate.HasValue)
            {
                finalDate = DateTime.Now;
            }

            ViewData["inicialDate"] = inicialDate.Value.ToString("yyyy-MM-dd");
            ViewData["finalDate"] = finalDate.Value.ToString("yyyy-MM-dd");

            var result = await _relatorioVendasService.FindByDateAsync(inicialDate, finalDate);
            return View(result);
        }
    }
}
