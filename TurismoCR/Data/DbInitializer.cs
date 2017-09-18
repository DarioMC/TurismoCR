using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TurismoCR.Models;

namespace TurismoCR.Data
{
    public class DbInitializer
    {

        public static void Initialize(TurismoCRContext context)
        {
            context.Database.EnsureCreated();


        }
    }
}
