using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoDev1.Models;

namespace ProjetoDev01.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly BancoDados _context;

        public UsuariosController(BancoDados context)
        {
            _context = context;
        }

        [HttpGet("validarEmail")]
        public IActionResult validarEmail(string email)
        {
            using(BancoDados db = new BancoDados(
                new DbContextOptionsBuilder<BancoDados>().UseNpgsql("Host=localhost;Port=5432;Pooling=true;Database=PROJETO_DEVdb;User Id=postgres;Password=root;").Options)
                )
            {
                if(db.Usuarios.FirstOrDefault(m => m.Email == email) != null) // Se nenhum elemento atender à condição, ele retornará null
                {
                    return Json(new { existe = true });
                }
            }
            return Json(new { existe = false });
        }

        //Login
        [HttpPost("login")]
        public IActionResult Login(string email, string password)
        {
            // Lógica de autenticação aqui
            // Verifique o email e a senha no banco de dados ou em outro lugar
            using (var context = new BancoDados(new DbContextOptionsBuilder<BancoDados>().UseNpgsql("Host=localhost;Port=5432;Pooling=true;Database=PROJETO_DEVdb;User Id=postgres;Password=root;").Options))
            {
                // Executar consulta SQL e armazenar os resultados em uma lista de objetos Usuario
                var usuarios = context.Usuarios.ToList();

                foreach (var usuario in usuarios)
                {
                    if (email == usuario.Email && password == usuario.Senha)
                    {
                        // Login bem-sucedido
                        return Json(new { success = true });
                    }
                }
            }
            // Login falhou
            return Json(new { success = false });

        }

        // GET: Usuarios
        [HttpGet("listaUsuario")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Senha")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
