using System;
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
        public ActionResult Login() {
            ViewData["Message"] = "Página de inicio de sesión.";
			return View();
		}

        [HttpPost]
		public ActionResult Auth(User user) {
			// check if user is well defined
			var isUserNullOrEmpty = user
                .GetType()
                .GetProperties()
	            .Where(pi => pi.GetValue(user) is string)
	            .Select(pi => (string)pi.GetValue(user))
	            .Any(value => String.IsNullOrEmpty(value));
            if (isUserNullOrEmpty == true) {
				// setting alert message
				TempData["msg"] = "<script>alert('Hay campos vacíos!');</script>";
            } else {
				try {
					// setting Neo4j connection
					var client = new GraphClient(
						// cambiar password (adrian) por el de su base Neo4j
						new Uri("http://localhost:7474/db/data"), "neo4j", "daniel"
					);
					client.Connect();
					// getting user from Neo4j
					var userConsulted = client
						.Cypher
						.Match("(userNeo4j:User)")
						.Where((User userNeo4j) => userNeo4j.UserName == user.UserName)
						.Return(userNeo4j => userNeo4j.As<User>())
						.Results;
					// if user exist in Neo4j
					if (userConsulted.Any()) {
						var foundUser = userConsulted.First();
						// checking if user is enabled
						if (foundUser.Enabled == true) {
							// if password is valid
							if (foundUser.Password == user.Password) {
								// adding user session cookie
								Response.Cookies.Append("userSession",
									foundUser.UserName,
									new CookieOptions
									{
										Expires = DateTimeOffset.Now.AddHours(1)
									}
								);
								// adding rol session cookie
								Response.Cookies.Append("rolSession",
									foundUser.Rol,
									new CookieOptions
									{
										Expires = DateTimeOffset.Now.AddHours(1)
									}
								);
								// setting alert message
								TempData["msg"] = "<script>alert('Excelente, tu usuario ha sido identificado.');</script>";
							} else {
								// setting alert message
								TempData["msg"] = "<script>alert('Contraseña incorrecta.');</script>";
							}
						} else {
							TempData["msg"] = "<script>alert('Tu usuario ha sido desactivado.');</script>";
						}
					} else {
						// setting alert message
						TempData["msg"] = "<script>alert('No existe el usuario en el sistema.');</script>";
					}
				} catch {
					// setting alert message
					TempData["msg"] = "<script>alert('No hay comunicación con Neo4j.');</script>";
				}    
            }
            // let's go to main page
            return RedirectToAction("Index", "Home");
		} // Auth

		public ActionResult LogOut() {
            if (Request.Cookies["userSession"] != null) {
                Response.Cookies.Delete("userSession");
			}
			if (Request.Cookies["rolSession"] != null) {
				Response.Cookies.Delete("rolSession");
			}
            return RedirectToAction("Index", "Home");
        }

		public ActionResult Register() {
			ViewData["Message"] = "Página de registro.";
			return View();
		}

		public ActionResult RegisterOwnerPlace() {
			ViewData["Message"] = "Página de registro.";
			return View();
		}

		[HttpPost]
		public ActionResult Reg(User user) {
			// check if user is well defined
			var isUserNullOrEmpty = user
				.GetType()
				.GetProperties()
				.Where(pi => pi.GetValue(user) is string)
				.Select(pi => (string)pi.GetValue(user))
				.Any(value => String.IsNullOrEmpty(value));
			if (isUserNullOrEmpty == true) {
				// setting alert message
				TempData["msg"] = "<script>alert('Hay campos vacíos!');</script>";
            } else {
				try {
					// setting Neo4j connection
					var client = new GraphClient(
						// cambiar password (adrian) por el de su base Neo4j
						new Uri("http://localhost:7474/db/data"), "neo4j", "adrian"
					);
					client.Connect();
					// getting user from Neo4j
					var userConsulted = client
						.Cypher
						.Match("(userNeo4j:User)")
						.Where((User userNeo4j) => userNeo4j.UserName == user.UserName)
						.Return(userNeo4j => userNeo4j.As<User>())
						.Results;
					// if user exist in Neo4j
					if (userConsulted.Any()) {
						// setting alert message
						TempData["msg"] = "<script>alert('Este usuario ya está registrado en Neo4j.');</script>";
					} else {
						client.Cypher
							  .Create("(userNeo4j:User {user})")
							  .WithParam("user", user)
							  .ExecuteWithoutResults();
						// setting alert message
						TempData["msg"] = "<script>alert('Usuario exitosamente registrado.');</script>";
					}
				} catch {
					// setting alert message
					TempData["msg"] = "<script>alert('No hay comunicación con Neo4j.');</script>";
				}
            }
			// let's go to main page
			return RedirectToAction("Index", "Home");
		} // Reg

		// Método para insertar una imagen o logotipo del vendedor-servicio
		public async System.Threading.Tasks.Task<ActionResult> AgregarImagenLugarAsync(HttpPostedFileBase theFile) {
            if (theFile.ContentLength > 0) {
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

        public async Task<ActionResult> BorrarImagenLugarAsync(ObjectId idImagen) {
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
