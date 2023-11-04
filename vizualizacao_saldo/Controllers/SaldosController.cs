using vizualizacao_saldo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vizualizacao_saldo;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Saldo saldo)
        {

            if (ModelState.IsValid)
            {
                _context.Saldos.Add(saldo);
               await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return View(saldo);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
                return NotFound();
            var dados = await _context.Saldos.FindAsync(id);
            if(dados == null)
                return NotFound();

            return View(dados);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Saldo saldo)
        {
            if(id != saldo.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Saldos.Update(saldo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var dados = await _context.Saldos.FindAsync(id);

            if(dados == null)
                return NotFound();

            return View(dados);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var dados = await _context.Saldos.FindAsync(id);

            if (dados == null)
                return NotFound();

            return View(dados);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
                return NotFound();

            var dados = await _context.Saldos.FindAsync(id);

            if (dados == null)
                return NotFound();

            _context.Saldos.Remove(dados);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index");
        }



    }
}