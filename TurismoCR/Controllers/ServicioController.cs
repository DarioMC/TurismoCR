﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using TurismoCR.Models;
using System.IO;
using System.Threading.Tasks;
using static TurismoCR.Controllers.ImageController;

namespace TurismoCR.Controllers
{
    public class ServicioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> InsertarServicioAsync(Servicio servicio)
        {
            var mongoClient = new MongoClient(connectionString: "mongodb://localhost");
            //var mongoServer = mongoClient.GetServer();
            var db = mongoClient.GetDatabase("TurismoCR");

            var ServicioCollection = db.GetCollection<Servicio>("Servicios");
            await ServicioCollection.InsertOneAsync(servicio);

            //Agregar redireccion a la interfaz en vez de View.
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
        public async Task<ActionResult> EditarServicioAsync(ObjectId IdServicio, Servicio cambiosServicio)
        {

            var mongoClient = new MongoClient(connectionString: "mongodb://localhost");
            //var mongoServer = mongoClient.GetServer();
            var db = mongoClient.GetDatabase("TurismoCR");

            var coleccion = db.GetCollection<Servicio>("Servicios");
            var filtro = Builders<Servicio>.Filter.Eq("_id", IdServicio);
            var resultado = await coleccion.ReplaceOneAsync(filtro, cambiosServicio, new UpdateOptions { IsUpsert = true });

            //Cambiar redireccion a otra vista en vez de View.
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> BorrarServicioAsync(ObjectId idServicio)
        {
            var mongoClient = new MongoClient(connectionString: "mongodb://localhost");
            var db = mongoClient.GetDatabase("TurismoCR");

            var coleccion = db.GetCollection<Servicio>("Servicios");
            var filtro = Builders<Servicio>.Filter.Eq("_id", idServicio);

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

        [HttpPost]
        public async Task<ActionResult> BuscarServicios()
        {
            var mongoClient = new MongoClient(connectionString: "mongodb://localhost");
            var db = mongoClient.GetDatabase("TurismoCR");

            var coleccion = db.GetCollection<Servicio>("Servicios");
            var filtro = new BsonDocument();
            var sort = Builders<Servicio>.Sort.Ascending("Categoria");
            var resultado = await coleccion.Find(filtro).Sort(sort).ToListAsync();

            //Agregar redireccion a otra vista con la lista por parámetro.
            return View();
        }
    }
}
