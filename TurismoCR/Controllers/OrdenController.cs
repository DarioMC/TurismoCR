using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TurismoCR.Data;
using TurismoCR.Models;
using System.Collections.Generic;

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
            
            
            return View();
        }

		public ActionResult VerOrdenesCompra() {
			ViewData["Message"] = "Página para ver órdenes de compra actual.";
			return View();
		}

        public ActionResult InsertaOrdenCompra(Orden nuevaOrden)
        {
            try
            {
                //Agrega la orden nueva sin hacer commit.
                _context.Add(nuevaOrden);

                //Realiza el commit.
                _context.SaveChanges();

                //Agregar redireccion de interfaz en vez de View.
                return View();
            }
            catch(Exception)
            {
                //Agregar redireccion de interfaz en vez de View.
                return View();
            }
        }

		public ActionResult VerOrdenes() {
			ViewData["Message"] = "Página para ver órdenes de compras realizadas.";
			return View();
		}

        public ActionResult BuscarOrdenesCompra(String usuarioId)
        {
            try
            {
                using (_context)
                {
                    //Busca todas las ordenes de compra de un usuario.
                    var ordenes = from p in _context.Ordenes
                                  where p.Usuario.Equals(usuarioId)
                                  select p;

                    List<Orden> ordenesCompra = new List<Orden>();

                    if(ordenes != null)
                    { 
                        foreach(Orden orden in ordenes)
                        {
                            //Agrega todos los resultados a una lista.
                            ordenesCompra.Add(orden);
                        }
                    }
                    return View(ordenesCompra);

                }
            }
            catch(Exception)
            {
                //Agregar redireccion de interfaz en vez de View.
                return View();
            }
        }
    }
}