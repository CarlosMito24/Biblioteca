using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Biblioteca.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}