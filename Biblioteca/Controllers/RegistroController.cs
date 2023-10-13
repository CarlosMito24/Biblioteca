using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    public class RegistroController : Controller
    {
        public IActionResult RegistroDePrestamos()
        {
            return View();
        }
    }
}
