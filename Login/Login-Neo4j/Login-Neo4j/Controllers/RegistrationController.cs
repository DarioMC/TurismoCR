using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Login_Neo4j.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register(Login_Neo4j.Models.User Userlog)
        {
            using (var driver = GraphDatabase.Driver("bolt://127.0.0.1:7687", AuthTokens.Basic("neo4j", "chavacampos14")))
            using (var session = driver.Session())
            {
                //var result = session.Run("CREATE (n) RETURN n");
                var result = session.Run("create(x: Persona{" +
                    "Nombre: '" + Userlog.Nombre + "'," +
                    "Apellido:'" + Userlog.Apellidos + "'," +
                    "Correo: '" + Userlog.Correo + "'," +
                    "Usuario: '" + Userlog.UserName + "'," +
                    "Contrasena:'" + Userlog.Password + "'," +
                    "Telefono:'" + Userlog.Telefono + "'," +
                    "Genero:'" + Userlog.Genero + "'," +
                    "Cedula:'" + Userlog.Cedula + "'," +
                    "FechaN: '" + Userlog.FechaN + "'}) return x");

                    return View("Index");
            }
        }
    }
}