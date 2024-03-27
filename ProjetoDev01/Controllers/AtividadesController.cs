using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoDev01.Models;
using ProjetoDev1.Models;

namespace ProjetoDev01.Controllers
{
    public class AtividadesController : Controller
    {
        private readonly BancoDados _context;

        public AtividadesController(BancoDados context)
        {
            _context = context;
        }

        public IActionResult EmitirNotas()
        {
            return View("~/Views/Home/EmetirNota.cshtml");
        }

        [HttpGet("buscaCodigo")]
        public async Task<IActionResult> BuscaCodigo(string codigoAtividade)
        {
            var atividadeEncontrada = await _context.CadastroAtividades.FirstOrDefaultAsync(
                m=>m.CodAtividade == codigoAtividade
                );

            if (atividadeEncontrada != null)
            {
                return Json(atividadeEncontrada);
            }
            return NotFound();
        }


        [HttpGet("listarAtividades")]
        // GET: Atividades
        public async Task<IActionResult> Index()
        {
            return View(await _context.CadastroAtividades.ToListAsync());
        }

        // GET: Atividades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cadAtividade = await _context.CadastroAtividades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cadAtividade == null)
            {
                return NotFound();
            }

            return View(cadAtividade);
        }

        // GET: Atividades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Atividades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CodAtividade,Grupo,CodigoFederal,Descricao,AtivPrevistaExcecao,RetencaoObrigatoria,Aliquota")] CadastroAtividade cadAtividade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cadAtividade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cadAtividade);
        }

        // GET: Atividades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cadAtividade = await _context.CadastroAtividades.FindAsync(id);
            if (cadAtividade == null)
            {
                return NotFound();
            }
            return View(cadAtividade);
        }

        // POST: Atividades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CodAtividade,Grupo,CodigoFederal,Descricao,AtivPrevistaExcecao,RetencaoObrigatoria,Aliquota")] CadastroAtividade cadAtividade)
        {
            if (id != cadAtividade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cadAtividade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CadAtividadeExists(cadAtividade.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cadAtividade);
        }

        // GET: Atividades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cadAtividade = await _context.CadastroAtividades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cadAtividade == null)
            {
                return NotFound();
            }

            return View(cadAtividade);
        }

        // POST: Atividades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cadAtividade = await _context.CadastroAtividades.FindAsync(id);
            if (cadAtividade != null)
            {
                _context.CadastroAtividades.Remove(cadAtividade);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CadAtividadeExists(int id)
        {
            return _context.CadastroAtividades.Any(e => e.Id == id);
        }

        [HttpGet("simulacao")]
        public IActionResult Simulacao()
        {
            return View("~/Views/Atividades/Simulacao.cshtml");
        }
    }
}
