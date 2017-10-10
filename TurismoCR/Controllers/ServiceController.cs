using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using TurismoCR.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using TurismoCR.Data;

namespace TurismoCR.Controllers
{
	public class ServiceController : Controller
	{

        private readonly TurismoCRContext _context;

        public ServiceController(TurismoCRContext context)
        {
            _context = context;
        }

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
            // check if parameters are null or empty
            if (String.IsNullOrEmpty(serviceName)
                || String.IsNullOrEmpty(serviceDescription)
                || String.IsNullOrEmpty(serviceCategory)
                || String.IsNullOrEmpty(serviceLatitude)
                || String.IsNullOrEmpty(serviceLongitude)
                || String.IsNullOrEmpty(selectServiceStartDate)
                || String.IsNullOrEmpty(selectServiceEndDate)
                || String.IsNullOrEmpty(servicePrice)) 
            {
				// setting alert message
				TempData["msg"] = "<script>alert('Hay campos vacíos!');</script>";
            }
            else 
            {
                // create connection with MongoDB
                try 
                {
					// setting MongoDB connection
					var mongoClient = new MongoClient("mongodb://localhost");
					var db = mongoClient.GetDatabase("TurismoCR");
                    // insert picture if exists
                    var pictureRandID = "";
					try
					{
						if (pictureFile.Length > 0)
						{
							using (var ms = new MemoryStream())
							{
								pictureFile.CopyTo(ms);
								var pictureBytes = ms.ToArray();
								var pictureStr = Convert.ToBase64String(pictureBytes);
								// act on the Base64 data
                                var pictures = db.GetCollection<PictureService>("Pictures");
                                // inserting picture service by reference
                                pictureRandID = Guid.NewGuid().ToString();
                                var pictureService = new PictureService(pictureStr, pictureRandID);
								await pictures.InsertOneAsync(pictureService);
							}
						}
					}
					catch
					{
						TempData["msg"] = "<script>alert('No se pudo cargar la imagen!');</script>";
					}
					// insert service
					var service = new Service(
                        Guid.NewGuid().ToString(),
                        Request.Cookies["userSession"],
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
					    pictureRandID
                    );
                    var services = db.GetCollection<Service>("Services");
                    await services.InsertOneAsync(service);
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
				// filter services for current owner user
				var result = await collection.Find(filter).ToListAsync();
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

		public async Task<ActionResult> EditService(String serviceID)
		{
			try
			{
				// setting MongoDB connection
				var mongoClient = new MongoClient("mongodb://localhost");
				var db = mongoClient.GetDatabase("TurismoCR");
				// getting reference to services
				var collection = db.GetCollection<Service>("Services");
				var filter = Builders<Service>.Filter.Eq("RandID", serviceID);
				// filter services for current owner user
				var result = await collection.Find(filter).ToListAsync();
				// if theres any result
				if (result.Count != 0)
				{
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
				TempData["msg"] = "<script>alert('No hay conexión con MongoDB.');</script>";
			}
			// let's go to main page
			return RedirectToAction("Index", "Home");
		}

		[HttpPost("EditServicePost")]
		public async Task<ActionResult> EditServicePost(
			String serviceID,
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
			String pictureID,
			IFormFile pictureFile)
		{
			// check if parameters are null or empty
			if (String.IsNullOrEmpty(serviceName)
				|| String.IsNullOrEmpty(serviceDescription)
				|| String.IsNullOrEmpty(serviceCategory)
				|| String.IsNullOrEmpty(serviceLatitude)
				|| String.IsNullOrEmpty(serviceLongitude)
				|| String.IsNullOrEmpty(selectServiceStartDate)
				|| String.IsNullOrEmpty(selectServiceEndDate)
				|| String.IsNullOrEmpty(servicePrice))
			{
				// setting alert message
				TempData["msg"] = "<script>alert('Hay campos vacíos!');</script>";
			}
			else
			{
				// create connection with MongoDB
				try
				{
					// setting MongoDB connection
					var mongoClient = new MongoClient("mongodb://localhost");
					var db = mongoClient.GetDatabase("TurismoCR");
					// insert picture if exists
					var pictureRandID = pictureID;
					try
					{
						if (pictureFile.Length > 0)
						{
							// delete old picture
							var picCollection = db.GetCollection<PictureService>("Pictures");
							var picFilter = Builders<PictureService>.Filter.Eq("RandID", pictureID);
							await picCollection.DeleteOneAsync(picFilter);
							// insert new picture
							using (var ms = new MemoryStream())
							{
								pictureFile.CopyTo(ms);
								var pictureBytes = ms.ToArray();
								var pictureStr = Convert.ToBase64String(pictureBytes);
								// act on the Base64 data
								var pictures = db.GetCollection<PictureService>("Pictures");
								// inserting picture service by reference
								pictureRandID = Guid.NewGuid().ToString();
								var pictureService = new PictureService(pictureStr, pictureRandID);
								await pictures.InsertOneAsync(pictureService);
							}
						}
					}
					catch
					{
						TempData["msg"] = "<script>alert('No se pudo cargar la imagen!');</script>";
					}
					// delete old service
					var servCollection = db.GetCollection<Service>("Services");
					var servFilter = Builders<Service>.Filter.Eq("RandID", serviceID);
					await servCollection.DeleteOneAsync(servFilter);
					// insert new service
					var service = new Service(
						Guid.NewGuid().ToString(),
						Request.Cookies["userSession"],
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
						pictureRandID
					);
					await servCollection.InsertOneAsync(service);
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

		public async Task<ActionResult> DeleteService(String serviceID)
        {
			try
			{
				// setting MongoDB connection
				var mongoClient = new MongoClient("mongodb://localhost");
				var db = mongoClient.GetDatabase("TurismoCR");
				// getting reference to services
				var servCollection = db.GetCollection<Service>("Services");
				var servFilter = Builders<Service>.Filter.Eq("RandID", serviceID);
				// filter services for specific service id
				var result = await servCollection.Find(servFilter).ToListAsync();
                // if theres any result
                if (result.Count != 0)
                {
                    // get service
                    var service = result.First();
                    // if service has picture
                    if (service.RandID != "")
                    {
                        // getting reference to pictures
                        var picCollection = db.GetCollection<PictureService>("Pictures");
                        var picFilter = Builders<PictureService>.Filter.Eq("RandID", service.PictureID);
                        // delete picture
                        await picCollection.DeleteOneAsync(picFilter);
                    }
					// delete service
					await servCollection.DeleteOneAsync(servFilter);
					// setting alert message
					TempData["msg"] = "<script>alert('El paquete ha sido borrado.');</script>";
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

		public ActionResult SearchServiceAux()
		{
			ViewData["Message"] = "Página para buscar paquetes turísticos";
			return View();
		}

        [HttpPost("SearchService")]
		public async Task<ActionResult> SearchService(String serviceCategory)
		{
			try
			{
				// setting MongoDB connection
				var mongoClient = new MongoClient("mongodb://localhost");
				var db = mongoClient.GetDatabase("TurismoCR");
				// getting reference to services
				var collection = db.GetCollection<Service>("Services");
			    var filterEnabled = Builders<Service>.Filter.Eq("Enabled", true);
                var filterCategory = Builders<Service>.Filter.Eq("Category", serviceCategory);
                var filter = filterEnabled & filterCategory;
				// filter services for current owner user
				var result = await collection.Find(filter).ToListAsync();
				if (result.Count == 0)
				{
					// setting alert message
					TempData["msg"] = "<script>alert('No hay paquetes registrados con sus preferencias!');</script>";
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
		}

        public List<int> CreateintDropdown()
        {
            var templist = new List<int>();
            for (int i=1; i < 21; i++)
            {
                templist.Add(i);
            }
            return templist; 
        }

        public async Task<ActionResult> ObtenerRecomendaciones()
        {
            // variables necesarias
            HashSet<String> ordenesRecientes = new HashSet<string>();
            List<List<Service>> recomendaciones = new List<List<Service>>();
            String usuarioId = Request.Cookies["userSession"].ToString();

            using (_context)
            {
                // busca las ultimas ordenes del usuario
                var ordenes = from p in _context.Ordenes
                               where p.Usuario.Equals(usuarioId)
                               orderby p.Fecha ascending
                               select p;

                // si existen ordenes obtiene recomendaciones
                if (ordenes != null)
                {
                    // obtiene las categorias de las ultimas ordenes
                    foreach (Orden orden in ordenes)
                    {
                        // agrega a la lista las categorias
                        ordenesRecientes.Add(orden.Categoria);
                    }

                    // consulta la lista final de categorias
                    foreach (String cat in ordenesRecientes)
                    {
                        // setting MongoDB connection
                        var mongoClient = new MongoClient("mongodb://localhost");
                        var db = mongoClient.GetDatabase("TurismoCR");
                        // getting reference to services
                        var collection = db.GetCollection<Service>("Services");
                        var filter = Builders<Service>.Filter.Eq("Category", cat);
                        // filter services for current owner user
                        var result = await collection.Find(filter).ToListAsync();
                        // guarda el resultado de recomendaciones en la lista
                        recomendaciones.Add(result);
                    }
                    // lista de recomendaciones encontradas
                    ViewBag.Recomendaciones = recomendaciones;

                    return View();
                }
                else
                {
                    // lista vacía de recomendaciones
                    ViewBag.Recomendaciones = recomendaciones;
                    return View();
                }
            }
        }
	}
}
