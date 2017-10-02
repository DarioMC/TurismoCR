using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using TurismoCR.Models;

namespace TurismoCR.Controllers
{
	public class ServiceController : Controller
	{

		public ActionResult AddService()
		{
			ViewData["Message"] = "Página para agregar paquete turístico";
			return View();
		}

		[HttpPost("AddServicePost")]
		public ActionResult AddServicePost(String serviceName,
										   String serviceDescription,
										   String serviceCategory,
										   String serviceProvince,
										   String serviceCanton,
										   String serviceDistrict,
										   String serviceLatitude,
										   String serviceLongitude,
										   String selectServiceStartDate,
										   String selectServiceEndDate,
										   String servicePrice,
										   Boolean serviceEnabled,
										   IFormFile pictureFile)
		{
			// create service
			Service service = new Service(Request.Cookies["userSession"],
										  serviceName,
										  serviceDescription,
										  serviceCategory,
										  serviceProvince,
										  serviceCanton,
										  serviceDistrict,
										  serviceLatitude,
										  serviceLongitude,
										  selectServiceStartDate,
										  selectServiceEndDate,
										  servicePrice,
										  serviceEnabled,
										  "picture");
			// save image on service
			try
			{
				if (pictureFile.Length > 0)
				{
					using (var ms = new MemoryStream())
					{
						pictureFile.CopyTo(ms);
						var fileBytes = ms.ToArray();
						string s = Convert.ToBase64String(fileBytes);
						// act on the Base64 data
						service.Picture = s;
					}
				}
			}
			catch
			{
				TempData["msg"] = "<script>alert('No se pudo cargar la imagen!');</script>";
			}
			// check if service is well defined
			var isServiceNullOrEmpty = service
				.GetType()
				.GetProperties()
				.Where(pi => pi.GetValue(service) is string)
				.Select(pi => (string)pi.GetValue(service))
				.Any(String.IsNullOrEmpty);
			if (isServiceNullOrEmpty == true)
			{
				// setting alert message
				TempData["msg"] = "<script>alert('Hay campos vacíos!');</script>";
			}
			else
			{
				try
				{
					// setting MongoDB connection
					var mongoClient = new MongoClient("mongodb://localhost");
					var db = mongoClient.GetDatabase("TurismoCR");
					// getting reference to services
					var serviceCollection = db.GetCollection<Service>("Services");
					// inserting service by reference
					serviceCollection.InsertOne(service);
					// setting alert message
					TempData["msg"] = "<script>alert('Paquete agregado exitosamente.');</script>";
				}
				catch
				{
					// setting alert message
					TempData["msg"] = "<script>alert('No hay comunicación con MongoDB.');</script>";
				}
			}
			// let's go to main page
			return RedirectToAction("Index", "Home");
		}

		public async Task<ActionResult> ShowServices()
		{
			try
			{
				// setting MongoDB connection
				var mongoClient = new MongoClient("mongodb://localhost");
				var db = mongoClient.GetDatabase("TurismoCR");
				// getting reference to services
				var collection = db.GetCollection<Service>("Services");
				var ownerUsername = Request.Cookies["userSession"];
				var filter = Builders<Service>.Filter.Eq("OwnerUsername", ownerUsername);
				var sort = Builders<Service>.Sort.Ascending("Category");
				// filter services for current owner user
				var result = await collection.Find(filter).Sort(sort).ToListAsync();
				if (result.Count == 0)
				{
					// setting alert message
					TempData["msg"] = "<script>alert('No hay paquetes registrados!');</script>";
				}
				else
				{
					// send services to view
					ViewBag.ownerServices = result;
					// show view
					ViewData["Message"] = "Página para editar o borrar paquetes turísticos";
					return View();
				}
			}
			catch
			{
				// setting alert message
				TempData["msg"] = "<script>alert('No hay comunicación con MongoDB.');</script>";
			}
			// let's go to main page
			return RedirectToAction("Index", "Home");

		}

		public ActionResult EditService()
		{
			ViewData["Message"] = "Página para editar paquete turístico";
            return View();
		}

