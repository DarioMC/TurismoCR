using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using TurismoCR.Models;
using MongoDB.Driver;

namespace TurismoCR.Controllers
{
	public class UserController : Controller
	{
		public ActionResult Login()
		{
			ViewData["Message"] = "Página de inicio de sesión.";
			return View();
		}

		[HttpPost]
		public ActionResult Auth(User user)
		{
			// check if user is well defined
			var isUserNullOrEmpty = user
				.GetType()
				.GetProperties()
				.Where(pi => pi.GetValue(user) is string)
				.Select(pi => (string)pi.GetValue(user))
				.Any(String.IsNullOrEmpty);
			if (isUserNullOrEmpty == true)
			{
				// setting alert message
				TempData["msg"] = "<script>alert('Hay campos vacíos!');</script>";
			}
			else
			{
				try
				{
					// setting Neo4j connection
					var client = new GraphClient(
						// cambiar password (adrian) por el de su base Neo4j
						new Uri("http://localhost:7474/db/data"), "neo4j", "adrian"
					);
					client.Connect();
					// getting user from Neo4j
					var userConsulted = client
						.Cypher
						.Match("(userNeo4j:User)")
						.Where((User userNeo4j) => userNeo4j.UserName == user.UserName)
						.Return(userNeo4j => userNeo4j.As<User>())
						.Results;
					// if user exist in Neo4j
					if (userConsulted.Any())
					{
						var foundUser = userConsulted.First();
						// checking if user is enabled
						if (foundUser.Enabled == true)
						{
							// if password is valid
							if (foundUser.Password == user.Password)
							{
								// adding user session cookie
								Response.Cookies.Append("userSession",
									foundUser.UserName,
									new CookieOptions
									{
										Expires = DateTimeOffset.Now.AddHours(1)
									}
								);
								// adding rol session cookie
								Response.Cookies.Append("rolSession",
									foundUser.Rol,
									new CookieOptions
									{
										Expires = DateTimeOffset.Now.AddHours(1)
									}
								);
								// setting alert message
								TempData["msg"] = "<script>alert('Excelente, tu usuario ha sido identificado.');</script>";
							}
							else
							{
								// setting alert message
								TempData["msg"] = "<script>alert('Contraseña incorrecta.');</script>";
							}
						}
						else
						{
							TempData["msg"] = "<script>alert('Tu usuario ha sido desactivado.');</script>";
						}
					}
					else
					{
						// setting alert message
						TempData["msg"] = "<script>alert('No existe el usuario en el sistema.');</script>";
					}
				}
				catch
				{
					// setting alert message
					TempData["msg"] = "<script>alert('No hay comunicación con Neo4j.');</script>";
				}
			}
			// let's go to main page
			return RedirectToAction("Index", "Home");
		} // Auth

		public ActionResult LogOut()
		{
			if (Request.Cookies["userSession"] != null)
			{
				Response.Cookies.Delete("userSession");
			}
			if (Request.Cookies["rolSession"] != null)
			{
				Response.Cookies.Delete("rolSession");
			}
			return RedirectToAction("Index", "Home");
		}

		public ActionResult Register()
		{
			ViewData["Message"] = "Página de registro.";
			return View();
		}

		public ActionResult RegisterOwnerPlace()
		{
			ViewData["Message"] = "Página de registro.";
			return View();
		}

		public ActionResult RegisterAdmin()
		{
			ViewData["Message"] = "Página de registro.";
			return View();
		}

		[HttpPost]
		public ActionResult Reg(User user)
		{
			// check if user is well defined
			var isUserNullOrEmpty = user
				.GetType()
				.GetProperties()
				.Where(pi => pi.GetValue(user) is string)
				.Select(pi => (string)pi.GetValue(user))
				.Any(String.IsNullOrEmpty);
			if (isUserNullOrEmpty == true)
			{
				// setting alert message
				TempData["msg"] = "<script>alert('Hay campos vacíos!');</script>";
			}
			else
			{
				try
				{
					// setting Neo4j connection
					var client = new GraphClient(
						// cambiar password (adrian) por el de su base Neo4j
						new Uri("http://localhost:7474/db/data"), "neo4j", "adrian"
					);
					client.Connect();
					// getting user from Neo4j
					var userConsulted = client
						.Cypher
						.Match("(userNeo4j:User)")
						.Where((User userNeo4j) => userNeo4j.UserName == user.UserName)
						.Return(userNeo4j => userNeo4j.As<User>())
						.Results;
					// if user exist in Neo4j
					if (userConsulted.Any())
					{
						// setting alert message
						TempData["msg"] = "<script>alert('Este usuario ya está registrado en Neo4j.');</script>";
					}
					else
					{
						client.Cypher
							  .Create("(userNeo4j:User {user})")
							  .WithParam("user", user)
							  .ExecuteWithoutResults();
						// setting alert message
						TempData["msg"] = "<script>alert('Usuario exitosamente registrado.');</script>";
					}
				}
				catch
				{
					// setting alert message
					TempData["msg"] = "<script>alert('No hay comunicación con Neo4j.');</script>";
				}
			}
			// let's go to main page
			return RedirectToAction("Index", "Home");
		} // Reg

