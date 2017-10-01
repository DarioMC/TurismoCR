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
			return RedirectToAction("AddImageService", "Service");
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
                // filter services for current owner user
                var result = await collection.Find(filter).Sort(sort).ToListAsync();
                if (result.Count == 0) {
					// setting alert message
					TempData["msg"] = "<script>alert('No hay paquetes registrados!');</script>";
                } else {
					// saving services
					ViewBag.ownerServices = result;
					// show view
                    ViewData["Message"] = "Página para editar o borrar paquetes turísticos";
					return View();
                }
            } catch {
				// setting alert message
				TempData["msg"] = "<script>alert('No hay comunicación con MongoDB.');</script>";
            }
			// let's go to main page
			return RedirectToAction("Index", "Home");

		}

		public ActionResult EditService(Service service) {
			// saving service id to edit
			Response.Cookies.Append("serviceIDToEdit",
                service.BackupID,
				new CookieOptions
				{
					Expires = DateTimeOffset.Now.AddMinutes(20)
				}
			);
			ViewData["Message"] = "Página para editar paquete turístico";
			return View(service);
		}

		[HttpPost]
		public async Task<ActionResult> EditServiceAsync(Service serviceChanged) {
			// check if service is well defined
			var isServiceNullOrEmpty = serviceChanged
				.GetType()
				.GetProperties()
				.Where(pi => pi.GetValue(serviceChanged) is string)
				.Select(pi => (string)pi.GetValue(serviceChanged))
				.Any(value => String.IsNullOrEmpty(value));
            if (isServiceNullOrEmpty == true) {
				// setting alert message
				TempData["msg"] = "<script>alert('Hay campos vacíos!');</script>";
            } else {
				try {
					// getting service id to edit 
					ObjectId serviceID = ObjectId.Parse(Request.Cookies["serviceIDToEdit"]);
					// setting MongoDB connection
					var mongoClient = new MongoClient(connectionString: "mongodb://localhost");
					var db = mongoClient.GetDatabase("TurismoCR");
					// getting reference to services
					var collection = db.GetCollection<Service>("Services");
					// setting service id filter
					var filter = Builders<Service>.Filter.Eq("_id", serviceID);
					// deleting old service by reference with filter
					await collection.DeleteOneAsync(filter);
					// inserting service edited by reference
					await collection.InsertOneAsync(serviceChanged);
					// deleting cookie with service id
					Response.Cookies.Delete("serviceIDToEdit");
					// setting alert message
					TempData["msg"] = "<script>alert('Paquete editado exitosamente!');</script>";
				} catch {
					// setting alert message
					TempData["msg"] = "<script>alert('No hay comunicación con MongoDB.');</script>";
				}    
            }
			// let's go to main page
			return RedirectToAction("Index", "Home");
		}

        public ActionResult AddImageService()
        {
            ViewData["Message"] = "Página para agregar paquete turístico";
            return View();
        }

        public async Task<ActionResult> AddImageServiceAsync(Imagenes TheFile) {
            List<HttpPostedFileBase> listImages = new List<HttpPostedFileBase>();

            listImages.Add(TheFile.img1);
            listImages.Add(TheFile.img2);
            listImages.Add(TheFile.img3);
            listImages.Add(TheFile.img4);
            listImages.Add(TheFile.img5);

            foreach (HttpPostedFileBase theFile in listImages) { 
                if (theFile.ContentLength > 0) {
                    string theFileName = Path.GetFileName(theFile.FileName);

                    byte[] thePictureAsBytes = new byte[theFile.ContentLength];
                    using (BinaryReader theReader = new BinaryReader(theFile.InputStream))
                    {
                        thePictureAsBytes = theReader.ReadBytes(theFile.ContentLength);
                    }

                    string thePictureDataAsString = Convert.ToBase64String(thePictureAsBytes);

                    var mongoClient = new MongoClient(connectionString: "mongodb://localhost");
                    var db = mongoClient.GetDatabase("TurismoCR");
                    var collection = db.GetCollection<Service>("Services");
                    var userCookie = Request.Cookies["userSession"];
                    var ownerUsername = userCookie.ToString();
                    var filter = Builders<Service>.Filter.Eq("OwnerUsername", ownerUsername);
                    //var sort = Builders<Service>.Sort.
                    // filter services for current owner user
                    var result = await collection.Find(filter).ToListAsync();

                    Imagen thePicture = new Imagen()
                    {
                        FileName = theFileName,
                        PictureDataAsString = thePictureDataAsString,
                        idServicio = result.Last().BackupID
                    };
                    //thePicture._id = ObjectId.GenerateNewId();

                    var serviceCollection = db.GetCollection<Imagen>("ImgService");
                    await serviceCollection.InsertOneAsync(thePicture);
                }

            }

            return RedirectToAction("Index", "Home");

        }

        public async Task<ActionResult> DeleteServiceAsync(Service service) {
            try {
                ObjectId serviceID = ObjectId.Parse(service.BackupID);
                var mongoClient = new MongoClient(connectionString: "mongodb://localhost");
                var db = mongoClient.GetDatabase("TurismoCR");
                var collection = db.GetCollection<Service>("Services");
                var filter = Builders<Service>.Filter.Eq("_id", serviceID);
                await collection.DeleteOneAsync(filter);
				// setting alert message
				TempData["msg"] = "<script>alert('Servicio borrado con éxito!');</script>";
            } catch {
				// setting alert message
				TempData["msg"] = "<script>alert('No hay comunicación con MongoDB.');</script>";    
            }
			// let's go to main page
			return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteImageServiceAsync(ObjectId idImagen) {
            var mongoClient = new MongoClient(connectionString: "mongodb://localhost");
            var db = mongoClient.GetDatabase("TurismoCR");
            var collection = db.GetCollection<Imagen>("ImgServicio");
            var filter = Builders<Imagen>.Filter.Eq("_id", idImagen);
            await collection.DeleteOneAsync(filter);
            return View();
        }

        public async Task<ActionResult> SearchService() {
            try {
				// setting MongoDB connection
				var mongoClient = new MongoClient(connectionString: "mongodb://localhost");
				var db = mongoClient.GetDatabase("TurismoCR");
				// getting reference to services
				var collection = db.GetCollection<Service>("Services");
                var filter = Builders<Service>.Filter.Eq("Enabled", true);
				var sort = Builders<Service>.Sort.Ascending("Category");
				// filter services for current owner user
				var result = await collection.Find(filter).Sort(sort).ToListAsync();
				if (result.Count == 0) {
					// setting alert message
					TempData["msg"] = "<script>alert('No hay paquetes registrados!');</script>";
				}
				else {
					// saving services
					ViewBag.enabledServices = result;
					// show view
					ViewData["Message"] = "Página para ver paquetes turístico en oferta.";
					return View();
				}
            } catch {
				// setting alert message
				TempData["msg"] = "<script>alert('No hay comunicación con MongoDB.');</script>"; 
            }
			// let's go to main page
			return RedirectToAction("Index", "Home");
		}
    }
}
