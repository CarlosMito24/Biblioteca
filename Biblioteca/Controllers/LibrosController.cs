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
            var cliente = _context.Tabla_Usuarios.Find(id);
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
    }
}
