using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Configuration;

namespace TurismoCR.Controllers
{
    public class CarController : Controller
    {
        public IActionResult Index()
        {

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");

            IDatabase redisDB = redis.GetDatabase();

            redisDB.StringSet("Test", "123queso");


            return View();
        }


    }
}