using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Configuration;
using TurismoCR.Models;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Driver;


namespace TurismoCR.Controllers
{
    public class CarController : Controller
    {
        
        Carrito car = new Carrito();

        public IActionResult Index()
        {
            var result = consultServices();

            return View(result);
        }

        //Agrega un Servicio 
        [HttpPost]
        public ActionResult AddService(CarService service)
        {
            var redisDB = RedisInstance();
            var ownerUsername = Request.Cookies["userSession"];


            String result = redisDB.StringGet(ownerUsername);
            var tempcar = JsonConvert.DeserializeObject<Carrito>(result);
            tempcar.Products.Add(service);

            String json = JsonConvert.SerializeObject(tempcar);
            redisDB.StringSet(ownerUsername, json);
            return RedirectToAction("Index", "Home");
        }

        //Elimina un servicio
        [HttpPost]
        public ActionResult DeleteService(CarService delSer)
        {
            try
            {
                var redisDB = RedisInstance();
                var ownerUsername = Request.Cookies["userSession"];
                var tempProduc = new List<CarService>();

                String result = redisDB.StringGet(ownerUsername);
                var car = JsonConvert.DeserializeObject<Carrito>(result);
                tempProduc = car.Products;

                //Se convierte a string para la comparacion, ya que el id no es fijo.  

                var y = delSer.ToString();
                foreach (var i in tempProduc)
                {
                    var x = i.ToString();

                    if (x.Equals(y))
                    {
                        tempProduc.Remove(i);
                        redisDB.KeyDelete(ownerUsername);
                        car.Products = tempProduc;
                        String json = JsonConvert.SerializeObject(car);

                        redisDB.StringGetSet(ownerUsername, json);

                        return View("Index", tempProduc);

                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }


        //Connsulta en la base si ese carro tiene servicios un servicio
        private List<CarService> consultServices()
        {

            var redisDB = RedisInstance();
            var ownerUsername = Request.Cookies["userSession"];

            String result = redisDB.StringGet(ownerUsername);

            //Verifica si ese usuario ya tiene un carrito creado. 
            if (result == null)
            {

                car.IdCarrito = 1;
                car.UserCar = ownerUsername;
                car.Products = new List<CarService>();


                String json = JsonConvert.SerializeObject(car);
                redisDB.StringSet(ownerUsername, json);
                return car.Products;

            }
            else
            {
                var tempcar = JsonConvert.DeserializeObject<Carrito>(result);
                var products = tempcar.Products;
                if (products.Count == 0)
                {
                    return null;
                }
                else
                {
                    return products;
                }
                
            }

        }

        [HttpPost]
        public void Add(string qnt, string id, string randid, string ownn, string name, string descrip,
                    string cat, string prov, string cant, string dist, string lon, string lat, string dstart,
                    string dend, string price, string ena, string photo) 
        {
            Service tempser = new Service(randid,ownn,name,descrip,cat,prov,cant,dist,lon,lat,dstart,dend,price,
                                                                        true,photo);
            CarService tempCser = new CarService(tempser, qnt);

            var redisDB = RedisInstance();
            var ownerUsername = Request.Cookies["userSession"];


            String result = redisDB.StringGet(ownerUsername);
            var tempcar = JsonConvert.DeserializeObject<Carrito>(result);
            tempcar.Products.Add(tempCser);

            String json = JsonConvert.SerializeObject(tempcar);
            redisDB.StringSet(ownerUsername, json);


            var f = "goood ";

        }

        private ActionResult Follow()
        {
            return RedirectToAction("Index", "Home");
        }





        //Devulve una instancia de la base de datos. 
        private IDatabase RedisInstance()
        {
            var products = new List<Service>();
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase redisDB = redis.GetDatabase();
            return redisDB;
        }


    }

    /*internal class ServiceList : Service
    {
        private DateTime dateTime1;
        private DateTime dateTime2;
        private string v1;
        private string v2;
        private string v3;
        private string v4;
        private string v5;
        private string v6;

        public ServiceList(DateTime dateTime1, DateTime dateTime2, string v1, string v2, string v3, string v4, string v5, string v6)
        {
            this.dateTime1 = dateTime1;
            this.dateTime2 = dateTime2;
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            this.v4 = v4;
            this.v5 = v5;
            this.v6 = v6;
        }
    }*/
}