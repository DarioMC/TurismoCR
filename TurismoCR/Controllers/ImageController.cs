using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TurismoCR.Models;
using TurismoCR.Controllers;
using MongoDB.Bson;
using MongoDB.Driver;
using System.IO;
using System.Linq.Expressions;
using System.Web;
using System.Threading.Tasks;
using System.Linq;
using Neo4jClient;

namespace TurismoCR.Controllers
{
    public class ImageController : Controller
    {
        /*public IActionResult Index()
        {
            var theModel = GetThePictures();
            return View(theModel);
        }


        public ActionResult AddPicture()
        {
            return View();
        }
        */

        public ActionResult Index()
        {
            //var theModel = GetThePictures();
            //return View(theModel);
            return View();
        }

        public ActionResult InsertarImagenLugar()
        {
            return View();

        }

        public ActionResult InsertarImagenServicio()
        {
            return View();

        }

        public ActionResult AddPicture()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult AgregarImagen(HttpPostedFileBase theFile)
        {
            HttpCookie nCookie = Request.Cookies["sesionAbierta"];
            String nUsuario = nCookie.Value.ToString();
            // cambiar password Neo4j
            var cliente = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "adrian");
            cliente.Connect();

            var query = cliente.Cypher
                .Match("(userF:User)")
                .Where((User userF) => userF.UserName == nUsuario)
                .Return(userF => userF.As<User>());

            var userResult = query.Results;

            //var lastId = ["nProd"] as Servicio;

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
                thePicture._id = ObjectId.GenerateNewId();

                bool didItInsert = InsertPictureIntoDatabase(thePicture);

                if (didItInsert)
                    ViewBag.Message = "La imagen fue actualizada correctamente";
                else
                    ViewBag.Message = "Ocurrió un error";
            }
            else
                ViewBag.Message = "Debe subir una imagen";

            return RedirectToAction("Admin", "Servicios", userResult.First());
        }

        private bool InsertPictureIntoDatabase(Imagen thePicture)
        {
            try
            {
                var CollecitonDB = GetPictureCollection();
                var theResult = CollecitonDB.InsertOneAsync(thePicture);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public List<Imagen> GetThePictures()
        {
            var thePictureColleciton = GetPictureCollection();

            // var filter = Builders<Imagenes>.Filter.Exists(_ => true);
            var thePictureCursor = thePictureColleciton.Find(new BsonDocument())
                .Skip(0)
                .Limit(100)
                .ToList();

            return thePictureCursor; //;== new List<Imagenes>();
        }


        public async Task<Imagen> GetAsync(int cod)
        {
            var thePictureColleciton = GetPictureCollection();
            var account = thePictureColleciton.Find(f => f.codPro == cod).FirstAsync();
            return await account;
        }


        public async Task<FileContentResult> ShowPicture(int cod)
        {
            var thePictureColleciton = GetPictureCollection();
            var thePicture = new Imagen();
            thePicture = await GetAsync(cod);

            byte[] thePictureDataAsBytes = Convert.FromBase64String(thePicture.PictureDataAsString);

            return new FileContentResult(thePictureDataAsBytes, "image/jpeg");
        }



        private IMongoCollection<Imagen> GetPictureCollection()
        {

            var Client = new MongoClient("mongodb://localhost:27017");

            var DB = Client.GetDatabase("TurismoCR");

            var collectionDB = DB.GetCollection<TurismoCR.Models.Imagen>("pictures");


            return collectionDB;
        }


        public class HttpPostedFileBase
        {
            internal readonly string FileName;
            internal readonly int ContentLength;
            internal readonly Stream InputStream;
        }

        private class HttpCookie
        {
            internal readonly object Value;

            public static implicit operator HttpCookie(string v)
            {
                throw new NotImplementedException();
            }
        }
    }
}