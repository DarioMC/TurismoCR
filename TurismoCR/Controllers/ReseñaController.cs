using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TurismoCR.Models;

namespace TurismoCR.Controllers
{
    public class ReseñaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}