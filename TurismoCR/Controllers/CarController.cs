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

            //car.IdCarrito=1;
            //car.Usuario = "ochavarria";
            //listser.Add(new Servicio(new DateTime(12, 12, 12), new DateTime(12, 12, 12), "Gold", "Vista al volcan ", "15500", "Cartago", "Cartago", "Cartago"));
            //listser.Add(new Servicio(new DateTime(12, 12, 12), new DateTime(12, 12, 12), "Turista", "Vista a la playa", "10000", "Limon", "Limon", "Limon"));

            //car.Productos = listser;
            
            //redisDB.StringAppend("ochavarria", JsonConvert.SerializeObject(new Servicio(new DateTime(12, 12, 12), new DateTime(12, 12, 12), "Silver", "Vista a la montaña", "18000", "SanJose", "SanJose", "SanJose")));

            

            var result = consultarServicios();

            return View(result);
        }

        [HttpPost]
        public void Agregar(Servicio service )
        {
            
            car.Productos.Add(service);

                
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase redisDB = redis.GetDatabase();
                 
                
            String result = redisDB.StringGet("ochavarria");
            var tempcar = JsonConvert.DeserializeObject<Carrito>(result);
            tempcar.Productos.Add(service);

            String json = JsonConvert.SerializeObject(tempcar);
            redisDB.StringSet("ochavarria",json);

            //return View(tempcar.Productos);
            
            
        }

        [HttpPost]
        public ActionResult DeleteService(Servicio delSer)
        {
            try
            {

                var tempProduc = new List<Servicio>();
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
                IDatabase redisDB = redis.GetDatabase();
                String result = redisDB.StringGet("ochavarria");
                var car = JsonConvert.DeserializeObject<Carrito>(result);
                tempProduc = car.Productos;


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



        private List<Servicio> consultarServicios()
        {

            var productos = new List<Servicio>();

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase redisDB = redis.GetDatabase();
            String result = redisDB.StringGet("ochavarria");
            var car = JsonConvert.DeserializeObject<Carrito>(result);
            productos = car.Productos;

            if (productos == null)
            {
                productos = new List<Servicio>();
            }
            return productos;
        }

    }
}