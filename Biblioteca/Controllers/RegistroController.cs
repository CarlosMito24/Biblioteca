using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Modelos;

namespace Biblioteca.Controllers
{
    public class RegistroController : Controller
    {
        private readonly DBContext _context;

        public RegistroController(DBContext context)
        {
            _context = context;
        }
        public async Task <IActionResult> RegistroDePrestamos()
        {
            var dbContext1 = _context.Tabla_Registros.Include(v => v.VariablesLibro).Include(v => v.VariablesUsuarios);
            return View(await dbContext1.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> DetallesRegistro(int? id)
        {
            if (id == null || _context.Tabla_Registros == null)
            {
                return NotFound();
            }

            var variablesRegistro = await _context.Tabla_Registros
                .Include(v => v.VariablesLibro)
                .Include(v => v.VariablesUsuarios)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (variablesRegistro == null)
            {
                return NotFound();
            }

            return View(variablesRegistro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DetallesRegistro()
        {
            return RedirectToAction(nameof(RegistroDePrestamos));
        }

        public IActionResult CrearRegistro()
        {
            ViewData["VariablesUsuariosID"] = new SelectList(_context.Tabla_Usuarios, "ID", "Nombre");
            ViewData["VariablesLibroID"] = new SelectList(_context.Tabla_Libros, "ID", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearRegistro([Bind("ID,VariablesUsuariosID,VariablesLibroID")] VariablesRegistro variablesRegistro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(variablesRegistro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(RegistroDePrestamos));
            }
            ViewData["VariablesUsuariosID"] = new SelectList(_context.Tabla_Usuarios, "ID", "Nombre", variablesRegistro.VariablesUsuariosID);
            ViewData["VariablesLibroID"] = new SelectList(_context.Tabla_Libros, "ID", "Nombre", variablesRegistro.VariablesLibroID);
            return View(variablesRegistro);
        }

        [HttpGet]
        public async Task<IActionResult> ModificarRegistro(int? id)
        {
            if (id == null || _context.Tabla_Registros == null)
            {
                return NotFound();
            }

            var variablesRegistro = await _context.Tabla_Registros.FindAsync(id);
            if (variablesRegistro == null)
            {
                return NotFound();
            }
            ViewData["VariablesLibroID"] = new SelectList(_context.Tabla_Libros, "ID", "Nombre", variablesRegistro.VariablesLibroID);
            ViewData["VariablesUsuariosID"] = new SelectList(_context.Tabla_Usuarios, "ID", "Nombre", variablesRegistro.VariablesUsuariosID);
            return View(variablesRegistro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModificarRegistro(int id, [Bind("ID,VariablesUsuariosID,VariablesLibroID")] VariablesRegistro variablesRegistro)
        {
            if (id != variablesRegistro.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(variablesRegistro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VariablesRegistroExists(variablesRegistro.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(RegistroDePrestamos));
            }
            ViewData["VariablesLibroID"] = new SelectList(_context.Tabla_Libros, "ID", "Nombre", variablesRegistro.VariablesLibroID);
            ViewData["VariablesUsuariosID"] = new SelectList(_context.Tabla_Usuarios, "ID", "Nombre", variablesRegistro.VariablesUsuariosID);
            return View(variablesRegistro);
        }

        private bool VariablesRegistroExists(int id)
        {
            return _context.Tabla_Registros.Any(e => e.ID == id);
        }

        [HttpGet]
        public async Task<IActionResult> EliminarRegistro(int? id)
        {
            if (id == null || _context.Tabla_Registros == null)
            {
                return NotFound();
            }

            var variablesRegistro = await _context.Tabla_Registros
                .Include(v => v.VariablesLibro)
                .Include(v => v.VariablesUsuarios)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (variablesRegistro == null)
            {
                return NotFound();
            }

            return View(variablesRegistro);
        }

        [HttpPost, ActionName("EliminarRegistro")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarRegistroConfirmed(int id)
        {
            if (_context.Tabla_Registros == null)
            {
                return Problem("Entity set 'DBContext.Tabla_Registros'  is null.");
            }
            var variablesRegistro = await _context.Tabla_Registros.FindAsync(id);
            if (variablesRegistro != null)
            {
                _context.Tabla_Registros.Remove(variablesRegistro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(RegistroDePrestamos));
        }
    }
}
