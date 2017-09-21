﻿using System;
using System.Linq;
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
                new Uri("http://localhost:7474/db/data"), "neo4j", "adrian"
            );
			client.Connect();
            var query = client.Cypher
                              .Match("(userNeo4j:User)")
                              .Where((User userNeo4j) => userNeo4j.UserName == user.UserName)
                              .Return(userNeo4j => userNeo4j.As<User>())
                              .Results;
			// here!
			foreach (object o in query)
			{
                System.Diagnostics.Debug.WriteLine("Test:" + o);
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
