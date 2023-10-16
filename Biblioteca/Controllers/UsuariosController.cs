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
    public class UsuariosController : Controller
    {
        private readonly DBContext _context;

        public UsuariosController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
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

        [HttpGet]
        public async Task<IActionResult> ModificarUsuario(int? id)
        {
            if (id == null || _context.Tabla_Usuarios == null)
            {
                return NotFound();
            }

            var variablesUsuarios = await _context.Tabla_Usuarios.FindAsync(id);
            if (variablesUsuarios == null)
            {
                return NotFound();
            }
            return View(variablesUsuarios);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModificarUsuario(int id, [Bind("ID,Nombre,Apellido,Teléfono,DUI")] VariablesUsuarios variablesUsuarios)
        {
            if (id != variablesUsuarios.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(variablesUsuarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VariablesUsuariosExists(variablesUsuarios.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(RegistroDeUsuarios));
            }
            return View(variablesUsuarios);
        }

        private bool VariablesUsuariosExists(int iD)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<IActionResult> EliminarUsuario(int? id)
        {
            if (id == null || _context.Tabla_Usuarios == null)
            {
                return NotFound();
            }

            var variablesUsuarios = await _context.Tabla_Usuarios
                .FirstOrDefaultAsync(m => m.ID == id);
            if (variablesUsuarios == null)
            {
                return NotFound();
            }

            return View(variablesUsuarios);
        }

        [HttpPost, ActionName("EliminarUsuario")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarUsuarioConfirmed(int id)
        {
            if (_context.Tabla_Usuarios == null)
            {
                return Problem("Entity set 'DBContext.Tabla_Usuarios'  is null.");
            }
            var variablesUsuarios = await _context.Tabla_Usuarios.FindAsync(id);
            if (variablesUsuarios != null)
            {
                _context.Tabla_Usuarios.Remove(variablesUsuarios);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(RegistroDeUsuarios));
        }
    }
}