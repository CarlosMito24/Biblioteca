using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    public class LibrosController : Controller
    {
        public IActionResult RegistroDeLibros()
        {
            return View();
        }
    }
}
