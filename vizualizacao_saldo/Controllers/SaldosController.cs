using vizualizacao_saldo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mf_dev_backend_2023.Controllers
{
    public class SaldosController : Controller
    {
        private readonly AppDbContext _context;
        public SaldosController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dados = await _context.Saldos.ToListAsync();

            return View(dados);
        }
    }
}