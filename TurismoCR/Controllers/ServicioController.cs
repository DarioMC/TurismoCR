using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;


namespace TurismoCR.Controllers
{
    public class ServicioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public void ConexionMongoDB()
        {
            
            /// Al crear la base mongo para esta conexión  nombrarla --> TurismoCR 
            var mc = new MongoClient(connectionString: "mongodb://localhost");
            var db = mc.GetDatabase("TurismoCR");

            ///MongoDatabase mongo = mc.GetDatabase("TurismoCR");
        }

    }
}