		public ActionResult DisableView()
		{
			// setting Neo4j connection
			var client = new GraphClient(
				// cambiar password (adrian) por el de su base Neo4j
				new Uri("http://localhost:7474/db/data"), "neo4j", "adrian"
			);
			client.Connect();
			// getting user from Neo4j
			var userConsulted = client
				.Cypher
				.Match("(userNeo4j:User)")
				.Where((User userNeo4j) => userNeo4j.Rol != "Administrator")
				.Return(userNeo4j => userNeo4j.As<User>())
				.Results;
			return View(userConsulted);
		}

		[HttpPost]
		public ActionResult Disable(User user)
		{
			// setting Neo4j connection
			var client = new GraphClient(
				// cambiar password (adrian) por el de su base Neo4j
				new Uri("http://localhost:7474/db/data"), "neo4j", "adrian"
			);
			client.Connect();
			// getting and disable user from Neo4j
			var userConsulted = client
				.Cypher
				.Match("(userNeo4j:User)")
				.Where((User userNeo4j) => userNeo4j.UserName == user.UserName)
				.Set("userNeo4j.Enabled={Enable}")
				.WithParams(new { Enable = "false" })
				.Return(userNeo4j => userNeo4j.As<User>())
				.Results;
			// setting alert message
			TempData["msg"] = "<script>alert('El usuario ha sido desabilitado.');</script>";
			return RedirectToAction("Index", "Home");
		}

        [HttpPost]
		public ActionResult Enable(User user)
		{
			// setting Neo4j connection
			var client = new GraphClient(
				// cambiar password (adrian) por el de su base Neo4j
				new Uri("http://localhost:7474/db/data"), "neo4j", "adrian"
			);
			client.Connect();
			// getting and enable user from Neo4j
			var userConsulted = client
				.Cypher
				.Match("(userNeo4j:User)")
				.Where((User userNeo4j) => userNeo4j.UserName == user.UserName)
				.Set("userNeo4j.Enabled={Enable}")
				.WithParams(new { Enable = "true" })
				.Return(userNeo4j => userNeo4j.As<User>())
				.Results;
			// setting alert message
			TempData["msg"] = "<script>alert('El usuario ha sido habilitado.');</script>";
			return RedirectToAction("Index", "Home");
		}

        public ActionResult FollowView()
        {
            var clientLogged = Request.Cookies["userSession"];
			ViewData["Message"] = "Página para seguir usuarios clientes.";
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
            return View(clientUsers);
        }

        public ActionResult Follow(String username)
		{
            var loggedUser = Request.Cookies["userSession"];
			var client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "adrian");
			client.Connect();
            client.Cypher.Match("(a:User)", "(b:User)")
                         .Where((User a) => a.UserName == loggedUser)
                         .AndWhere((User b) => b.UserName == username)
						 .CreateUnique("(a)- [:Sigue] -> (b)")
						 .ExecuteWithoutResults();
			// setting alert message
			TempData["msg"] = "<script>alert('Estas siguiendo a este usuario!');</script>";
            return RedirectToAction("Index", "Home");
		}

        public ActionResult UnFollow(String username)
        {
			var loggedUser = Request.Cookies["userSession"];
			var client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "adrian");
			client.Connect();
			client.Cypher.Match("(a)-[r]->(b)")
						 .Where((User a) => a.UserName == loggedUser)
						 .AndWhere((User b) => b.UserName == username)
						 .AndWhere("Type(r) = 'Sigue'")
						 .Delete("r").ExecuteWithoutResults();
			// setting alert message
			TempData["msg"] = "<script>alert('Ya no estas siguiendo a este usuario!');</script>";
			return RedirectToAction("Index", "Home");
        }
	}
}
