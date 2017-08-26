using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Neo4jClient;

namespace TurismoCR.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Ejemplo básico para conectarse con Neo4j.
        /// </summary>
        public void ConexionNeo4j()
        {
            var cliente = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "neo4j");
            cliente.Connect();
        }
    }
}