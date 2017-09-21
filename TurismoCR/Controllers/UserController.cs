using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Neo4jClient;

namespace TurismoCR.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Login()
		{
            ViewData["Message"] = "Página de inicio de sesión.";
			return View();
		}

        public ActionResult Register()
        {
            ViewData["Message"] = "Página de registro.";
            return View();
        }

		public void ConexionNeo4j()
		{
			var cliente = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "adrian");
			cliente.Connect();
		}
    }
}
