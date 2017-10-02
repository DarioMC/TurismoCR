using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TurismoCR.Models;
using TurismoCR.Data;
using Microsoft.AspNetCore.Http;

namespace TurismoCR.Controllers
{
    public class ReseñaController : Controller
    {
        private readonly TurismoCRContext _context;

        public ReseñaController(TurismoCRContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

		public ActionResult InsertarReseña(int idOrden)
        {

			ViewData["Message"] = "Página para agregar reseña sobre servicio/paquete turístico";

            Response.Cookies.Append("idServicio",
                idOrden.ToString(),
                new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddSeconds(120)
                }
            );

            return View();
		}

        public ActionResult InsertarReseñas(Reseña reseña)
        {
            reseña.Fecha = DateTime.Now;
            reseña.IdServicio = Convert.ToInt32(Request.Cookies["idServicio"].ToString());
            reseña.Usuario = Request.Cookies["userSession"].ToString();


            try
            {
                //Agregar la reseña sin commit.
                _context.Add(reseña);

                //Realiza el commit del add.
                _context.SaveChanges();

                //Cambiar redireccion a otra vista en vez de a simple View().
                return RedirectToAction("Index", "Orden");
            }
            catch
            {

                //Cambiar redireccion a otra vista en vez de a simple View().
                return View();
            }

        }

		public ActionResult BuscarReseña() {
			ViewData["Message"] = "Página para buscar reseña sobre servicio/paquete turístico";
			return View();
		}

        public ActionResult MisReseñas()
        {
            String usuario = Request.Cookies["userSession"].ToString();

            try
            {

                //Usando el context de la BD, busca todos las reseñas asociadas a un Servicio
                using (_context)
                {
                    var reseñas = from p in _context.Reseñas
                                where p.Usuario.Equals(usuario)
                                select p;

                    List<Reseña> listaReseñas = new List<Reseña>();

                    //Agrega a una lista todas las reseñas encontradas.
                    if(reseñas != null)
                    { 
                        foreach(Reseña reseña in reseñas)
                        {
                            listaReseñas.Add(reseña);
                        }
                    }
                    //Cambiar redireccion a otra vista en vez de a simple View().
                    ViewBag.listaReseñas = listaReseñas;
                    return View();
                }
            }
            catch
            {
                //Cambiar redireccion a otra vista en vez de a simple View().
                return View();
            }
        }

        public ActionResult BorrarReseñas(int idReseña)
        {
            try
            {

                //Busca el objeto por el id específico y luego lo borra de la tabla.
                using (_context)
                {
                    var objeto = from p in _context.Reseñas
                                 where p.IdResenia.Equals(idReseña)
                                 select p;
                    
                    _context.Remove(objeto.First());

                    _context.SaveChanges();

                    //Cambiar redireccion a otra vista en vez de a simple View().
                    return RedirectToAction("MisReseñas", "Reseña");
                }

            }
            catch
            {

                //Cambiar redireccion a otra vista en vez de a simple View().
                return View();
            }
        }
    }
}