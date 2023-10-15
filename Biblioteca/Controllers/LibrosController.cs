using Biblioteca.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Controllers
{
    public class LibrosController : Controller
    {
        private readonly DBContext _context;

        public LibrosController(DBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> RegistroDeLibros(string buscar)
        {
            ViewData["Buscar"] = buscar;

            var _clientes = from a in _context.Tabla_Libros select a;
            _clientes = _clientes.OrderBy(a => a.Nombre);

            if (!String.IsNullOrEmpty(buscar))
            {
                _clientes = _clientes.Where(a => a.Nombre.Contains(buscar));
            }
            return View(await _clientes.AsNoTracking().ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> DetallesLibro(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cliente = _context.Tabla_Libros.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DetallesLibro()
        {
            return RedirectToAction(nameof(RegistroDeLibros));
        }

        [HttpGet]
        public async Task<IActionResult> CrearLibro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearLibro(VariablesLibro ordenes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordenes);
                _context.SaveChanges();
                return RedirectToAction("RegistroDeLibros");
            }
            return View();
        }

        public async Task<IActionResult> ModificarLibro(int? id)
        {
            if (id == null || _context.Tabla_Libros == null)
            {
                return NotFound();
            }

            var variablesLibro = await _context.Tabla_Libros.FindAsync(id);
            if (variablesLibro == null)
            {
                return NotFound();
            }
            return View(variablesLibro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModificarLibro(int id, [Bind("ID,Nombre,Autor")] VariablesLibro variablesLibro)
        {
            if (id != variablesLibro.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(variablesLibro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VariablesLibroExists(variablesLibro.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(RegistroDeLibros));
            }
            return View(variablesLibro);
        }

        private bool VariablesLibroExists(int iD)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<IActionResult> EliminarLibro(int? id)
        {
            if (id == null || _context.Tabla_Libros == null)
            {
                return NotFound();
            }

            var variablesLibro = await _context.Tabla_Libros
                .FirstOrDefaultAsync(m => m.ID == id);
            if (variablesLibro == null)
            {
                return NotFound();
            }

            return View(variablesLibro);
        }

        [HttpPost, ActionName("EliminarLibro")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarLibroConfirmed(int id)
        {
            if (_context.Tabla_Libros == null)
            {
                return Problem("Entity set 'DBContext.Tabla_Libros'  is null.");
            }
            var variablesLibro = await _context.Tabla_Libros.FindAsync(id);
            if (variablesLibro != null)
            {
                _context.Tabla_Libros.Remove(variablesLibro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(RegistroDeLibros));
        }
    }
}
