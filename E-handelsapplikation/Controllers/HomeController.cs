using E_handelsapplikation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_handelsapplikation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