        [HttpPost("EditValidService")]
		public async Task<ActionResult> EditValidService(String serviceID)
		{
            try
            {
                // setting MongoDB connection
                var mongoClient = new MongoClient("mongodb://localhost");
                var db = mongoClient.GetDatabase("TurismoCR");
                // getting reference to services
                var collection = db.GetCollection<Service>("Services");
                var filter = Builders<Service>.Filter.Eq("_id", serviceID);
                var sort = Builders<Service>.Sort.Ascending("Category");
                // filter services for current owner user
                var result = await collection.Find(filter).Sort(sort).ToListAsync();
                if (result.Count != 0)
                {
                    // TODO Save serviceID in Cookie
                    //      or Sent service to view
                    // show view
                    ViewData["Message"] = "Página para editar paquete turístico";
					return View();
                }
            }
            catch 
            {
				// setting alert message
				TempData["msg"] = "<script>alert('No hay conexión con MongoDB o el paquete no existe.');</script>";
            }
			// let's go to main page
			return RedirectToAction("Index", "Home");
		}

		/*[HttpPost]
		public async Task<ActionResult> EditServicePost(Service serviceChanged)
		{
			// check if service is well defined
			var isServiceNullOrEmpty = serviceChanged
				.GetType()
				.GetProperties()
				.Where(pi => pi.GetValue(serviceChanged) is string)
				.Select(pi => (string)pi.GetValue(serviceChanged))
				.Any(value => String.IsNullOrEmpty(value));
			if (isServiceNullOrEmpty == true)
			{
				// setting alert message
				TempData["msg"] = "<script>alert('Hay campos vacíos!');</script>";
			}
			else
			{
				try
				{
					// getting service id to edit 
					ObjectId serviceID = ObjectId.Parse(Request.Cookies["serviceIDToEdit"]);
					// setting MongoDB connection
					var mongoClient = new MongoClient("mongodb://localhost");
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
				}
				catch
				{
					// setting alert message
					TempData["msg"] = "<script>alert('No hay comunicación con MongoDB.');</script>";
				}
			}
			// let's go to main page
			return RedirectToAction("Index", "Home");
		}

		public async Task<ActionResult> DeleteServiceAsync(Service service)
		{
			try
			{
				ObjectId serviceID = ObjectId.Parse(service.BackupID);
				var mongoClient = new MongoClient("mongodb://localhost");
				var db = mongoClient.GetDatabase("TurismoCR");
				var collection = db.GetCollection<Service>("Services");
				var filter = Builders<Service>.Filter.Eq("_id", serviceID);
				await collection.DeleteOneAsync(filter);
				// setting alert message
				TempData["msg"] = "<script>alert('Servicio borrado con éxito!');</script>";
			}
			catch
			{
				// setting alert message
				TempData["msg"] = "<script>alert('No hay comunicación con MongoDB.');</script>";
			}
			// let's go to main page
			return RedirectToAction("Index", "Home");
		}

		public async Task<ActionResult> SearchService()
		{
			try
			{
				// setting MongoDB connection
				var mongoClient = new MongoClient("mongodb://localhost");
				var db = mongoClient.GetDatabase("TurismoCR");
				// getting reference to services
				var collection = db.GetCollection<Service>("Services");
				var filter = Builders<Service>.Filter.Eq("Enabled", true);
				var sort = Builders<Service>.Sort.Ascending("Category");
				// filter services for current owner user
				var result = await collection.Find(filter).Sort(sort).ToListAsync();
				if (result.Count == 0)
				{
					// setting alert message
					TempData["msg"] = "<script>alert('No hay paquetes registrados!');</script>";
				}
				else
				{
					// saving services
					ViewBag.enabledServices = result;
					// show view
					ViewData["Message"] = "Página para ver paquetes turístico en oferta.";
					return View();
				}
			}
			catch
			{
				// setting alert message
				TempData["msg"] = "<script>alert('No hay comunicación con MongoDB.');</script>";
			}
			// let's go to main page
			return RedirectToAction("Index", "Home");
		}*/
	}
}
