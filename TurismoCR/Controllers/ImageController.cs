using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TurismoCR.Models;
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

        public ActionResult AddPicture()
        {
            return View();
        }
        /*
        [HttpPost]
        public ActionResult AgregarImagen(HttpPostedFileBase theFile)
        {
            HttpCookie nCookie = Request.Cookies["sesionAbierta"];
            String nUsuario = nCookie.Value.ToString();
            var cliente = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "test");
            cliente.Connect();

            var query = cliente.Cypher
                .Match("(userF:User)")
                .Where((Usuario userF) => userF.User == nUsuario)
                .Return(userF => userF.As<Usuario>());

            var userResult = query.Results;

            var lastId = Session["nProd"] as Producto;

            if (theFile.ContentLength > 0)
            {
                string theFileName = Path.GetFileName(theFile.FileName);

                byte[] thePictureAsBytes = new byte[theFile.ContentLength];
                using (BinaryReader theReader = new BinaryReader(theFile.InputStream))
                {
                    thePictureAsBytes = theReader.ReadBytes(theFile.ContentLength);
                }

                string thePictureDataAsString = Convert.ToBase64String(thePictureAsBytes);

                Imagenes thePicture = new Imagenes()
                {
                    FileName = theFileName,
                    PictureDataAsString = thePictureDataAsString,
                    codPro = lastId.Id
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

            return RedirectToAction("Admin", "Shop", userResult.First());
        }

        private bool InsertPictureIntoDatabase(Imagenes thePicture)
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


        public List<Imagenes> GetThePictures()
        {
            var thePictureColleciton = GetPictureCollection();

            // var filter = Builders<Imagenes>.Filter.Exists(_ => true);
            var thePictureCursor = thePictureColleciton.Find(new BsonDocument())
                .Skip(0)
                .Limit(100)
                .ToList();

            return thePictureCursor; //;== new List<Imagenes>();
        }


        public async Task<Imagenes> GetAsync(int cod)
        {
            var thePictureColleciton = GetPictureCollection();
            var account = thePictureColleciton.Find(f => f.codPro == cod).FirstAsync();
            return await account;
        }


        public async Task<FileContentResult> ShowPicture(int cod)
        {
            var thePictureColleciton = GetPictureCollection();
            var thePicture = new Imagenes();
            thePicture = await GetAsync(cod);

            byte[] thePictureDataAsBytes = Convert.FromBase64String(thePicture.PictureDataAsString);

            return new FileContentResult(thePictureDataAsBytes, "image/jpeg");
        }



        private IMongoCollection<Imagenes> GetPictureCollection()
        {

            var Client = new MongoClient("mongodb://localhost:27017");

            var DB = Client.GetDatabase("TurismoCR");

            var collectionDB = DB.GetCollection<Imagenes>("pictures");


            return collectionDB;
        }

        */



    }
}