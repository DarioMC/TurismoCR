using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Configuration;
using TurismoCR.Models;
using Newtonsoft.Json;

namespace TurismoCR.Controllers
{
    public class CarController : Controller
    {
        List<Servicio> listser = new List<Servicio>();
        Carrito car = new Carrito();

        public IActionResult Index()
        {
            var result = consultarServicios();
            return View(result);
        }

        //Elimina un Agrega un Servicio 
        [HttpPost]
        public void Agregar(Servicio service )
        {
            var redisDB = RedisInstance();
            car.Productos.Add(service);

            String result = redisDB.StringGet("ochavarria");
            var tempcar = JsonConvert.DeserializeObject<Carrito>(result);
            tempcar.Productos.Add(service);

            String json = JsonConvert.SerializeObject(tempcar);
            redisDB.StringSet("ochavarria",json);

            //return View(tempcar.Productos);
            
            
        }

        //Elimina un servicio
        [HttpPost]
        public ActionResult DeleteService(Servicio delSer)
        {
            try
            {
                var redisDB = RedisInstance();

                var tempProduc = new List<Servicio>();

                String result = redisDB.StringGet("ochavarria");
                var car = JsonConvert.DeserializeObject<Carrito>(result);
                tempProduc = car.Productos;

                //Se convierte a string para la comparacion, ya que el id no es fijo.  
                
                var y = delSer.ToString();
                foreach (var i in tempProduc)
                {
                    var x= i.ToString();
                    
                    if (x.Equals(y))
                    {
                        tempProduc.Remove(i);
                        redisDB.KeyDelete("ochavarria");
                        car.Productos = tempProduc;
                        String json = JsonConvert.SerializeObject(car);

                        redisDB.StringGetSet("ochavarria", json);

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
        private List<Servicio> consultarServicios()
        {

            var redisDB = RedisInstance();

            String result = redisDB.StringGet("ochavarria");

            //Verifica si ese usuario ya tiene un carrito creado. 
            if (result == null)
            {

                car.IdCarrito = 1;
                car.Usuario = "ochavarria";
                car.Productos = new List<Servicio>();
                //car.Productos.Add(new Servicio(new DateTime(12, 12, 12), new DateTime(12, 12, 12), "Gold", "Vista al volcan ", "15500", "Cartago", "Cartago", "Cartago"));
                //car.Productos.Add(new Servicio(new DateTime(12, 12, 12), new DateTime(12, 12, 12), "Turista", "Vista a la playa", "10000", "Limon", "Limon", "Limon"));
                
                String json = JsonConvert.SerializeObject(car);
                redisDB.StringSet("ochavarria", json);
                return car.Productos; 

            }
            else {
                var tempcar = JsonConvert.DeserializeObject<Carrito>(result);
                var productos = tempcar.Productos;
                return productos;
            }
            
        }

        //Devulve una instancia de la base de datos. 
        private IDatabase RedisInstance()
        {
            var productos = new List<Servicio>();
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase redisDB = redis.GetDatabase();
            return redisDB; 
        }


    }
}