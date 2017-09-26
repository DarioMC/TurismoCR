using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TurismoCR.Models;
using TurismoCR.Data;

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

        public ActionResult InsertarReseña(Reseña reseña)
        {

            try
            {
                //Agregar la reseña sin commit.
                _context.Add(reseña);

                //Realiza el commit del add.
                _context.SaveChanges();

                //Cambiar redireccion a otra vista en vez de a simple View().
                return View();
            }
            catch(Exception ex)
            {

                //Cambiar redireccion a otra vista en vez de a simple View().
                return View();
            }

        }

        public ActionResult BuscarReseñas(String servicioId)
        {
            try
            {

                //Usando el context de la BD, busca todos las reseñas asociadas a un Servicio
                using (_context)
                {
                    var reseñas = from p in _context.Reseñas
                                where p.IdResenia.Equals(servicioId)
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
                    return View(listaReseñas);
                }
            }
            catch (Exception ex)
            {
                //Cambiar redireccion a otra vista en vez de a simple View().
                return View();
            }
        }

        public ActionResult BorrarReseña(int idReseña)
        {
            try
            {

                //Busca el objeto por el id específico y luego lo borra de la tabla.
                using (_context)
                {
                    var objeto = from p in _context.Reseñas
                                 where p.IdResenia.Equals(idReseña)
                                 select p;

                    _context.Remove(objeto);

                    _context.SaveChanges();

                    //Cambiar redireccion a otra vista en vez de a simple View().
                    return View();
                }

            }
            catch (Exception ex)
            {

                //Cambiar redireccion a otra vista en vez de a simple View().
                return View();
            }
        }
    }
}