using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TurismoCR.Data;
using TurismoCR.Models;

namespace TurismoCR.Controllers
{
    public class OrdenController : Controller
    {
        private readonly TurismoCRContext _context;

        public OrdenController(TurismoCRContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            Orden ejmOrden = new Orden("LuisDavid", DateTime.Now, "Viajes", "5x Dias en crucero por el caribe", 14500);
            _context.Add(ejmOrden);

            _context.SaveChanges();
            return View();
        }
    }
}