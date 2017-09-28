using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using TurismoCR.Models;
using static TurismoCR.Controllers.ImageController;

namespace TurismoCR.Controllers
{
    public class ServiceController : Controller
    {

		public ActionResult AddService() {
			ViewData["Message"] = "Página para agregar paquete turístico";
			return View();
		}

        [HttpPost]
        public async Task<ActionResult> AddServiceAsync(Service service) {
			// check if service is well defined
			var isServiceNullOrEmpty = service
				.GetType()
				.GetProperties()
				.Where(pi => pi.GetValue(service) is string)
				.Select(pi => (string)pi.GetValue(service))
				.Any(value => String.IsNullOrEmpty(value));
            if (isServiceNullOrEmpty == true) {
                // setting alert message
                TempData["msg"] = "<script>alert('Hay campos vacíos!');</script>";
            } else {
                try {
                    // setting MongoDB connection
                    var mongoClient = new MongoClient(connectionString: "mongodb://localhost");
                    var db = mongoClient.GetDatabase("TurismoCR");
                    // getting reference to services
                    var serviceCollection = db.GetCollection<Service>("Services");
                    // inserting service by reference
                    await serviceCollection.InsertOneAsync(service);
					// setting alert message
					TempData["msg"] = "<script>alert('Paquete agregado exitosamente.');</script>";
                } catch {
                    // setting alert message
                    TempData["msg"] = "<script>alert('No hay comunicación con MongoDB.');</script>";
                }
            }
			// let's go to home
			return RedirectToAction("Index", "Home");
        }

		public async Task<ActionResult> ShowServices() {
            try {
                // setting MongoDB connection
                var mongoClient = new MongoClient(connectionString: "mongodb://localhost");
                var db = mongoClient.GetDatabase("TurismoCR");
                // getting reference to services
                var collection = db.GetCollection<Service>("Services");
                var userCookie = Request.Cookies["userSession"];
                var ownerUsername = userCookie.ToString();
                var filter = Builders<Service>.Filter.Eq("ownerUsername", ownerUsername);
                var sort = Builders<Service>.Sort.Ascending("Category");
                // filter services for curret owner user
                var result = await collection.Find(filter).Sort(sort).ToListAsync();
                // saving services
                ViewBag.ownerServices = result;
            } catch {
                TempData["msg"] = "<script>alert('No hay comunicación con MongoDB.');</script>";
            }
            // show view
			return View();
		}

		public async Task<ActionResult> EditarServicioAsync(ObjectId servicioId)
		{
			var mongoClient = new MongoClient(connectionString: "mongodb://localhost");
			var db = mongoClient.GetDatabase("TurismoCR");
			var coleccion = db.GetCollection<Service>("Servicios");
			var filtro = Builders<Service>.Filter.Eq("_id", servicioId);
			var resultado = await coleccion.FindAsync(filtro);
			return View(resultado);
		}

		[HttpPost]
		public async Task<ActionResult> EditarServicioAsync(ObjectId IdServicio, Service cambiosServicio)
		{
			// TODO check if user has services
			var mongoClient = new MongoClient(connectionString: "mongodb://localhost");
			//var mongoServer = mongoClient.GetServer();
			var db = mongoClient.GetDatabase("TurismoCR");

			var coleccion = db.GetCollection<Service>("Servicios");
			var filtro = Builders<Service>.Filter.Eq("_id", IdServicio);
			var resultado = await coleccion.ReplaceOneAsync(filtro, cambiosServicio, new UpdateOptions { IsUpsert = true });

			//Cambiar redireccion a otra vista en vez de View.
			return View();
		}

        public async Task<ActionResult> AgregarImagenServicioAsync(HttpPostedFileBase theFile)
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

                var ServicioCollection = db.GetCollection<Imagen>("ImgServicio");
                await ServicioCollection.InsertOneAsync(thePicture);

                //Agregar redireccion a interfaz en vez de vista.
                return View();


            }
            /*else
                ViewBag.Message = "Debe subir una imagen";*/

            //Verifica redireccion.
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> BorrarServicioAsync(ObjectId idServicio)
        {
            var mongoClient = new MongoClient(connectionString: "mongodb://localhost");
            var db = mongoClient.GetDatabase("TurismoCR");

            var coleccion = db.GetCollection<Service>("Servicios");
            var filtro = Builders<Service>.Filter.Eq("_id", idServicio);

            var resultado = await coleccion.DeleteOneAsync(filtro);

            //Agregar redireccion a otra vista en vez de View.
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> BorrarImagenServicioAsync(ObjectId idImagen)
        {
            var mongoClient = new MongoClient(connectionString: "mongodb://localhost");
            var db = mongoClient.GetDatabase("TurismoCR");

            var coleccion = db.GetCollection<Imagen>("ImgServicio");
            var filtro = Builders<Imagen>.Filter.Eq("_id", idImagen);

            var resultado = await coleccion.DeleteOneAsync(filtro);

            //Agregar redireccion a otra vista en vez de View.
            return View();
        }

		public ActionResult BuscarServicio()
		{
			ViewData["Message"] = "Página para buscar servicios/paquetes turístico";
			return View();
		}

        [HttpPost]
        public async Task<ActionResult> BuscarServicios()
        {
            var mongoClient = new MongoClient(connectionString: "mongodb://localhost");
            var db = mongoClient.GetDatabase("TurismoCR");

            var coleccion = db.GetCollection<Service>("Servicios");
            var filtro = new BsonDocument();
            var sort = Builders<Service>.Sort.Ascending("Categoria");
            var resultado = await coleccion.Find(filtro).Sort(sort).ToListAsync();

            //Agregar redireccion a otra vista con la lista por parámetro.
            return View();
        }
    }
}
