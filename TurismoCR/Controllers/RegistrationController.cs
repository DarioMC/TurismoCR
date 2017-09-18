using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Neo4j.Driver.V1;
using System.Web;

namespace Login_Neo4j.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register(TurismoCR.Models.User Userlog)
        {
            using (var driver = GraphDatabase.Driver("bolt://127.0.0.1:7687", AuthTokens.Basic("neo4j", "chavacampos14")))
            using (var session = driver.Session())
            {
                //var result = session.Run("CREATE (n) RETURN n");
                var result = session.Run("create(x: Persona{" +
                    "Nombre: '" + Userlog.Name + "'," +
                    "Apellido:'" + Userlog.LastName1 + "'," +
                    "Correo: '" + Userlog.Email + "'," +
                    "Usuario: '" + Userlog.UserName + "'," +
                    "Contrasena:'" + Userlog.Password + "'," +
                    "Telefono:'" + Userlog.PhoneNumber + "'," +
                    "Genero:'" + Userlog.Genre + "'," +
                    "Cedula:'" + Userlog.ID + "'," +
                    "FechaN: '" + Userlog.BirthDate + "'}) return x");

                return View("Index");
            }
        }
    }
}