using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TurismoCR.Models;
using TurismoCR.Data;
using Microsoft.AspNetCore.Http;
using Neo4jClient;

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

        public ActionResult ReseñasAmigos()
        {
            try
            {
                //Conexion a Neo4j.
                var client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "adrian");
                client.Connect();

                //Usuario logueado actualmente.
                var username = Request.Cookies["userSession"];

                //Variables necesarias.
                IEnumerable<User> listaClientes = GetClientes();
                List<String> meSiguen = new List<string>();
                List<Reseña> reseniasAmigos = new List<Reseña>();


                foreach(User usuario in listaClientes)
                {
                    //Confirma si por cada usuario existe alguno que siga al logueado.
                    bool follow = (
                    client.Cypher
                          .Match("(a: User { UserName: '" + usuario.UserName + "'})-[:Sigue]->(b: User { UserName: '" + username + "'})")
                          .Return<Node<User>>("a")
                          .Results
                          .Count() == 1
                    );

                    //En caso de que lo siga, lo agrega a una lista.
                    if (follow == true)
                        meSiguen.Add(usuario.UserName);
                    
                }

                using (_context)
                {
                    try
                    {
                        //Por cada nombre en la lista obtiene reseñas propias de ese usuario.
                        foreach(String usrName in meSiguen)
                        {
                            //Busca las reseñas del usuario.
                            var resenias = from p in _context.Reseñas
                                           where p.Usuario.Equals(usrName)
                                           select p;

                            //En caso de haber alguna reseña, la agrega a la lista final.
                            if(resenias.Count() > 0)
                                foreach (Reseña tmpRes in resenias)
                                    reseniasAmigos.Add(tmpRes);

                        }
                    }
                    catch
                    {

                    }
                }

                //Guarda la lista en el viewbag.
                ViewBag.reseniasAmigos = reseniasAmigos;

                return View();
            }
            catch
            {
                return View();
            }
        }

        //Metodo para traerse los clientes del sistema.
        public IEnumerable<User> GetClientes()
            
        {
            var clientLogged = Request.Cookies["userSession"];
            // setting Neo4j connection
            var client = new GraphClient(
                // cambiar password (adrian) por el de su base Neo4j
                new Uri("http://localhost:7474/db/data"), "neo4j", "adrian"
            );
            client.Connect();
            // getting client users from Neo4j
            var clientUsers = client
                .Cypher
                .Match("(userNeo4j:User)")
                .Where((User userNeo4j) => (userNeo4j.Rol == "Client"))
                .AndWhere((User userNeo4j) => (userNeo4j.UserName != clientLogged))
                .Return(userNeo4j => userNeo4j.As<User>())
                .Results;
            return clientUsers;
        }
    }
}