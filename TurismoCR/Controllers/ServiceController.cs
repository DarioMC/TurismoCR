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
		public async Task<ActionResult> AddServicePost(
                                           String serviceName,
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
					await serviceCollection.InsertOneAsync(service);
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
                // filter services for current owner user
                var result = await collection.Find(filter).ToListAsync();
                // if theres any result
                if (result.Count != 0)
                {
					// save service id in cookie
					Response.Cookies.Append("serviceIDToEdit",
						serviceID,
						new CookieOptions
						{
                        Expires = DateTimeOffset.Now.AddMinutes(15)
						}
					);
                    // send service to view
                    ViewBag.serviceToEdit = result.First();
                    // show view
                    ViewData["Message"] = "Página para editar paquete turístico.";
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

		[HttpPost("EditServicePost")]
		public async Task<ActionResult> EditServicePost(
										   String serviceName,
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
            // edit service
		    try
			{
				// setting MongoDB connection
				var mongoClient = new MongoClient("mongodb://localhost");
				var db = mongoClient.GetDatabase("TurismoCR");
				// getting reference to services
				var serviceCollection = db.GetCollection<Service>("Services");
                // creating filter to search specific service id
                String serviceID = "";
                if (Request.Cookies["serviceIDToEdit"] != null){
                    serviceID = Request.Cookies["serviceIDToEdit"];
                }
                var filter = Builders<Service>.Filter.Eq("_id", serviceID);
				// deleting service by reference
                await serviceCollection.DeleteOneAsync(filter);
				// inserting service by reference
				await serviceCollection.InsertOneAsync(service);
                // delete service id cookie
                Response.Cookies.Delete("serviceIDToEdit");
				// setting alert message
				TempData["msg"] = "<script>alert('Paquete editado exitosamente.');</script>";
			}
			catch
			{
				// setting alert message
				TempData["msg"] = "<script>alert('No hay comunicación con MongoDB.');</script>";
			}
			// let's go to main page
			return RedirectToAction("Index", "Home");
		}

		public ActionResult DeleteService()
		{
			ViewData["Message"] = "Página para borrar paquete turístico";
			return View();
		}

		[HttpPost("DeleteServicePost")]
        public async Task<ActionResult> DeleteServicePost(String serviceID)
        {
			try
			{
				// setting MongoDB connection
				var mongoClient = new MongoClient("mongodb://localhost");
				var db = mongoClient.GetDatabase("TurismoCR");
				// getting reference to services
				var collection = db.GetCollection<Service>("Services");
				var filter = Builders<Service>.Filter.Eq("_id", serviceID);
				// filter services for specific service id
				var result = await collection.Find(filter).ToListAsync();
                // if theres any result
                if (result.Count != 0)
                {
                    // delete service
                    await collection.DeleteOneAsync(filter);
                    // setting alert message
                    TempData["msg"] = "<script>alert('El paquete ha sido borrado');</script>";
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

		/*
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
