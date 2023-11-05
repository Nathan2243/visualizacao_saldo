using vizualizacao_saldo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vizualizacao_saldo;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace vizualizacao_saldo.Controllers
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

        // criação do exportar para Excel, linhas e colunas, em return File foi adicionado um link que será o download Excel.
        public IActionResult Exportar()
        {
            var dados = GetDados();

            using (XLWorkbook workBook = new XLWorkbook())
            {
                workBook.AddWorksheet(dados,"Relatório de Despesas");

                using (MemoryStream ms = new MemoryStream()) 
                {
                    workBook.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Cadastro_Despesa.xls");
                }

            }
        }
        // Colunas e linhas adicionadas para a exportação do relatório em Excel
        private DataTable GetDados()
        {
            DataTable dataTable = new DataTable();

            dataTable.TableName = "Dados Saldos";
            dataTable.Columns.Add("Valor", typeof(int));
            dataTable.Columns.Add("Resumo", typeof(string));
            dataTable.Columns.Add("Tipo_Saldo", typeof(string));

            var dados = _context.Saldos.ToList();
            
            if(dados.Count > 0)
            {
                dados.ForEach(Despesas =>
                {
                    dataTable.Rows.Add(Despesas.Valor, Despesas.Resumo, Despesas.Tipo_Saldo);
                });
            }



            return dataTable;

        }


    }
}