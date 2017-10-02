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
        List<Service> listser = new List<Service>();
        Carrito car = new Carrito();

        public IActionResult Index()
        {
            var result = consultServices();
            return View(result);
        }

        //Elimina un Agrega un Servicio 
        [HttpPost]
        public void AddService(Service service )
        {
            var redisDB = RedisInstance();
            car.Products.Add(service);

            String result = redisDB.StringGet("doncangrejo");
            var tempcar = JsonConvert.DeserializeObject<Carrito>(result);
            tempcar.Products.Add(service);

            String json = JsonConvert.SerializeObject(tempcar);
            redisDB.StringSet("doncangrejo",json);

            //return View(tempcar.Productos);
            
            
        }

        //Elimina un servicio
        [HttpPost]
        public ActionResult DeleteService(Service delSer)
        {
            try
            {
                var redisDB = RedisInstance();

                var tempProduc = new List<Service>();

                String result = redisDB.StringGet("doncangrejo");
                var car = JsonConvert.DeserializeObject<Carrito>(result);
                tempProduc = car.Products;

                //Se convierte a string para la comparacion, ya que el id no es fijo.  
                
                var y = delSer.ToString();
                foreach (var i in tempProduc)
                {
                    var x= i.ToString();
                    
                    if (x.Equals(y))
                    {
                        tempProduc.Remove(i);
                        redisDB.KeyDelete("doncangrejo");
                        car.Products = tempProduc;
                        String json = JsonConvert.SerializeObject(car);

                        redisDB.StringGetSet("doncangrejo", json);

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
        private List<Service> consultServices()
        {

            var redisDB = RedisInstance();

            String result = redisDB.StringGet("doncangrejo");

            //Verifica si ese usuario ya tiene un carrito creado. 
            if (result == null)
            {

                car.IdCarrito = 1;
                car.UserCar = "doncangrejo";
                car.Products = new List<Service>();
                //revisar la clase temporal Servicio
                //car.Products.Add();
                //car.Products.Add(new Service(new DateTime(12, 12, 12), new DateTime(12, 12, 12), "Gold", "Vista al volcan ", "15500", "Cartago", "Cartago", "Cartago"));
                //car.Productos.Add(new Servicio(new DateTime(12, 12, 12), new DateTime(12, 12, 12), "Turista", "Vista a la playa", "10000", "Limon", "Limon", "Limon"));

                String json = JsonConvert.SerializeObject(car);
                redisDB.StringSet("doncangrejo", json);
                return car.Products; 

            }
            else {
                var tempcar = JsonConvert.DeserializeObject<Carrito>(result);
                var products = tempcar.Products;
                return products;
            }
            
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