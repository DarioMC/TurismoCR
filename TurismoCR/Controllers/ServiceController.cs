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
                var filter = Builders<Service>.Filter.Eq("OwnerUsername", ownerUsername);
                var sort = Builders<Service>.Sort.Ascending("Category");
                // filter services for curret owner user
                var result = await collection.Find(filter).Sort(sort).ToListAsync();
                if (result.Count == 0) {
                    TempData["msg"] = "<script>alert('No hay paquetes registrados!');</script>";
                } else {
					// saving services
					ViewBag.ownerServices = result;
					// show view
                    ViewData["Message"] = "Página para editar o borrar paquetes turísticos";
					return View();
                }

            } catch {
                TempData["msg"] = "<script>alert('No hay comunicación con MongoDB.');</script>";
            }
			// let's go to main page
			return RedirectToAction("Index", "Home");

		}

		public ActionResult EditService(Service service) {
			ViewData["Message"] = "Página para editar paquete turístico";
			return View(service);
		}

		[HttpPost]
		public async Task<ActionResult> EditServiceAsync(Service serviceChanged)
		{
            // TODO Cambiar id y obtener
            ObjectId IdService = ObjectId.Parse("59cd7df5a9da8b6574d01e52");
			var mongoClient = new MongoClient(connectionString: "mongodb://localhost");
			var db = mongoClient.GetDatabase("TurismoCR");
			var collection = db.GetCollection<Service>("Services");
			var filter = Builders<Service>.Filter.Eq("_id", IdService);
            await collection.DeleteOneAsync(filter);
            // inserting service by reference
            await collection.InsertOneAsync(serviceChanged);
			// let's go to main page
			return RedirectToAction("Index", "Home");
		}

        public async Task<ActionResult> AddImageServiceAsync(HttpPostedFileBase theFile)
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
        public async Task<ActionResult> BorrarServicioAsync(ObjectId idServ)
        {
            var mongoClient = new MongoClient(connectionString: "mongodb://localhost");
            var db = mongoClient.GetDatabase("TurismoCR");

            var collection = db.GetCollection<Service>("Services");
            var filter = Builders<Service>.Filter.Eq("_id", idServ);
            var result = await collection.DeleteOneAsync(filter);

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
