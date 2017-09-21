using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using TurismoCR.Models;

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
		public ActionResult Auth(User user)
		{
			var client = new GraphClient(
                // cambiar password (adrian) por el de su base Neo4j
                new Uri("http://localhost:7474/db/data"), "neo4j", "adrian"
            );
			client.Connect();
            var userConsulted = client
                              .Cypher
                              .Match("(userNeo4j:User)")
                              .Where((User userNeo4j) => userNeo4j.UserName == user.UserName)
                              .Return(userNeo4j => userNeo4j.As<User>())
                              .Results;
            if (userConsulted.Any()) {
                var foundUser = userConsulted.First();
                if (foundUser.Password == user.Password) {
                    Response.Cookies.Append("openSession", 
                        foundUser.UserName,
                        new CookieOptions
                        {
                           Expires = DateTimeOffset.Now.AddHours(2)
                        }
                    );
                    
                    var myCookie = Request.Cookies["openSession"];
                    if (myCookie != null) {
                        System.Diagnostics.Debug.WriteLine("Test:");
                    }
				}
            }
            return RedirectToAction("Index", "Home");

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
