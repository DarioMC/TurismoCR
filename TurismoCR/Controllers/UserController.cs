using System;
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

        [HttpPost]
		public void Auth()
		{
			var client = new GraphClient(
                new Uri("http://localhost:7474/db/data"), "neo4j", "adrian"
            );
			client.Connect();
		}

		[HttpPost]
		public void Reg()
		{
			var client = new GraphClient(
                new Uri("http://localhost:7474/db/data"), "neo4j", "adrian"
            );
			client.Connect();
		}

    }
}
