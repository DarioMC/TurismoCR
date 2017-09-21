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
            var mongoClient = new MongoClient(connectionString: "mongodb://localhost");
            ///var mongoServer = mongoClient.GetServer();
            var db = mongoClient.GetDatabase("TurismoCR");

        }

        public ActionResult InsertarServicio(Models.Servicio servicio)
        {
            ///Start startInsert = new Start("prueba de insert desde csharp", "ejecutar insert", 2);
            ///MongoCollection mgCollection = db.GetCollection<Start>("start");
            ///mgCollection.Insert<Start>(startInsert);
            return View();
        }

    }
}