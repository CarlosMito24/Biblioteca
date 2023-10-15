using Biblioteca.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly DBContext _context;

        public UsuariosController(DBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> RegistroDeUsuarios(string buscar)
        {
            ViewData["Buscar"] = buscar;

            var _clientes = from a in _context.Tabla_Usuarios select a;
            _clientes = _clientes.OrderBy(a => a.Nombre);

            if (!String.IsNullOrEmpty(buscar))
            {
                _clientes = _clientes.Where(a => a.Nombre.Contains(buscar) || a.Apellido.Contains(buscar));
            }
            return View(await _clientes.AsNoTracking().ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> DetallesUsuario(int? id)
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
        public IActionResult DetallesUsuario()
        {
            return RedirectToAction(nameof(RegistroDeUsuarios));
        }

        [HttpGet]
        public async Task<IActionResult> CrearUsuario()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearUsuario(VariablesUsuarios ordenes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordenes);
                _context.SaveChanges();
                return RedirectToAction("RegistroDeUsuarios");
            }
            return View();
        }
    }
}