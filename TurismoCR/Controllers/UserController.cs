﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using TurismoCR.Models;
using static TurismoCR.Controllers.ImageController;
using System.IO;
using MongoDB.Driver;
using System.Threading.Tasks;
using MongoDB.Bson;

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
                            Expires = DateTimeOffset.Now.AddHours(1)
                        }
                    );

                    // debug
                    var myCookie = Request.Cookies["openSession"];
                    if (myCookie != null) {
                        System.Diagnostics.Debug.WriteLine("Test");
                        System.Diagnostics.Debug.WriteLine(myCookie.ToString());
                    }
				}
            }
            return RedirectToAction("Index", "Home");

		}

		[HttpPost]
        public void LogOut(User user) {}

		[HttpPost]
		public void Reg()
		{
			var client = new GraphClient(
                new Uri("http://localhost:7474/db/data"), "neo4j", "adrian"
            );
			client.Connect();
		}

        //Método para inserta una imagen o logotipo del vendedor-servicio
        public async System.Threading.Tasks.Task<ActionResult> AgregarImagenLugarAsync(HttpPostedFileBase theFile)
        {

            if (theFile.ContentLength > 0)
            {
                string theFileName = Path.GetFileName(theFile.FileName);

                byte[] thePictureAsBytes = new byte[theFile.ContentLength];
                using (BinaryReader theReader = new BinaryReader(theFile.InputStream))
                {
                    thePictureAsBytes = theReader.ReadBytes(theFile.ContentLength);
                }

                string thePictureDataAsString = Convert.ToBase64String(thePictureAsBytes);

                Imagen thePicture = new Imagen()
                {
                    FileName = theFileName,
                    PictureDataAsString = thePictureDataAsString,
                    //codPro = lastId.Id
                };
                //thePicture._id = ObjectId.GenerateNewId();

                var mongoClient = new MongoClient(connectionString: "mongodb://localhost");
                var db = mongoClient.GetDatabase("TurismoCR");

                var ServicioCollection = db.GetCollection<Imagen>("ImgLugar");
                await ServicioCollection.InsertOneAsync(thePicture);

                //Agregar redireccion a interfaz en vez de vista.
                return View();


            }
            /*else
                ViewBag.Message = "Debe subir una imagen";*/

            //Verifica redireccion.
            return View();
        }

        public async Task<ActionResult> BorrarImagenServicioAsync(ObjectId idImagen)
        {
            var mongoClient = new MongoClient(connectionString: "mongodb://localhost");
            var db = mongoClient.GetDatabase("TurismoCR");

            var coleccion = db.GetCollection<Imagen>("ImgLugar");
            var filtro = Builders<Imagen>.Filter.Eq("_id", idImagen);

            var resultado = await coleccion.DeleteOneAsync(filtro);

            //Agregar redireccion a otra vista en vez de View.
            return View();
        }
    }
}
